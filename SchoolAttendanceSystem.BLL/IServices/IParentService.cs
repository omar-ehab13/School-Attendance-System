using SchoolAttendanceSystem.BLL.DTOs;
using SchoolAttendanceSystem.BLL.DTOs.ParentDTOs;
using SchoolAttendanceSystem.BLL.DTOs.ParentDTOs.Requests;
using SchoolAttendanceSystem.BLL.DTOs.ParentDTOs.Responses;

namespace SchoolAttendanceSystem.BLL.IServices
{
    public interface IParentService
    {
        Task<GenericResponse<ParentModel>> AddParentAsync(AddParentRequest request);
        Task<GenericResponse<ParentModel>> GetParentAsync(string parentId);
        Task<PagedResponse<List<ParentModel>>> GetParentsAsync(int pageNumber, int pageSize, string name);
        Task<GenericResponse<ParentModel>> UpdateParentAsync(UpdateParentDto dto);
        Task<GenericResponse<object>> DeleteParentAsync(string parentId);
    }
}
