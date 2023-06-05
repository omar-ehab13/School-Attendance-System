using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAttendanceSystem.BLL.DTOs.ClassDTOs;
using SchoolAttendanceSystem.BLL.Filters;
using SchoolAttendanceSystem.BLL.IServices;

namespace SchoolAttendanceSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateNewClass([FromBody] ClassModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _classService.CreateClassAsync(model);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> GetClasses([FromBody] PaginationClassFilter pagination)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _classService.GetAllClassesAsync(pagination);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpGet]
        [Route("{grade}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> GetClassesInsideGrade(string grade)
        {
            var response = await _classService.GetClassesInsideGrade(grade);

            if (!response.Succeeded)
                return NotFound(response.Errors);

            return Ok(response);
        }

        [HttpPost("update")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UpdateClassAsync([FromBody] UpdateClass classModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _classService.UpdateClassAsync(classModel);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpDelete("{className}")]
        public async Task<IActionResult> DeleteClassAsync(string className)
        {
            var response = await _classService.DeleteClassAsync(className);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response.Message);
        }

        [HttpGet("{className}/students")]
        [Authorize(Roles = "Admin,SuperAdmin,Teacher")]
        public async Task<IActionResult> GetAllStudentsInsideClassAsync(string className)
        {
            var response = await _classService.GetStudentsInsideClassAsync(className);

            if (!response.Succeeded)
                return NotFound(response.Errors);

            return Ok(response);
        }
    }
}
