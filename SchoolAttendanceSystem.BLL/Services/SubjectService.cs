using SchoolAttendanceSystem.BLL.DTOs;
using SchoolAttendanceSystem.BLL.DTOs.SubjectDTOs;
using SchoolAttendanceSystem.BLL.Filters;
using SchoolAttendanceSystem.BLL.IServices;
using SchoolAttendanceSystem.DAL.Constants;
using SchoolAttendanceSystem.DAL.Entities.Domain;
using SchoolAttendanceSystem.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }


        public async Task<GenericResponse<SubjectDto>> CreateSubjectAsync(CreateSubjectDto dto)
        {
            // Create code for the subject
            var initCode = InitialCharSubjects.GetInitialSubject(dto.SubjectName);

            if (initCode == null)
                return new() { Errors = new() { "Invalid subject name" } };

            var subjectCode = initCode + $"-{dto.Class}";

            // create new subject and save it in DB
            var subject = new Subject
            {
                SubjectCode = subjectCode,
                SubjcetName = dto.SubjectName,
                ClassName = dto.Class
            };

            if (!await _subjectRepository.CreateAsync(subject))
                return new() { Errors = new() { "error in creating new subject" } };

            await _subjectRepository.SaveChangesAsync();

            return new()
            {
                Succeeded = true,
                Status = 200,
                Message = "created",
                Data = null
            };
        }

        public async Task<GenericResponse<object>> DeleteSubjectAsync(string subjectCode)
        {
            var subject = await _subjectRepository.GetByIdAsync(subjectCode);

            if (subject == null)
                return new() { Errors = new() { "invalid subject" } };

            _subjectRepository.Delete(subject);
            await _subjectRepository.SaveChangesAsync();

            return new()
            {
                Succeeded = true,
                Status = 200,
                Message = "Deleted",
                Data = null
            };
        }

        public async Task<GenericResponse<IEnumerable<string>>> GetSbjectsNames()
        {
            var names = new List<string>();

            foreach (var subject in Enum.GetValues(typeof(Subjects)))
                names.Add(subject.ToString());

            return new()
            {
                Succeeded = true,
                Status = 200,
                Message = "successeded",
                Data = names
            };
        }

        public async Task<GenericResponse<SubjectDto>> GetSubjectAsync(string subjectCode)
        {
            var subject = await _subjectRepository.GetSubjectIncludeTeacher(subjectCode);

            if (subject == null)
                return new() { Status = 404, Errors = new() { "not found" } };

            var subjectDto = new SubjectDto
            {
                SubjectCode = subject.SubjectCode,
                SubjectName = subject.SubjcetName,
                Class = subject.ClassName,
            };

            return new()
            {
                Succeeded = true,
                Status = 200,
                Data = subjectDto
            };
        }

        public async Task<GenericResponse<IEnumerable<SubjectDto>>> GetSubjectsForClass(string className)
        {
            var subjects = await _subjectRepository.GetSubjectsInsideClass(className);
            var subjectsDto = new List<SubjectDto>();

            foreach (var s in subjects)
                subjectsDto.Add(new()
                {
                    SubjectCode = s.SubjectCode,
                    SubjectName= s.SubjcetName,
                    Class = s.ClassName,
                    TeacherName = s.Teacher.User.FullName
                });

            return new()
            {
                Succeeded = true,
                Status = 200,
                Data = subjectsDto
            };
        }

        public async Task<GenericResponse<SubjectDto>> UpdateSubjectAsync(UpdateSubjectDto dto)
        {
            var subject = await _subjectRepository.GetByIdAsync(dto.SubjectCode);
            var initSubject = InitialCharSubjects.GetInitialSubject(dto.SubjectName);

            if (initSubject == null)
                return new() { Errors = new() { "invalid subject name" } };


            var newSubject = new Subject
            {
                SubjectCode = initSubject + $"-{dto.Class}",
                SubjcetName = dto.SubjectName,
                ClassName = dto.Class
            };


            if (!_subjectRepository.Update(subject, newSubject))
                return new() { Errors = new() { "error in updating the subject" } };

            await _subjectRepository.SaveChangesAsync();

            return new()
            {
                Succeeded = true,
                Status = 200,
                Message = "updaetd",
                Data = new()
                {
                    SubjectCode = newSubject.SubjectCode,
                    SubjectName = newSubject.SubjcetName,
                    Class = newSubject.ClassName,
                }
            };
        }

        public async Task<PagedResponse<IEnumerable<SubjectDto>>> GetPaginatedSubjects(PaginationSubjectFilter pagination)
        {
            var subjects = await _subjectRepository.GetAllSubjectsIncludeTeacher();

            if (!String.IsNullOrEmpty(pagination.Grade))
            {
                subjects = subjects.Where(s => s.ClassName.Contains(pagination.Grade)).ToList();
            }
            if (!String.IsNullOrEmpty(pagination.Class))
            {
                subjects = subjects.Where(s => s.ClassName == pagination.Class).ToList();
            }

            var totalCount = subjects.Count();

            subjects = subjects
                       .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                       .Take(pagination.PageSize)
                       .ToList();

            var subjectsDto = new List<SubjectDto>();

            foreach (var s in subjects)
                subjectsDto.Add(new()
                {
                    SubjectCode = s.SubjectCode,
                    SubjectName = s.SubjcetName,
                    Class = s.ClassName,
                });

            return new()
            {
                Succeeded = true,
                Status = 200,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalRecords = totalCount,
                Data = subjectsDto
            };

        }


    }
}
