using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.ParentDTOs.Requests
{
    public class UpdateParentDto
    {
        public string ParentId { get; set; } = null!;
        public string? Name { get; set; }
        public string? ExternalEmail { get; set; }
        public string? Job { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Governorate { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public IFormFile? Image { get; set; }
    }
}
