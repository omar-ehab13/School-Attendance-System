using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.Entities.Domain
{
    public class StudyingDay
    {
        [Key]
        public string DayCode { get; set; } = null!;

        public string ClassName { get; set; } = null!;

        [ForeignKey("ClassName")]
        public Class Class { get; set; } = null!;

        public List<StudyPeriod> Periods { get; set; } = null!;
    }
}
