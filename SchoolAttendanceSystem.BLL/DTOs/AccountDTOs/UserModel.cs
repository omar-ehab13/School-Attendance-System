using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.AccountDTOs
{
    public class UserModel
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Role { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
    }
}
