using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs
{
    public class PagedResponse<T> : GenericResponse<T>
    {
        public int PageNumber { get;set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        //public int TotalPages { get; set; }

        public PagedResponse()
        {
            PageNumber = 1;
            PageSize = 10;
            TotalRecords = 0;
        }

        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalRecords / PageSize);
            }
        }
    }
}
