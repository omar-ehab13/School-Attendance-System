using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SchoolAttendanceSystem.BLL.DTOs.ParentDTOs.Requests
{
    public class AddParentRequest
    {
        [Required, MaxLength(50)]
        public string FullName { get; set; } = null!;

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

        public IFormFile? Image { get; set; }
    }
}
