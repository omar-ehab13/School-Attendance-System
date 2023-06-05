using SchoolAttendanceSystem.BLL.DTOs;
using SchoolAttendanceSystem.BLL.DTOs.ClassDTOs;
using SchoolAttendanceSystem.BLL.DTOs.StudentDTOs;
using SchoolAttendanceSystem.BLL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.IServices
{
    public interface IClassService
    {
        Task<GenericResponse<object>> CreateClassAsync(ClassModel dto);
        Task<PagedResponse<IEnumerable<ClassModel>>> GetAllClassesAsync(PaginationClassFilter pagination);
        Task<GenericResponse<List<string>>> GetClassesInsideGrade(string grade);
        Task<GenericResponse<ClassModel>> UpdateClassAsync(UpdateClass dto);
        Task<GenericResponse<object>> DeleteClassAsync(string className);
        Task<GenericResponse<IEnumerable<StudentModel>>> GetStudentsInsideClassAsync(string className);
    }
}
