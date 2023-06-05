using Microsoft.EntityFrameworkCore;
using SchoolAttendanceSystem.DAL.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.Entities.Domain
{
    public class Log
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime LogTime { get; set; } = DateTime.Now;

        public string Action { get; set; } = ActionTypes.Out.ToString();

        public string AttendanceStateId { get; set; } = null!;

        [ForeignKey("AttendanceStateId ")]
        public StudentAttendanceState StudentAttendanceState { get; set; } = null!;
    }
}
