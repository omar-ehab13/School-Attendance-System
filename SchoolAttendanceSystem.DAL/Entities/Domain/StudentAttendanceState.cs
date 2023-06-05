using SchoolAttendanceSystem.DAL.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAttendanceSystem.DAL.Entities.Domain
{
    public class StudentAttendanceState
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string StudentId { get; set; } = null!;

        [Column(TypeName = "Date")]
        public DateTime DateOfDay { get; set; } = DateTime.Now;

        [Required]
        public string Status { get; set; } = StatusTypes.Absent.ToString();

        [Required]
        [ForeignKey("StudentId")]
        public Student Student { get; set; } = null!;

        public ICollection<Log>? Logs { get; set; }
    }
}
