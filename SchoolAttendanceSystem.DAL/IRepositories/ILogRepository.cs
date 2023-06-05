using SchoolAttendanceSystem.DAL.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.IRepositories
{
    public interface ILogRepository : IGenericRepository<Log>
    {
        Task<Log>? GetLastLogForStudent(StudentAttendanceState attend);
        bool UpdateAttendanceState(StudentAttendanceState attend);
    }
}
