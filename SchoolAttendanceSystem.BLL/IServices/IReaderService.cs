using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.IServices
{
    public interface IReaderService
    {
        Task<bool> ReadAsync(string studentId);
    }
}
