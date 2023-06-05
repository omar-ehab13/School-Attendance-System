using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.TeacherDTOs.Requests
{
    public class SetTeacherToSubject
    {
        public string TeacherId { get; set; } = null!;

        public string SubjectCode { get; set; } = null!;
    }
}
