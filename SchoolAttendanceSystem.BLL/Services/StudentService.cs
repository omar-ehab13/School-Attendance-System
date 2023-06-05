using Bogus.DataSets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolAttendanceSystem.BLL.DTOs;
using SchoolAttendanceSystem.BLL.DTOs.AccountDTOs;
using SchoolAttendanceSystem.BLL.DTOs.StudentDTOs;
using SchoolAttendanceSystem.BLL.DTOs.StudentDTOs.Requests;
using SchoolAttendanceSystem.BLL.Extensions.Profiles;
using SchoolAttendanceSystem.BLL.Filters;
using SchoolAttendanceSystem.BLL.IServices;
using SchoolAttendanceSystem.DAL.Constants;
using SchoolAttendanceSystem.DAL.Entities.Auth;
using SchoolAttendanceSystem.DAL.Entities.Domain;
using SchoolAttendanceSystem.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.Services
{
    public class StudentService : IStudentService
    {
        #region Private Fields
        private readonly IStudentRepository _studentRepository;
        private readonly IParentService _parentService;
        private readonly IParentRepository _parentRepository;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        #endregion

        #region Constructor
        public StudentService(
            IStudentRepository studentRepository,
            IParentService parentService,
            IParentRepository parentRepository,
            UserManager<User> userManager,
            IWebHostEnvironment env)
        {
            _studentRepository = studentRepository;
            _parentService = parentService;
            _parentRepository = parentRepository;
            _userManager = userManager;
            _env = env;
        }
        #endregion

        public async Task<GenericResponse<object>> AddStdudentAsync(AddStudentRequest request)
        {
            #region add new parent if not exist

            var parent = await _parentRepository.FindParentByNid(request.ParentNid!);
            StringBuilder message = new StringBuilder();

            // Check if the parent is not exist already
            if (parent == null)
            {
                var addParentResponse = await _parentService.AddParentAsync(request.ToAddParentRequest());

                if (!addParentResponse.Succeeded)
                    return new GenericResponse<object> { Errors = addParentResponse.Errors };

                parent = await _parentRepository.FindParentByNid(request.ParentNid!);
                message.Append("New Parent Created");
            }

            #endregion

            #region create new student

            string? imageUrl = request.Image == null?
                DefaultUrls.DefaultUserImage : 
                await FactoryService.GetImageUrlAsync(request.Image, _env);

            var student = request.ToStudetnEnttiy(parent, imageUrl);

            if (!await _studentRepository.CreateAsync(student))
            {
                // TODO: Delete user and parent
                return new GenericResponse<object> { Errors = new() { "Error in creating a new student" } };
            }

            await _studentRepository.SaveChangesAsync();
            message.Append(",New Student Created");

            #endregion

            #region prepare response and return it

            var user = await _userManager.FindByIdAsync(parent.ParentId);

            object data = new
            {
                StudentId = student.Id,
                StudentName = student.FirstName + " " + user.FullName,
                Class = student.ClassCode,
                ParentId = parent.ParentId,
                ParentEmail = user.Email,
                ParentPhone = parent.PhoneNumber,
                ParentJob = parent.Job,
                Governorate = parent.Governorate,
                City = parent.City,
                Address = parent.Address,
                ImageUrl = imageUrl
            };

            return new GenericResponse<object>
            {
                Succeeded = true,
                Status = 201,
                Message = message.ToString(),
                Data = data
            };

            #endregion
        }

        public async Task<GenericResponse<object>> DeleteStudentAsync(string id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            
            var message = "";

            if (student == null)
                return new() { Errors = new() { "Invalid id" } };

            var imageUrl = student.ImageUrl;

            if (!_studentRepository.Delete(student))
                return new() { Errors = new() { "Cannot delte student" } };

            if (imageUrl != DefaultUrls.DefaultUserImage)
            {
                var path = Path.Combine(_env.WebRootPath, imageUrl!);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }

            message = "student deleted";
            await _studentRepository.SaveChangesAsync();

            var parent = await _parentRepository.GetByIdAsync(student.ParentId);
            var students = await _parentRepository.GetStudentsAsync(parent);

            if (students.Count() == 0)
            {
                await _parentService.DeleteParentAsync(parent.ParentId);
                message += ",parent deleted";
            }

            await _studentRepository.SaveChangesAsync();

            return new()
            {
                Succeeded = true,
                Status = 200,
                Message = message
            };
        }

        public async Task<GenericResponse<StudentModel>> GetStudentByIdAsync(string id)
        {
            var student = await _studentRepository.GetStudentIncludeClass(id);

            if (student == null)
                return new() { Errors = new() { "Invalid id" } };

            var parent = await _parentRepository.GetByIdAsync(student.ParentId);
            var user = await _userManager.FindByIdAsync(parent.ParentId);

            var studentDto = student.ToStudentDTO(parent, user);

            return new()
            {
                Succeeded = true,
                Status = 200,
                Data = studentDto
            };
        }

        public async Task<PagedResponse<IEnumerable<StudentModel>>> GetStudentsAsync(PaginationStudentsFilter pagination)
        {
            IEnumerable<Student> students;
            if (pagination.Name == null)
            {
                students = await _studentRepository.GetPaginatedStudents(pagination.PageNumber, pagination.PageSize);
            }
            else
            {
                students = await _studentRepository.GetPaginatedStudents(pagination.PageNumber, pagination.PageSize, pagination.Name);
            }

            

            if (!string.IsNullOrEmpty(pagination.Grade))
            {
                students = students.Where(s => s.Class.Grade == pagination.Grade);

                if (!string.IsNullOrEmpty(pagination.Class))
                {
                    students = students.Where(s => s.Class.ClassName == pagination.Class);
                }
            }

            if (!string.IsNullOrEmpty(pagination.Name))
            {
                students = students.Where(
                    s => s.FirstName.ToLower().Contains(pagination.Name.ToLower()));
            }

            var allStudents = await _studentRepository.GetAllAsync();
            var totalRecords = allStudents.Count();
            var studentModels = new List<StudentModel>();

            foreach (var student in students)
            {
                var parent = await _parentRepository.GetByIdAsync(student.ParentId);
                var user = await _userManager.FindByIdAsync(parent.ParentId);

                studentModels.Add(student.ToStudentDTO(parent, user));
            }

            return new PagedResponse<IEnumerable<StudentModel>>
            {
                Succeeded = true,
                Status = 200,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalRecords = totalRecords,
                Data = studentModels
            };
        }

        public async Task<GenericResponse<StudentModel>> UpdateStudentAsync(UpdateStudentDto dto)
        {
            var student = await _studentRepository.GetByIdAsync(dto.StudentId);

            if (student == null)
                return new() { Errors = new() { "Invalid id" } };

            string imageUrl = dto.Image == null ?
                student.ImageUrl! :
                await FactoryService.GetImageUrlAsync(dto.Image, _env);

            var updatedStudent = dto.FromUpdateToStudentModel(student, imageUrl);

            if (!_studentRepository.Update(student, updatedStudent))
                return new() { Errors = new() { "Cannot update student" } };

            await _studentRepository.SaveChangesAsync();

            var parent = await _parentRepository.GetByIdAsync(student.ParentId);
            var user = await _userManager.FindByIdAsync(parent.ParentId);

            var studentModel = updatedStudent.ToStudentDTO(parent, user);

            return new()
            {
                Succeeded = true,
                Status = 200,
                Message = "updated",
                Data = studentModel
            };
        }

        public async Task<GenericResponse<object>> UpdateUserImageAsync(UpdateStudentImageDto dto)
        {
            var student = await _studentRepository.GetByIdAsync(dto.StudentId);

            if (student == null)
                return new() { Errors = new() { "Invalid id" } };

            var imageUrl = student.ImageUrl;

            if (imageUrl == null)
            {
                imageUrl = DefaultUrls.DefaultUserImage;
            }

            // Add new image
            if (dto.Image != null)
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
                imageUrl = await FactoryService.GetImageUrlAsync(dto.Image, _env);
            }

            var newStudent = student;
            newStudent.ImageUrl = imageUrl;

            _studentRepository.Update(student, newStudent);
            await _studentRepository.SaveChangesAsync();

            return new()
            {
                Succeeded = true,
                Status = 200,
                Message = "image updated",
                Data = newStudent
            };
        }
    }
}
