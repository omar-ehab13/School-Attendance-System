using SchoolAttendanceSystem.BLL.DTOs.ParentDTOs;
using SchoolAttendanceSystem.BLL.DTOs.ParentDTOs.Requests;
using SchoolAttendanceSystem.BLL.DTOs.ParentDTOs.Responses;
using SchoolAttendanceSystem.DAL.Entities.Auth;
using SchoolAttendanceSystem.DAL.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.Extensions.Profiles
{
    public static class ParentMappingExtension
    {
        public static Parent ToParentEntity(this AddParentRequest dto, User user)
        {
            return new Parent
            {
                ParentId = user.Id,
                User = user,
                City = dto.City,
                Governorate = dto.Governorate,
                Address = dto.Address,
                Job = dto.Job,
                ExternalEmail = dto.ExternalEmail,
                PhoneNumber = dto.PhoneNumber,
                Nid = dto.Nid
            };
        }

        public static ParentModel ToParentModel(this Parent parent, User user, List<ChildrenDto>? students = null)
        {
            return new ParentModel
            {
                ParentId = parent.ParentId,
                Nid = parent.Nid,
                ParentName = user.FullName,
                Email = user.Email,
                ExternalEmail = parent.ExternalEmail,
                City = parent.City,
                Governorate = parent.Governorate,
                Address = parent.Address,
                PhoneNumber = parent.PhoneNumber,
                Job = parent.Job,
                ImageUrl = user.ImageUrl,
                Students = students
            };
        }

        public static Parent FromUpdateToParentEntity(this UpdateParentDto dto, Parent oldParent)
        {
            return new()
            {
                ParentId = oldParent.ParentId,
                Nid = oldParent.Nid,
                Job = dto.Job,
                ExternalEmail = dto.ExternalEmail,
                PhoneNumber = dto.PhoneNumber,
                Governorate = dto.Governorate,
                City = dto.City,
                Address = dto.Address
            };
        }
    }
}
