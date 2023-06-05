using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAttendanceSystem.BLL.DTOs.TeacherDTOs.Requests;
using SchoolAttendanceSystem.BLL.Filters;
using SchoolAttendanceSystem.BLL.IServices;
using SchoolAttendanceSystem.DAL.Constants;

namespace SchoolAttendanceSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IAccountService _accountService;

        public TeachersController(ITeacherService teacherService, IAccountService accountService)
        {
            _teacherService = teacherService;
            _accountService = accountService;
        }

        [HttpPost("add")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateTeacherAsync([FromForm] CreateTeacherRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _teacherService.CreateTeacherAsync(request);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetParents(PaginationTeacherFilter requeset)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _teacherService.GetTeachersAsync(requeset);

                if (!response.Succeeded)
                    return NotFound(response.Errors);

                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> GetParentById(string id)
        {
            var resposne = await _teacherService.GetParentAsync(id);

            if (!resposne.Succeeded)
                return NotFound(resposne.Errors);

            return Ok(resposne);
        }

        [HttpGet("specialize")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> GetAllSpecialize()
        {
            return Ok(TeacherSpechialized.AllSpecialized);
        }

        [HttpPost("update")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UpdateTeacherAsync([FromForm] UpdateTeacherDto dto)
        {
            var response = await _teacherService.UpdateParentAsync(dto);

            if (!response.Succeeded)
                return NotFound(response.Errors);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteTeacherAsync(string id)
        {
            var response = await _teacherService.DeleteTeacherAsync(id);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response.Message);
        }

        [HttpPost("update/image")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UpdateTeacherImge([FromForm] IFormFile image)
        {
            var userId = User?.FindFirst("uid")?.Value;

            if (userId == null)
                return Forbid();

            var response = await _accountService.UpdateUserImageAsync(userId, image);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpGet("{teacherId}/subjects")]
        //[Authorize(Roles ="SuperAdmin,Admin,Teacher")]
        public async Task<IActionResult> GetSubjectsForTeacher(string teacherId)
        {
            var response = await _teacherService.GetSubjectsForTeacher(teacherId);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpPost("set-to-subject")]
        public async Task<IActionResult> SetToSubjectAsync([FromBody] SetTeacherToSubject dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _teacherService.SetTeacherToSubjectAsync(dto);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }
    }
}
