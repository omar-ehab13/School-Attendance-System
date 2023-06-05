using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.Filters
{
    public class PaginationStudentsFilter : PaginationFilter
    {
        public string? Class { get; set; }
        public string? Grade { get; set; }
    }
}
