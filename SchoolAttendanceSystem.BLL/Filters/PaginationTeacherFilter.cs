using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.Filters
{
    public class PaginationTeacherFilter : PaginationFilter
    {
        public string? Specialize { get; set; }
    }
}
