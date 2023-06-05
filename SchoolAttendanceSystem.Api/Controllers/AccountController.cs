using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SchoolAttendanceSystem.BLL.DTOs.AccountDTOs.Requests;
using SchoolAttendanceSystem.BLL.DTOs.ParentDTOs.Requests;
using SchoolAttendanceSystem.BLL.IServices;

namespace SchoolAttendanceSystem.Api.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("api/account/register")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registerd = await _accountService.RegisterUserAsync(request);

            if (!registerd.Succeeded)
                return BadRequest(registerd.Errors);

            return Ok(registerd);
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _accountService.LoginAsync(request);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpGet("api/account/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var response = await _accountService.GetUserByIdAsync(id);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpDelete("api/account/{id}")]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            var response = await _accountService.DeleteUserAsync(id);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response.Message);
        }

        [HttpPost("api/account/update")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst("uid")?.Value;

            if (userId == null)
                return Forbid();

            var response = await _accountService.UpdateUserAsync(userId, dto);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpPost("api/account/update/image")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> UpdateUserImageAsync([FromForm] IFormFile? image)
        {
            var userId = User.FindFirst("uid")?.Value;

            if (userId == null)
                return Forbid();

            var response = await _accountService.UpdateUserImageAsync(userId, image);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }
    }
}
