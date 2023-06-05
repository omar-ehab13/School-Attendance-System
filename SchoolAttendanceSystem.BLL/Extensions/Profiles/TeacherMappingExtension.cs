using SchoolAttendanceSystem.BLL.DTOs.TeacherDTOs;
using SchoolAttendanceSystem.BLL.DTOs.TeacherDTOs.Requests;
using SchoolAttendanceSystem.DAL.Entities.Auth;
using SchoolAttendanceSystem.DAL.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.Extensions.Profiles
{
    public static class TeacherMappingExtension
    {
        public static Teacher FromCreateToTeacherEntity(this CreateTeacherRequest dto, string teacherId)
        {
            return new()
            {
                Id = teacherId,
                Specialize = dto.Specialize,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                City = dto.City,
                Governorate = dto.Governorate
            };
        }

        public static Teacher ToTeacherEntity(this TeacherDto dto) =>
            new()
            {
                Id = dto.Id,
                Specialize = dto.Specialize,
                PhoneNumber = dto.PhoneNumber,
                Governorate = dto.Governorate,
                City = dto.City,
                Address = dto.Address
            };

        public static TeacherDto ToTeacherDto(this Teacher teacher, User user) =>
            new()
            {
                Id = teacher.Id,
                Email = user.Email,
                Name = user.FullName,
                Specialize = teacher.Specialize,
                PhoneNumber = teacher.PhoneNumber,
                Governorate = teacher.Governorate,
                City = teacher.City,
                Address = teacher.Address,
                ImageUrl = user.ImageUrl
            };

        public static Teacher FromUpdateDtoToTeacherEntity(this UpdateTeacherDto dto, Teacher oldTeacher)
        {
            return new()
            {
                Id = dto.TeacherId,
                Governorate = dto.Governorate,
                City = dto.City,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                Specialize = dto.Specialize,
            };
        }
    }
}
