using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.Entities.Domain
{
    public class Class
    {
        [Required, MaxLength(3)]
        [Key]
        public string ClassName { get; set; } = null!;

        [Required, MaxLength(3)]
        public string Grade { get; set; } = null!;

        public ICollection<Student>? Students { get; set; }
        public List<StudyingDay>? StudyingDays { get; set; }
    }
}
