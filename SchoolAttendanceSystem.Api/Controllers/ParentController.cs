using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAttendanceSystem.BLL.DTOs.ParentDTOs.Requests;
using SchoolAttendanceSystem.BLL.Filters;
using SchoolAttendanceSystem.BLL.IServices;
using System.Security.Claims;

namespace SchoolAttendanceSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentsController : ControllerBase
    {
        private readonly IParentService _parentService;
        private readonly IAccountService _accountService;
        
        public ParentsController(IParentService parentService, IAccountService accountService)
        {
            _parentService = parentService;
            _accountService = accountService;
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> RegisterParent([FromForm] AddParentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registerd = await _parentService.AddParentAsync(request);

            if (!registerd.Succeeded)
                return BadRequest(registerd.Errors);

            return Ok(registerd);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Parent,Admin,SuperAdmin")]
        public async Task<IActionResult> GetParentAsync(string id)
        {
            try
            {
                if (id == null)
                    return BadRequest(ModelState);

                // check if the user is parent and correct parent to get correct info
                string? role = User.FindFirst(ClaimTypes.Role)?.Value;

                if (role == "Parent")
                {
                    //var userId = User.FindFirst("id")?.Value;
                    var userId = User.FindFirst("uid")?.Value;

                    if (userId != id)
                        return Forbid("Cannot access");
                }

                var response = await _parentService.GetParentAsync(id);

                if (!response.Succeeded)
                    return NotFound(response.Errors);

                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> GetParents([FromBody] PaginationFilter paginationFilter)
        {
            var response = await _parentService.GetParentsAsync(
                paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.Name);

            if (!response.Succeeded)
                return NotFound(response.Message);

            return Ok(response);
        }

        [HttpPost("update")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UpdateParentAsync([FromForm] UpdateParentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _parentService.UpdateParentAsync(dto);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DelteParentAsync(string id)
        {
            var respone = await _parentService.DeleteParentAsync(id);

            if (!respone.Succeeded)
                return BadRequest(respone.Errors);

            return Ok(respone.Message);
        }

        [HttpPost("update/image")]
        [Authorize(Roles = "Parent")]
        public async Task<IActionResult> UpdateParentImageAsync([FromForm] IFormFile? image)
        {
            var userId = User?.FindFirst("uid")?.Value;

            if (userId == null)
                return Forbid();

            var response = await _accountService.UpdateUserImageAsync(userId, image);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }
    }
}
