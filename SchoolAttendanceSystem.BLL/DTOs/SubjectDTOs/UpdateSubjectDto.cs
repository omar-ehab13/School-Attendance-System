using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.SubjectDTOs
{
    public class UpdateSubjectDto
    {
        public string SubjectCode { get; set; } = null!;
        public string SubjectName { get; set; } = null!;
        public string Class { get; set; } = null!;
    }
}
