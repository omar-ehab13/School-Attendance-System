using SchoolAttendanceSystem.BLL.DTOs;
using SchoolAttendanceSystem.BLL.DTOs.SubjectDTOs;
using SchoolAttendanceSystem.BLL.DTOs.TeacherDTOs;
using SchoolAttendanceSystem.BLL.DTOs.TeacherDTOs.Requests;
using SchoolAttendanceSystem.BLL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.IServices
{
    public interface ITeacherService
    {
        Task<GenericResponse<TeacherDto>> CreateTeacherAsync(CreateTeacherRequest request);
        Task<PagedResponse<List<TeacherDto>>> GetTeachersAsync(PaginationTeacherFilter request);
        Task<GenericResponse<TeacherDto>> GetParentAsync(string id);
        Task<GenericResponse<TeacherDto>> UpdateParentAsync(UpdateTeacherDto dto);
        Task<GenericResponse<object>> DeleteTeacherAsync(string id);
        Task<GenericResponse<IEnumerable<SubjectDto>>> GetSubjectsForTeacher(string id);
        Task<GenericResponse<IEnumerable<SubjectDto>>> SetTeacherToSubjectAsync(SetTeacherToSubject dto);
    }
}
