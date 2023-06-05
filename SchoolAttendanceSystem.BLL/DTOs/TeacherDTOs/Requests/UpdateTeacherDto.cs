using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.TeacherDTOs.Requests
{
    public class UpdateTeacherDto
    {
        public string TeacherId { get; set; } = null!;
        public string? Name { get; set; }
        public string? Specialize { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Governorate { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public IFormFile? Image { get; set; }
    }
}
