using SchoolAttendanceSystem.BLL.DTOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.AttendanceDTOs.Response
{
    public class StudentAttendanceResponse : GenericResponse<IEnumerable<StudentAttendanceModel>>
    {
        public int Total { get; set; }
        public int Present { get; set; }
        public int Absent { get; set; }
        public int Excused { get; set; }
    }
}
