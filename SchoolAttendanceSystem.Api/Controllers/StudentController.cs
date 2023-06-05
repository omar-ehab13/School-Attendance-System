using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAttendanceSystem.BLL.DTOs.StudentDTOs.Requests;
using SchoolAttendanceSystem.BLL.Filters;
using SchoolAttendanceSystem.BLL.IServices;

namespace SchoolAttendanceSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> AddStudentAsync([FromForm] AddStudentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _studentService.AddStdudentAsync(request);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpPost("")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> GetStudents(PaginationStudentsFilter request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _studentService.GetStudentsAsync(request);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin,Parent")]
        public async Task<IActionResult> GetStudentAsync(string id)
        {
            var response = await _studentService.GetStudentByIdAsync(id);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpPost("update")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UpdateStudentAsync([FromForm] UpdateStudentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _studentService.UpdateStudentAsync(dto);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteStudentAsync(string id)
        {
            var response = await _studentService.DeleteStudentAsync(id);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response.Message);
        }

        [HttpPost("update/image")]
        [Authorize(Roles = "SuperAdmin,Parent")]
        public async Task<IActionResult> UpdateStudentImage([FromForm] UpdateStudentImageDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _studentService.UpdateUserImageAsync(dto);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }
    }
}
