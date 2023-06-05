using SchoolAttendanceSystem.BLL.DTOs;
using SchoolAttendanceSystem.BLL.DTOs.SubjectDTOs;
using SchoolAttendanceSystem.BLL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.IServices
{
    public interface ISubjectService
    {
        Task<GenericResponse<SubjectDto>> CreateSubjectAsync(CreateSubjectDto dto);
        Task<GenericResponse<SubjectDto>> GetSubjectAsync(string subjectCode);
        Task<GenericResponse<SubjectDto>> UpdateSubjectAsync(UpdateSubjectDto dto);
        Task<GenericResponse<object>> DeleteSubjectAsync(string subjectCode);
        Task<GenericResponse<IEnumerable<SubjectDto>>> GetSubjectsForClass(string className);
        Task<GenericResponse<IEnumerable<string>>> GetSbjectsNames();
        Task<PagedResponse<IEnumerable<SubjectDto>>> GetPaginatedSubjects(PaginationSubjectFilter pagination);
    }
}
