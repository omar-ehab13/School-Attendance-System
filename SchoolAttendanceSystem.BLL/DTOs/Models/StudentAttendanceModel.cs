using SchoolAttendanceSystem.BLL.DTOs.StudentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.Models
{
    public class StudentAttendanceModel
    {
        public string Day { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int? NumberOfLogs { get; set; }
    }
}
