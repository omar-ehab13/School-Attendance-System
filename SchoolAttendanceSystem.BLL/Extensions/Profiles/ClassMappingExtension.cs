using SchoolAttendanceSystem.BLL.DTOs.ClassDTOs;
using SchoolAttendanceSystem.DAL.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.Extensions.Profiles
{
    public static class ClassMappingExtension
    {
        public static Class ToClassEntity(this ClassModel dto)
        {
            return new Class
            {
                ClassName = dto.Class,
                Grade = dto.Grade,
            };
        }
    }
}
