using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAttendanceSystem.BLL.IServices;

namespace SchoolAttendanceSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly IReaderService _reader;

        public ReaderController(IReaderService reader)
        {
            _reader = reader;
        }

        [HttpPost]
        [Route("read/{studentId}")]
        public async Task<IActionResult> ReadAsync(string studentId)
        {
            if (await _reader.ReadAsync(studentId))
                return Ok("Readed");

            return BadRequest("Invalid Id");
        }
    }
}
