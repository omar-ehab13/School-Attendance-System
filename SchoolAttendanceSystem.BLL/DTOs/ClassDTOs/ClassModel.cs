using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.ClassDTOs
{
    public class ClassModel
    {
        [Required, MaxLength(3)]
        public string Class { get; set; } = null!;

        [Required, MaxLength(3)]
        public string Grade { get; set; } = null!;
    }
}
