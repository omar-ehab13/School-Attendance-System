using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using SchoolAttendanceSystem.BLL.DTOs;
using SchoolAttendanceSystem.BLL.DTOs.AccountDTOs.Requests;
using SchoolAttendanceSystem.BLL.DTOs.ParentDTOs;
using SchoolAttendanceSystem.BLL.DTOs.ParentDTOs.Requests;
using SchoolAttendanceSystem.BLL.DTOs.ParentDTOs.Responses;
using SchoolAttendanceSystem.BLL.Extensions.Profiles;
using SchoolAttendanceSystem.BLL.IServices;
using SchoolAttendanceSystem.DAL.Constants;
using SchoolAttendanceSystem.DAL.Entities.Auth;
using SchoolAttendanceSystem.DAL.Entities.Domain;
using SchoolAttendanceSystem.DAL.IRepositories;

namespace SchoolAttendanceSystem.BLL.Services
{
    public class ParentService : IParentService
    {
        #region Private Fields
        private readonly IParentRepository _parentRepository;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        #endregion

        #region Constructor
        public ParentService(
            IParentRepository parentRepository, 
            UserManager<User> userManager,
            IWebHostEnvironment env
            )
        {
            _parentRepository = parentRepository;
            _userManager = userManager;
            _env = env;
        }
        #endregion

        public async Task<GenericResponse<ParentModel>> AddParentAsync(AddParentRequest request)
        {
            #region create new user
            var registerUserRequest = new RegisterUserRequest
            {
                FullName = request.FullName,
                Role = RoleTypes.Parent.ToString(),
            };

            string imageUrl = request.Image == null ?
                DefaultUrls.DefaultUserImage :
                await FactoryService.GetImageUrlAsync(request.Image, _env);

            var user = await FactoryService.CreateUserAsync(registerUserRequest, _userManager, imageUrl);

            if (user == null)
                return new GenericResponse<ParentModel> { Errors = new(){ "Cannot create the user" } };
            #endregion

            #region create new parent
            var parent = request.ToParentEntity(user);

            if (!await _parentRepository.CreateAsync(parent))
            {
                await _userManager.DeleteAsync(user);
                return new GenericResponse<ParentModel> { Errors = new() { "Error while creating the parent in DB" } };
            }

            await _parentRepository.SaveChangesAsync();
            #endregion

            return new GenericResponse<ParentModel>
            {
                Succeeded = true,
                Status = 201,
                Message = "Created",
                Data = parent.ToParentModel(user)
            };
        }

        public async Task<PagedResponse<List<ParentModel>>> GetParentsAsync(
            int pageNumber = 1, int pageSize = 10, string? name=null)
        {
            IEnumerable<Parent> parents;
            int totalRecords;

            if (name == null)
            {
                parents = await _parentRepository.GetPaginatedParents(pageNumber, pageSize);
                var allData = await _parentRepository.GetAllAsync();
                totalRecords = allData.Count();
            }
            else
            {
                parents = await _parentRepository.GetPaginatedParents(pageNumber, pageSize, name);
                var allData = await _parentRepository.GetParentsByName(name);
                totalRecords = allData.Count();
            }

            if (parents == null)
            {
                return new PagedResponse<List<ParentModel>>
                {
                    Message = "Not Found",
                    Status = 404
                };
            }

            var parentsModel = new List<ParentModel>();

            foreach (var parent in parents)
                parentsModel.Add(await FactoryService.GetParentModelAsync(parent.ParentId, _parentRepository, _userManager));

            var response = new PagedResponse<List<ParentModel>>()
            {
                Succeeded = true,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                Data = parentsModel,
                Status = 200
            };

            return response;
        }

        public async Task<GenericResponse<ParentModel>> GetParentAsync(string parentId)
        {
            var parentModel = await FactoryService.GetParentModelAsync(parentId, _parentRepository, _userManager);

            if (parentModel == null)
                return new GenericResponse<ParentModel>
                {
                    Status = 404,
                    Message = "Not Found"
                };

            return new GenericResponse<ParentModel>
            {
                Succeeded = true,
                Status = 200,
                Message = "Succeeded",
                Data = parentModel
            };
        }

        public async Task<GenericResponse<ParentModel>> UpdateParentAsync(UpdateParentDto dto)
        {
            var parent = await _parentRepository.GetByIdAsync(dto.ParentId);

            if (parent == null)
                return new() { Errors = new() { "Invalid Id" } };

            var updatedParent = dto.FromUpdateToParentEntity(parent);

            if (!_parentRepository.Update(parent, updatedParent))
                return new() { Errors = new() { "Cannot update parent" } };

            await _parentRepository.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(dto.ParentId);

            user.FullName = dto.Name;
            await _userManager.UpdateAsync(user);

            var parentModel = updatedParent.ToParentModel(user);

            return new()
            {
                Succeeded = true,
                Status = 201,
                Message = "Updated",
                Data = parentModel
            };
        }

        public async Task<GenericResponse<object>> DeleteParentAsync(string parentId)
        {
            var user = await _userManager.FindByIdAsync(parentId);

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
    }
}
