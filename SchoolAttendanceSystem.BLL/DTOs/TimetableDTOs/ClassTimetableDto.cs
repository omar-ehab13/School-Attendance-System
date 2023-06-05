using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.Timetable
{
    public class ClassTimetableDto
    {
        public DayTimetable? Sunday { get; set; }
        public DayTimetable? Monday { get; set; }
        public DayTimetable? Tuesday { get; set; }
        public DayTimetable? Wednesday { get; set; }
        public DayTimetable? Thursday { get; set; }
    }

    public class DayTimetable
    {
        public string? First { get; set; }
        public string? Second { get; set; }
        public string? Third { get; set; }
        public string? Forth { get; set; }
        public string? Fifth { get; set; }
        public string? Sixth { get; set; }
        public string? Seventh { get; set; }
    }
}
