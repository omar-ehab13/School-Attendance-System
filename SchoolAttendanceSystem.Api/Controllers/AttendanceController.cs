using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAttendanceSystem.BLL.DTOs.AttendanceDTOs;
using SchoolAttendanceSystem.BLL.IServices;

namespace SchoolAttendanceSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAttendanceReport()
        {
            if (await _attendanceService.CreateDefaultAttendanceReport())
                return Ok("Created");

            return BadRequest("Not Created");
        }

        [HttpPost("students/month")]
        public async Task<IActionResult> GetByMonth(GetAttendanceForMonthDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _attendanceService.GetStudentAttendanceStateForMonth(request);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpGet("students/semester/{studentId}")]
        public async Task<IActionResult> GetStudentStatForSemester(string studentId)
        {
            var response = await _attendanceService.GetStudentAttendanceStateForSemester(studentId);

            if (!response.Succeeded)
                return NotFound(response.Errors);

            return Ok(response);
        }

        [HttpGet("class/today/{className}")]
        public async Task<IActionResult> GetTodayClassAttendance(string className)
        {
            var response = await _attendanceService.GetClassAttendnaceForDay(className, new DateTime(2022,11,16));

            return Ok(response);
        }
    }
}
