using SchoolAttendanceSystem.DAL.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.TeacherDTOs
{
    public class TeacherDto
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [MaxLength(100)]
        public string? Governorate { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [Required]
        public string Specialize { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? ImageUrl { get; set; } = DefaultUrls.DefaultUserImage;
    }
}
