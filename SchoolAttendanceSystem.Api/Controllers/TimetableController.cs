using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAttendanceSystem.BLL.IServices;

namespace SchoolAttendanceSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : ControllerBase
    {
        private readonly ITimetableService _timetableService;

        public TimetableController(ITimetableService timetableService)
        {
            _timetableService = timetableService;
        }

        [HttpGet("class/{className}")]
        public async Task<IActionResult> GetClassTimetableAsync(string className)
        {
            var response = await _timetableService.GetTimetableForClassAsync(className);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpGet("classes-times")]
        public async Task<IActionResult> GetClassTimes()
        {
            var response = await _timetableService.GetClassesTimesAsync();

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpGet("teacher/{teacherId}")]
        //[Authorize(Roles = "SuperAdmi,Admin,Teacher")]
        public async Task<IActionResult> GetTimetableForTeacher(string teacherId)
        {
            var response = await _timetableService.GetTiemtableForTeacher(teacherId);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }
    }
}
