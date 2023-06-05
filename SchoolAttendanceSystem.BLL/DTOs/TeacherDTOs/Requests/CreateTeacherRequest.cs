using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.TeacherDTOs.Requests
{
    public class CreateTeacherRequest
    {
        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string Specialize { get; set; } = null!;

        public string? PhoneNumber { get; set; } = null!;
        public string? Governorate { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; } = null!;
        public IFormFile? Image { get; set; }
    }
}
