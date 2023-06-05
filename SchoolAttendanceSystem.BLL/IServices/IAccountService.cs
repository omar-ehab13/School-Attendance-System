using Microsoft.AspNetCore.Http;
using SchoolAttendanceSystem.BLL.DTOs;
using SchoolAttendanceSystem.BLL.DTOs.AccountDTOs;
using SchoolAttendanceSystem.BLL.DTOs.AccountDTOs.Requests;

namespace SchoolAttendanceSystem.BLL.IServices
{
    public interface IAccountService
    {
        Task<AuthResponse<object>> LoginAsync(LoginRequest request);
        Task<GenericResponse<UserModel>> RegisterUserAsync(RegisterUserRequest request);
        Task<GenericResponse<UserModel>> GetUserByIdAsync(string id);
        Task<GenericResponse<object>> DeleteUserAsync(string id);
        Task<GenericResponse<UserModel>> UpdateUserAsync(string id, UpdateUserDto dto);
        Task<GenericResponse<UserModel>> UpdateUserImageAsync(string id, IFormFile userImage);
    }
}
