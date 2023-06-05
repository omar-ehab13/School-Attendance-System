using SchoolAttendanceSystem.BLL.DTOs.ParentDTOs.Requests;
using SchoolAttendanceSystem.BLL.DTOs.StudentDTOs;
using SchoolAttendanceSystem.BLL.DTOs.StudentDTOs.Requests;
using SchoolAttendanceSystem.DAL.Entities.Auth;
using SchoolAttendanceSystem.DAL.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.Extensions.Profiles
{
    public static class StudentMappingExtension
    {
        public static AddParentRequest ToAddParentRequest(this AddStudentRequest dto)
        {
            return new AddParentRequest
            {
                FullName = dto.ParentName,
                Nid = dto.ParentNid!,
                PhoneNumber = dto.ParentPhone!,
                City = dto.City,
                Governorate = dto.Governorate,
                Address = dto.Address,
                ExternalEmail = dto.ParentEmail,
                Job = dto.ParentJob
            };
        }

        public static Student ToStudetnEnttiy(this AddStudentRequest dto, Parent parent, string imageUrl)
        {
            return new Student
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = dto.Name,
                Age = dto.Age,
                Gender = dto.Gender,
                ParentId = parent.ParentId,
                Parent = parent,
                BirthDate = dto.BirthDate,
                ClassCode = dto.Class,
                ImageUrl = imageUrl
            };
        }

        public static StudentModel ToStudentDTO(this Student student, Parent parent, User user)
        {
            
            return new StudentModel
            {
                Id = student.Id,
                Name = student.FirstName + " " + user.FullName,
                Gender = student.Gender,
                Age = student.Age,
                BirthDate = student.BirthDate,
                Grade = student.Class?.Grade,
                Class = student.ClassCode,
                ParentId = parent.ParentId,
                Address = parent.Address,
                ParentEmail = user.Email,
                ParentExternalEmail = parent.ExternalEmail,
                City = parent.City,
                Governorate = parent.Governorate,
                ParentJob = parent.Job,
                ParentNid = parent.Nid,
                ParentPhone = parent.PhoneNumber,
                ImageUrl = student.ImageUrl,
            };
        }

        public static Student FromUpdateToStudentModel(this UpdateStudentDto dto, Student oldStudent, string imageUrl)
        {
            return new()
            {
                Id = dto.StudentId,
                ParentId = oldStudent.ParentId,
                FirstName = dto.Name,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                ClassCode = dto.Class,
                Age = dto.Age,
                ImageUrl = imageUrl
            };
        }
    }
}
