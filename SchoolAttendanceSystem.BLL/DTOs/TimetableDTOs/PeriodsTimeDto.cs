using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.TimetableDTOs
{
    public class PeriodsTimeDto
    {
        public int Index { get; set; }
        public string? Name { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
    }
}
