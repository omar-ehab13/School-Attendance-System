using Microsoft.AspNetCore.Http;
using SchoolAttendanceSystem.BLL.DTOs;
using SchoolAttendanceSystem.BLL.DTOs.StudentDTOs;
using SchoolAttendanceSystem.BLL.DTOs.StudentDTOs.Requests;
using SchoolAttendanceSystem.BLL.DTOs.StudentDTOs.Responses;
using SchoolAttendanceSystem.BLL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.IServices
{
    public interface IStudentService
    {
        Task<GenericResponse<object>> AddStdudentAsync(AddStudentRequest request);
        Task<PagedResponse<IEnumerable<StudentModel>>> GetStudentsAsync(PaginationStudentsFilter pagination);
        Task<GenericResponse<StudentModel>> GetStudentByIdAsync(string id);
        Task<GenericResponse<StudentModel>> UpdateStudentAsync(UpdateStudentDto dto);
        Task<GenericResponse<object>> DeleteStudentAsync(string id);
        Task<GenericResponse<object>> UpdateUserImageAsync(UpdateStudentImageDto dto);
    }
}
