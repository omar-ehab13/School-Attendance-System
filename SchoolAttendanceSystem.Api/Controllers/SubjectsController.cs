using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAttendanceSystem.BLL.DTOs.SubjectDTOs;
using SchoolAttendanceSystem.BLL.Filters;
using SchoolAttendanceSystem.BLL.IServices;

namespace SchoolAttendanceSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateSubjectAsync([FromBody] CreateSubjectDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _subjectService.CreateSubjectAsync(dto);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpGet("{subjectCode}")]
        public async Task<IActionResult> GetSubjectAsync(string subjectCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _subjectService.GetSubjectAsync(subjectCode);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        //[HttpPost("update")]
        //public async Task<IActionResult> UpdaetSubject([FromBody] UpdateSubjectDto dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var response = await _subjectService.UpdateSubjectAsync(dto);

        //    if (!response.Succeeded)
        //        return BadRequest(response.Errors);

        //    return Ok(response);
        //}

        [HttpDelete("{subjectCode}")]
        public async Task<IActionResult> DeleteSubjectAsync(string subjectCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _subjectService.DeleteSubjectAsync(subjectCode);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpGet("names")]
        public async Task<IActionResult> GetSubjectsNames()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _subjectService.GetSbjectsNames();

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetAllSubjects([FromBody] PaginationSubjectFilter pagination)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _subjectService.GetPaginatedSubjects(pagination);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(response);
        }
    }
}
