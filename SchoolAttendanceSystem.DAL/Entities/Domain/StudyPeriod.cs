using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.Entities.Domain
{
    public class StudyPeriod
    {
        [Key]
        public string PeriodCode { get; set; } = null!;

        [Required]
        public string PeriodName { get; set; } = null!;

        public string? DayCode { get; set; }

        [ForeignKey("DayCode")]
        public StudyingDay? Day { get; set; }

        public string? SubjectCode { get; set; } 

        [ForeignKey("SubjectCode")]
        public Subject? Subject { get; set; }
    }
}
