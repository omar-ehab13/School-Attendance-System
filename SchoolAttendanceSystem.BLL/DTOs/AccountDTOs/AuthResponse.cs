using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.AccountDTOs
{
    public class AuthResponse<T> : GenericResponse<T>
    {
        public string? Token { get; set; }
    }
}
