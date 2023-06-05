using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.Filters
{
    public class PaginationClassFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Grade { get; set; }

        public PaginationClassFilter()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public PaginationClassFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
