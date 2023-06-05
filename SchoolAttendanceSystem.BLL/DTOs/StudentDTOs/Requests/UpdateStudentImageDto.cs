using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.StudentDTOs.Requests
{
    public class UpdateStudentImageDto
    {
        public string StudentId { get; set; } = null!;

        public IFormFile? Image { get; set; }
    }
}
