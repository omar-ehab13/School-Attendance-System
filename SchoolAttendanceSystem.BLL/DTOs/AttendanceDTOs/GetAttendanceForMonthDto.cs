using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.AttendanceDTOs
{
    public class GetAttendanceForMonthDto
    {
        [Required]
        public string studentId { get; set; } = null!;
        public int month { get; set; }
    }
}
