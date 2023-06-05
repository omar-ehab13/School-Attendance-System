using SchoolAttendanceSystem.BLL.DTOs;
using SchoolAttendanceSystem.BLL.DTOs.AttendanceDTOs;
using SchoolAttendanceSystem.BLL.DTOs.AttendanceDTOs.Response;
using SchoolAttendanceSystem.BLL.DTOs.Models;

namespace SchoolAttendanceSystem.BLL.IServices
{
    public interface IAttendanceService
    {
        Task<bool> CreateDefaultAttendanceReport();
        Task<GenericResponse<StudentAttendanceModel>> GetStudentAttendance(string studentId, DateTime day);
        Task<StudentAttendanceResponse> GetStudentAttendanceStateForMonth(GetAttendanceForMonthDto dto);
        Task<SemesterAttendanceResponse> GetStudentAttendanceStateForSemester(string studentId);
        Task<ClassAttendanceResponse> GetClassAttendnaceForDay(string className, DateTime day);
    }
}
