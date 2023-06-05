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
    public class Teacher
    {
        [Key]
        public string Id { get; set; } = null!;

        public string Specialize { get; set; } = null!; // e,g: Math Teacher

        public string? PhoneNumber { get; set; }

        [MaxLength(100)]
        public string? Governorate { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }

        [ForeignKey("Id")]
        public User User { get; set; } = null!;

        public List<Subject> Subjects { get; set; } = null!;
    }
}
