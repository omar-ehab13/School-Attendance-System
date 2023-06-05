using Microsoft.EntityFrameworkCore;
using SchoolAttendanceSystem.DAL.Data;
using SchoolAttendanceSystem.DAL.Entities.Domain;
using SchoolAttendanceSystem.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.Repositories
{
    public class LogRepository : GenericRepository<Log>, ILogRepository
    {
        public LogRepository(ApplicationDbContext context) : base(context)
        {

        }

        // TODO: Update this query to optimized one 
        public async Task<Log>? GetLastLogForStudent(StudentAttendanceState attend)
        {
            var lastLog = await _context.Logs
                .Where(l => l.AttendanceStateId == attend.Id)
                .OrderByDescending(l => l.LogTime)
                .FirstOrDefaultAsync();

            if (lastLog == null)
                Console.WriteLine("last log is null");

            return lastLog;
        }

        public bool UpdateAttendanceState(StudentAttendanceState attend)
        {
            try
            {
                _context.Update(attend);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
