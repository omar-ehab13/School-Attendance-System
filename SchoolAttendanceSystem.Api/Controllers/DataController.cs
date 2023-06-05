using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAttendanceSystem.DAL.Data;

namespace SchoolAttendanceSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly DemoData _data;
        private readonly TimetableData _timetableData;

        public DataController(DemoData data, TimetableData timetableData)
        {
            _data = data;
            _timetableData = timetableData;
        }

        [HttpPost("attend/{studentId}")]
        public async Task<IActionResult> GenerateDemoDataAsync(string studentId)
        {
            if (!await _data.GenerateAsync(studentId))
                return BadRequest("This student is already has demo data");

            return Ok("Generated successfully");
        }

        [HttpPost("timetable/classTemp/{className}")]
        public async Task<IActionResult> GenereateTempTimetable(string className)
        {
            if (string.IsNullOrEmpty(className))
                return BadRequest();

            if (!await _timetableData.GenerateClassTimetableTemp(className))
                return BadRequest("Cannot create template");

            return Ok("created");
        }

        [HttpPost("timetable/subjects/{className}")]
        public async Task<IActionResult> GenereateSubjectsForClassAsync(string className)
        {
            var subjects = await _timetableData.GenerateSubjectsAsync(className);

            if (subjects == null)
                return BadRequest("Cannot add subjects");

            return Ok("created");
        }

        //[HttpGet("subjects/all")]
        //public async Task<IActionResult> GetAllSubjects()
        //{
        //    return Ok(await _data.GetSubjectsAsync());
        //}
    }
}
