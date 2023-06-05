using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.Entities.Domain
{
    public class Student
    {
        [Key]
        public string Id { get; set; } = null!;

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = null!;

        public string? Gender { get; set; }

        public int? Age { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? ImageUrl { get; set; }

        public ICollection<StudentAttendanceState>? AttendanceStates { get; set; }

        #region Relation with parent
        [Required]
        public string ParentId { get; set; } = null!;

        [ForeignKey("ParentId")]
        public Parent? Parent { get; set; }
        #endregion

        #region Relation with class
        public string? ClassCode { get; set; }

        [ForeignKey("ClassCode")]
        public Class? Class { get; set; }
        #endregion
    }
}
