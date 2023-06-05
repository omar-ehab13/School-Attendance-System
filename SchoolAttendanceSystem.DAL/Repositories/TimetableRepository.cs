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
    public class TimetableRepository : ITimetableRepository
    {
        private readonly ApplicationDbContext _context;

        public TimetableRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StudyingDay>? GetDaysForClassAsync(string className, string firstSubDayName)
        {
            return await _context.StudyingDays
                .Where(d => d.ClassName == className)
                .Where(d => d.DayCode.Contains(firstSubDayName.ToUpper()))
                .Include(d => d.Periods)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Subject>> GetSubjectsForClassAsync(string className)
        {
            return await _context.Subjects
                .Where(s => s.ClassName == className)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudyPeriod>> GetPeriodsForSubject(Subject subject)
        {
            return await _context.StudyPeriods.Where(p => p.SubjectCode == subject.SubjectCode).ToListAsync();
        }
    }
}
