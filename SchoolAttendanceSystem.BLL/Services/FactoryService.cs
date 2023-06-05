using Microsoft.AspNetCore.Identity;
using SchoolAttendanceSystem.BLL.DTOs.AccountDTOs;
using SchoolAttendanceSystem.BLL.DTOs;
using SchoolAttendanceSystem.BLL.DTOs.AccountDTOs.Requests;
using SchoolAttendanceSystem.BLL.DTOs.ParentDTOs;
using SchoolAttendanceSystem.BLL.Extensions.Profiles;
using SchoolAttendanceSystem.DAL.Constants;
using SchoolAttendanceSystem.DAL.Entities.Auth;
using SchoolAttendanceSystem.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using SchoolAttendanceSystem.BLL.DTOs.TeacherDTOs;

namespace SchoolAttendanceSystem.BLL.Services
{
    public static class FactoryService
    {
        public static async Task<ParentModel> GetParentModelAsync(
            string id,
            IParentRepository parentRepository,
            UserManager<User> userManager)
        {
            var parent = await parentRepository.GetByIdAsync(id);
            var user = await userManager.FindByIdAsync(id);

            if (user == null || parent == null)
                return null!;

            var stds = await parentRepository.GetStudentsAsync(parent);
            var students = new List<ChildrenDto>();

            foreach (var student in stds) 
            {
                students.Add(new() { Name = student.FirstName, StudentId = student.Id });
            }

            return parent.ToParentModel(user, students);
        }

        public static async Task<TeacherDto> GetTeacherDtoAsync(string id, ITeacherRepository _teacherRepository, UserManager<User> _userManager)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);
            var user = await _userManager.FindByIdAsync(id);

            if (user == null || teacher == null)
                return null;

            return teacher.ToTeacherDto(user);
        }

        public static async Task<User> CreateUserAsync(RegisterUserRequest request, UserManager<User> userManager, string imageUrl)
        {
            string email = await GenerateRandomEmail(request.FullName, request.Role);

            // if created email is already exist
            while (await userManager.FindByEmailAsync(email) != null)
            {
                email = await GenerateRandomEmail(request.FullName, request.Role, 2);
            }

            // convert request to user entity
            var user = request.ToUserEntity(email, imageUrl);

            // try to create new user in DB
            var result = await userManager.CreateAsync(user, Defaults.DefaultUserPassword);

            // check if the user not created correctly
            if (!result.Succeeded)
                return null!;

            // add the user to the role
            var addUserToRole = await userManager.AddToRoleAsync(user, request.Role);

            return user;
        }

        public static async Task<string> GetImageUrlAsync(IFormFile imageFile, IWebHostEnvironment env)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            var path = Path.Combine(env.WebRootPath, "Images", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return $"Images/{fileName}";
        }

        public static Task<string> GenerateRandomEmail(string fullName, string role, int num = 0)
        {
            var splitedName = fullName.Split(" ");

            if (splitedName.Length >= 2)
            {
                var firstName = splitedName[0].ToLower();
                var secondName = splitedName[1].ToLower();
                var emailExtension = EmailExtensions.GetEmailExtension(role);

                if (num == 0)
                    return Task.FromResult(firstName + secondName + emailExtension);

                byte[] randomNumbers = new byte[num];
                StringBuilder numbersBuilder = new StringBuilder();

                for (int i = 0; i < randomNumbers.Length; i++)
                {
                    randomNumbers[i] = (byte)new Random().Next(10);
                    numbersBuilder.Append(randomNumbers[i]);
                }

                return Task.FromResult(firstName + secondName + numbersBuilder.ToString() + emailExtension);
            }

            return Task.FromResult(fullName + new Random().Next(10_000).ToString() + EmailExtensions.GetEmailExtension(role));
        }
    }
}
