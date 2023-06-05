using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.AttendanceDTOs.Response
{
    public class SemesterAttendanceResponse : GenericResponse<object>
    {
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int Total { get; set; }
        public int Present { get; set; }
        public int Absent { get; set; }
        public int Excused { get; set; }
    }
}
