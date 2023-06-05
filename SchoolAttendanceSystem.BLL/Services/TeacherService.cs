
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;
using SchoolAttendanceSystem.BLL.DTOs;
using SchoolAttendanceSystem.BLL.DTOs.AccountDTOs.Requests;
using SchoolAttendanceSystem.BLL.DTOs.SubjectDTOs;
using SchoolAttendanceSystem.BLL.DTOs.TeacherDTOs;
using SchoolAttendanceSystem.BLL.DTOs.TeacherDTOs.Requests;
using SchoolAttendanceSystem.BLL.Extensions.Profiles;
using SchoolAttendanceSystem.BLL.Filters;
using SchoolAttendanceSystem.BLL.IServices;
using SchoolAttendanceSystem.DAL.Constants;
using SchoolAttendanceSystem.DAL.Entities.Auth;
using SchoolAttendanceSystem.DAL.Entities.Domain;
using SchoolAttendanceSystem.DAL.IRepositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SchoolAttendanceSystem.BLL.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly UserManager<User> _userManager;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IWebHostEnvironment _env;

        public TeacherService(ITeacherRepository teacherRepository, UserManager<User> userManager, IWebHostEnvironment env, ISubjectRepository subjectRepository)
        {
            _teacherRepository = teacherRepository;
            _userManager = userManager;
            _env = env;
            _subjectRepository = subjectRepository;
        }

        public async Task<GenericResponse<TeacherDto>> CreateTeacherAsync(CreateTeacherRequest request)
        {
            var registerUserRequest = new RegisterUserRequest
            {
                FullName = request.FullName,
                Role = RoleTypes.Teacher.ToString()
            };

            string? imageUrl = request.Image == null ?
                DefaultUrls.DefaultUserImage :
                await FactoryService.GetImageUrlAsync(request.Image, _env);

            var createdUser = await FactoryService.CreateUserAsync(registerUserRequest, _userManager, imageUrl);

            if (createdUser == null)
                return new() { Errors = new() { "Error in creating new user for a teacher" } };

            var teacherEntity = request.FromCreateToTeacherEntity(createdUser.Id);

            if (!await _teacherRepository.CreateAsync(teacherEntity))
                return new() { Errors = new() { "Error in creating a teacher in DB" } };

            await _teacherRepository.SaveChangesAsync();

            var teacherDto = teacherEntity.ToTeacherDto(createdUser);

            return new()
            {
                Succeeded = true,
                Status = 200,
                Message = "Created",
                Data = teacherDto
            };
        }

        public async Task<GenericResponse<object>> DeleteTeacherAsync(string id)
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

        public async Task<GenericResponse<TeacherDto>> GetParentAsync(string id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);

            if (teacher == null)
                return new() { Errors = new() { "Invalid id" } };

            var user = await _userManager.FindByIdAsync(id);

            return new()
            {
                Succeeded = true,
                Status = 200,
                Data = teacher.ToTeacherDto(user)
            };
        }

        public async Task<GenericResponse<IEnumerable<SubjectDto>>> GetSubjectsForTeacher(string id)
        {
            var teacher = await _teacherRepository.GetTeacherById(id);

            if (teacher == null)
                return new() { Errors = new() { "invalid id" } };

            var subjects = await _teacherRepository.GetAllSubejects(id);

            if (subjects == null)
                return new() { Message = "the teacher has no subjects", Errors = new() { "not found subjects for the teacher" } };

            var subjectsDto = new List<SubjectDto>();

            foreach (var s in subjects)
                subjectsDto.Add(new() { SubjectCode = s.SubjectCode, SubjectName = s.SubjcetName, Class = s.ClassName, TeacherName = teacher.User.FullName });

            return new()
            {
                Succeeded = true,
                Status = 200,
                Data = subjectsDto
            };

        }

        public async Task<PagedResponse<List<TeacherDto>>> GetTeachersAsync(PaginationTeacherFilter request)
        {
            var query = await _teacherRepository.GetAllTeachersAsync();

            if (query == null)
                return new() { Errors = new() { "There is no teachers in DB" } };

            // Apply filter by name, if provided
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(t => t.User.FullName.ToUpper().Contains(request.Name.ToUpper()));
            }

            // Apply filter by specialize, if provided
            if (!string.IsNullOrEmpty(request.Specialize))
            {
                query = query.Where(t => t.Specialize.ToUpper().Contains(request.Specialize.ToUpper()));
            }

            var totalRecords = await Task.Factory.StartNew(() => query.Count());

            var skip = (request.PageNumber - 1) * request.PageSize;
            var teachers = await Task.Factory.StartNew(() => query.Skip(skip).Take(request.PageSize).ToList());

            List<TeacherDto> teachersDto = new();

            foreach (var teacher in teachers)
                teachersDto.Add(teacher.ToTeacherDto(teacher.User));

            return new()
            {
                Succeeded = true,
                Status = 200,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                Message = "Get Teachers Succeeded",
                Data = teachersDto
            };
        }

        public async Task<GenericResponse<IEnumerable<SubjectDto>>> SetTeacherToSubjectAsync(SetTeacherToSubject dto)
        {
            var teacher = await _teacherRepository.GetTeacherById(dto.TeacherId);

            if (teacher == null)
                return new() { Errors = new() { "invalid id" } };

            var subject = await _subjectRepository.GetByIdAsync(dto.SubjectCode);

            if (subject == null)
                return new() { Errors = new() { "the subjcet not exist" } };

            subject.TeacherId = dto.TeacherId;
            subject.Teacher = teacher;

            await _subjectRepository.SaveChangesAsync();

            return await GetSubjectsForTeacher(dto.TeacherId);
        }

        public async Task<GenericResponse<TeacherDto>> UpdateParentAsync(UpdateTeacherDto dto)
        {
            var teacher = await _teacherRepository.GetByIdAsync(dto.TeacherId);

            if (teacher == null)
                return new() { Errors = new() { "Invalid id" } };

            var updatedTeacher = dto.FromUpdateDtoToTeacherEntity(teacher);

            if (!_teacherRepository.Update(teacher, updatedTeacher))
                return new() { Errors = new() { "Cannot update the teacher" } };

            await _teacherRepository.SaveChangesAsync();
            var user = await _userManager.FindByIdAsync(dto.TeacherId);

            user.FullName = dto.Name;
            await _userManager.UpdateAsync(user);

            return new()
            {
                Succeeded = true,
                Status = 201,
                Message = "Updated",
                Data = updatedTeacher.ToTeacherDto(user)
            };
        }
    }
}
