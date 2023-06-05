using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.AccountDTOs.Requests
{
    public class RegisterUserRequest
    {
        [Required, MaxLength(50)]
        public string FullName { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;

        public IFormFile? Image { get; set; }
    }
}
