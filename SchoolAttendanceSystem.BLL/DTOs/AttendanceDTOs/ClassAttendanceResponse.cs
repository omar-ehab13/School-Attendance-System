using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.AttendanceDTOs
{
    public class ClassAttendanceResponse : GenericResponse<List<StudentAttendnace>>
    {
        public int TotalStudents { get; set; }
        public int Present { get; set; }
        public int Absent { get; set; }
        public int Excused { get; set; }
    }

    public class StudentAttendnace
    {
        public string? Name { get; set; }
        public string? Status { get; set; }
    }
}
