using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.ClassDTOs
{
    public class UpdateClass
    {
        public string OldClassName { get; set; } = null!;

        public string OldGrade { get; set; } = null!;

        public string NewClassName { get; set; } = null!;

        public string NewGrade { get; set; } = null!;
    }
}
