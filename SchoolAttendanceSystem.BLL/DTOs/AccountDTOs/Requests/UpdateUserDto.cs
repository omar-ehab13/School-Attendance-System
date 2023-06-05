using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.AccountDTOs.Requests
{
    public class UpdateUserDto
    {
        public string FullName { get; set; } = null!;

        public string? PhoneNumber { get; set; }
    }
}
