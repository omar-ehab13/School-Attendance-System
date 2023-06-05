using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.StudentDTOs.Requests
{
    public class UpdateStudentDto
    {
        public string StudentId { get; set; } = null!;
        public string? Name { get; set; }
        public string? Class { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Age { get; set; }
        public IFormFile? Image { get; set; }
    }
}
