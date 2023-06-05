using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolAttendanceSystem.BLL.DTOs;
using SchoolAttendanceSystem.BLL.DTOs.AccountDTOs;
using SchoolAttendanceSystem.BLL.DTOs.AccountDTOs.Requests;
using SchoolAttendanceSystem.BLL.Extensions.Profiles;
using SchoolAttendanceSystem.BLL.Helpers;
using SchoolAttendanceSystem.BLL.IServices;
using SchoolAttendanceSystem.DAL.Constants;
using SchoolAttendanceSystem.DAL.Entities.Auth;
using SchoolAttendanceSystem.DAL.IRepositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolAttendanceSystem.BLL.Services
{
    public class AccountService : IAccountService
    {
        #region Private Fields
        private readonly UserManager<User> _userManager;
        private readonly JWT _jwt;
        private readonly IParentRepository _parentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IWebHostEnvironment _env;
        #endregion

        #region Constructor
        public AccountService(
            UserManager<User> userManager, 
            IOptions<JWT> options, 
            IParentRepository parentRepository,
            ITeacherRepository teacherRepository,
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _jwt = options.Value;
            _parentRepository = parentRepository;
            _teacherRepository = teacherRepository;
            _env = env;
        }
        #endregion

        public async Task<AuthResponse<object>> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            // check from email and password
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return new AuthResponse<object>
                {
                    Status = 404,
                    Message = "Not Found",
                    Errors = new() { "Invalid email or password" }
                };

            // create jwt token and get user id for response
            var jwtSecurityToken = await CreateJwtToken(user);
            var userId = await _userManager.GetUserIdAsync(user);

            // return response
            var roles = await _userManager.GetRolesAsync(user);

            var data = await GetResponseDataByRole(user, roles[0]);

            return new AuthResponse<object>
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Succeeded = true,
                Message = "Login Successfully",
                Status = 200,
                Data = data
            };
        }

        public async Task<GenericResponse<UserModel>> RegisterUserAsync(RegisterUserRequest request)
        {
            #region Create new random email

            string email = await FactoryService.GenerateRandomEmail(request.FullName, request.Role);

            while (await _userManager.FindByEmailAsync(email) != null)
            {
                email = await FactoryService.GenerateRandomEmail(request.FullName, request.Role, 2);
            }

            #endregion


            #region Create new user 

            // convert request to user entity

            string? imageUrl = request.Image == null ?
                DefaultUrls.DefaultUserImage : 
                await FactoryService.GetImageUrlAsync(request.Image, _env);

            var user = request.ToUserEntity(email, imageUrl);

            // try to create new user in DB
            var result = await _userManager.CreateAsync(user, Defaults.DefaultUserPassword);

            // check if the user not created correctly
            if (!result.Succeeded)
            {
                var errors = new List<string>();
                foreach (var error in result.Errors)
                    errors.Add(error.Description);

                return new GenericResponse<UserModel> { Errors = errors };
            }

            // add the user to the role
            var addUserToRole = await _userManager.AddToRoleAsync(user, request.Role);

            if (!addUserToRole.Succeeded)
                return new GenericResponse<UserModel> { Errors = new() { "Cannot add the user for the role" } };

            #endregion


            #region prepare response and return it

            var createdUser = await _userManager.FindByEmailAsync(email);

            var userModel = createdUser.ToUserModel(request.Role);

            return new GenericResponse<UserModel>
            {
                Succeeded = true,
                Status = 201,
                Message = "Created",
                Data = userModel
            };

            #endregion
        }


        public async Task<GenericResponse<object>> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return new() { Errors = new() { "Invalid id" } };

            var imageUrl = user.ImageUrl;

            if (imageUrl != DefaultUrls.DefaultUserImage)
            {
                var path = Path.Combine(_env.WebRootPath, imageUrl!);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }

            await _userManager.DeleteAsync(user);

            return new()
            {
                Succeeded = true,
                Status = 200,
                Message = "Deleted"
            };
        }

        public async Task<GenericResponse<UserModel>> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return new() { Errors = new() { "Invalid id" } };

            var roles = await _userManager.GetRolesAsync(user);

            var userModel = user.ToUserModel(roles[0]);

            return new()
            {
                Succeeded = true,
                Status = 200,
                Data = userModel
            };
        }

        public async Task<GenericResponse<UserModel>> UpdateUserAsync(string id, UpdateUserDto dto)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return new() { Errors = new() { "Invalid id" } };

            user.PhoneNumber = dto.PhoneNumber;
            user.FullName = dto.FullName;

            await _userManager.UpdateAsync(user);

            var roles = await _userManager.GetRolesAsync(user);
            var userModel = user.ToUserModel(roles[0]);

            return new()
            {
                Succeeded = true,
                Status = 200,
                Message = "updated",
                Data = userModel
            };
        }

        public async Task<GenericResponse<UserModel>> UpdateUserImageAsync(string? id, IFormFile? userImage)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return new() { Errors = new() { "Invalid id" } };

            var imageUrl = user.ImageUrl;

            if (imageUrl == null)
            {
                imageUrl = DefaultUrls.DefaultUserImage;
            }

            // Add new image
            if (userImage != null)
            {
                // Delete old image
                if (imageUrl != DefaultUrls.DefaultUserImage)
                {
                    var path = Path.Combine(_env.WebRootPath, imageUrl!);

                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }

                // add new image
                imageUrl = await FactoryService.GetImageUrlAsync(userImage, _env);
            }

            user.ImageUrl = imageUrl;
            await _userManager.UpdateAsync(user);

            var roles = await _userManager.GetRolesAsync(user);
            var userModel = user.ToUserModel(roles[0]);

            return new()
            {
                Succeeded = true,
                Status = 200,
                Message = "image updated",
                Data = userModel
            };
        }


        #region Private Methods

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            #region Determine Claims for the token

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim("uid", user.Id),
                new Claim("roles", roles[0]),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            #endregion

            #region Determine Signing Credentials

            var key = Encoding.UTF8.GetBytes(_jwt.Key);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            #endregion

            #region Create And Return

            var jwtSecurityToken = new JwtSecurityToken(
                //issuer: _jwt.Issuer,
                //audience: _jwt.Audience,
                claims: claims,
                //expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;

            #endregion
        }

        private async Task<object> GetResponseDataByRole(User user, string role)
        {
            //switch(role)
            //{
            //    case RoleTypes.Parent.ToString():
            //        return await _parentService.GetParentAsync(user.Id);
            //    case RoleTypes.Teacher.ToString():
            //    case RoleTypes.Admin.ToString():
            //    case RoleTypes.SuperAdmin.ToString():
            //        return null;
            //}

            if (role == RoleTypes.Parent.ToString())
                return await FactoryService.GetParentModelAsync(user.Id, _parentRepository, _userManager);

            if (role == RoleTypes.SuperAdmin.ToString() || role == RoleTypes.Admin.ToString())
                return user.ToUserModel(role);

            if (role == RoleTypes.Teacher.ToString())
                return await FactoryService.GetTeacherDtoAsync(user.Id, _teacherRepository, _userManager);

            return null!;
        }


        #endregion
    }
}
