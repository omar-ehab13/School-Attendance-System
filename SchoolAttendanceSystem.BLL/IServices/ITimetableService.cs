using SchoolAttendanceSystem.BLL.DTOs;
using SchoolAttendanceSystem.BLL.DTOs.Timetable;
using SchoolAttendanceSystem.BLL.DTOs.TimetableDTOs;

namespace SchoolAttendanceSystem.BLL.IServices
{
    public interface ITimetableService
    {
        Task<GenericResponse<ClassTimetableDto>> GetTimetableForClassAsync(string className);
        Task<GenericResponse<object>> GetClassesTimesAsync();
        Task<GenericResponse<IEnumerable<TeacherTimetableDto>>> GetTiemtableForTeacher(string teacherId);
    }
}
