using SchoolAttendanceSystem.DAL.Entities.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.Entities.Domain
{
    public class Parent
    {
        [Key]
        public string ParentId { get; set; } = null!;

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

        [ForeignKey("ParentId")]
        public User User { get; set; } = null!;

        public ICollection<Student>? Students { get; set; }
    }
}
