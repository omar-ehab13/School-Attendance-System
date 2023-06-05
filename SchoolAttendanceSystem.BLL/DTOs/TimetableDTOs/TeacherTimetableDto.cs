using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.TimetableDTOs
{
    public class TeacherTimetableDto
    {
        public string Day { get; set; } = null!;
        [Required]
        public int PeriodNo { get; set; }
        public string Period { get; set; } = null!;
        public string SubjectCode { get; set; } = null!;
        public string SubjectName { get; set; } = null!;
    }
}
