using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs
{
    public class GenericResponse<T>
    {
        public bool Succeeded { get; set; } = false;
        public ushort Status { get; set; } = 400; // Bad Request
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }
    }
}
