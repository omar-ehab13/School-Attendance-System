using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.Constants
{
    public class PeriodsTime
    {
        public static Period FirstPeriod { get; set; } = new() { StartTime = "08:15", EndTime = "09:00" };
        public static Period SecondPeriod { get; set; } = new() { StartTime = "09:00", EndTime = "09:45" };
        public static Period ThirdPeriod { get; set; } = new() { StartTime = "09:45", EndTime = "10:30" };
        public static Period ForthPeriod { get; set; } = new() { StartTime = "10:30", EndTime = "11:15" };
        public static Period FifthPeriod { get; set; } = new() { StartTime = "11:15", EndTime = "12:00" };
        public static Period SixthPeriod { get; set; } = new() { StartTime = "12:30", EndTime = "13:15" };
        public static Period SeventhPeriod { get; set; } = new() { StartTime = "13:15", EndTime = "14:00" };
        public static Period BreakPeriod { get; set; } = new() { StartTime = "12:00", EndTime = "12:30" };
    }

    public class Period
    {
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
    }
}
