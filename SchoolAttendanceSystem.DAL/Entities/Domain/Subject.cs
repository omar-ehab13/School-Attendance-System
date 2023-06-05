using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.Entities.Domain
{
    public class Subject
    {
        [Key]
        public string SubjectCode { get; set; } = null!;

        [Required]
        public string SubjcetName { get; set; } = null!;

        public string? ClassName { get; set; }

        [ForeignKey("ClassName")]
        public Class? Class { get; set; }

        [AllowNull]
        public string? TeacherId { get; set; }

        [AllowNull]
        public Teacher? Teacher { get; set; }

        public List<StudyPeriod>? Periods { get; set; }
    }
}
