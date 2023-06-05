using SchoolAttendanceSystem.BLL.DTOs.ClassDTOs;
using SchoolAttendanceSystem.DAL.Constants;

namespace SchoolAttendanceSystem.BLL.DTOs
{
    public class TimetableResponse<T> : GenericResponse<T>
    {
        public DailyClassTimes ClassTimes { get; set; } = new DailyClassTimes();
    }

    public class DailyClassTimes
    {
        public object FirstPeriodTime { get; set; } = PeriodsTime.FirstPeriod;
        public object SecondPeriodTime { get; set; } = PeriodsTime.SecondPeriod;
        public object ThirdPeriodTime { get; set; } = PeriodsTime.ThirdPeriod;
        public object ForthPeriodTime { get; set; } = PeriodsTime.ForthPeriod;
        public object FifthPeriodTime { get; set; } = PeriodsTime.FifthPeriod;
        public object SixthPeriodTime { get; set; } = PeriodsTime.SixthPeriod;
        public object SeventhPeriodTime { get; set; } = PeriodsTime.SeventhPeriod;
        public object BreakTime { get; set; } = PeriodsTime.BreakPeriod;
    }
}
