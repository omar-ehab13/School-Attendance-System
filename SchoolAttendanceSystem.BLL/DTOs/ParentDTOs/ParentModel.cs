using SchoolAttendanceSystem.DAL.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.ParentDTOs
{
    public class ParentModel
    {
        [Required]
        public string ParentId { get; set; } = null!;

        [Required]
        public string ParentName { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required, MaxLength(20)]
        public string Nid { get; set; } = null!;

        [MaxLength(100)]
        public string? Governorate { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }

        [MaxLength(100)]
        public string? Job { get; set; }

        [EmailAddress]
        public string? ExternalEmail { get; set; }

        [Required, MaxLength(20)]
        public string PhoneNumber { get; set; } = null!;

        public string? ImageUrl { get; set; } = DefaultUrls.DefaultUserImage;

        public List<ChildrenDto>? Students { get; set; }
    }

    public class ChildrenDto
    {
        public string Name { get; set; }
        public string StudentId { get; set; }
    }
}
