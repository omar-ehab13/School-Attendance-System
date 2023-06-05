using SchoolAttendanceSystem.BLL.DTOs.AccountDTOs;
using SchoolAttendanceSystem.BLL.DTOs.AccountDTOs.Requests;
using SchoolAttendanceSystem.DAL.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.Extensions.Profiles
{
    public static class AccountMappingExtension
    {
        public static User ToUserEntity(this RegisterUserRequest dto, string email, string? imageUrl)
        {
            return new User
            {
                FullName = dto.FullName,
                Email = email,
                UserName = email,
                ImageUrl = imageUrl
            };
        }

        public static UserModel ToUserModel(this User user, string role)
        {
            return new()
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = role,
                PhoneNumber = user.PhoneNumber,
                ImageUrl = user.ImageUrl
            };
        }

        public static User FromUpdateToUserEntity(this UpdateUserDto dto, User oldUser)
        {
            return new()
            {
                Id = oldUser.Id,
                Email = oldUser.Email,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                ImageUrl = oldUser.ImageUrl
            };
        }
    }
}
