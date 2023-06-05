using SchoolAttendanceSystem.BLL.DTOs.Models;
using SchoolAttendanceSystem.DAL.Entities.Domain;

namespace SchoolAttendanceSystem.BLL.Extensions.Profiles
{
    public static class AttendanceMappingExtension
    {
        public static StudentAttendanceModel ToStudentAttendanceModel(this StudentAttendanceState attendance)
        {
            return new StudentAttendanceModel
            {
                Day = attendance.DateOfDay.ToShortDateString().ToString(),
                Status = attendance.Status
            };
        }
    }
}
