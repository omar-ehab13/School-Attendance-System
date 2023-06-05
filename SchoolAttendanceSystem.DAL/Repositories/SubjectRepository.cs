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
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Subject>? GetSubjectIncludeTeacher(string subjectCode)
            => await _context.Subjects
                .Where(s => s.SubjectCode == subjectCode)
                .Include(s => s.Teacher)
                    .ThenInclude(t => t.User)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<Subject>> GetSubjectsInsideClass(string className)
            => await _context.Subjects
                    .Where(s => s.ClassName == className)
                    .Include(s => s.Teacher)
                        .ThenInclude(t => t.User)
                    .ToListAsync();

        public async Task<IEnumerable<Subject>> GetSubjectsInsideGrade(string grade)
            => await _context.Subjects
                    .Where(s => s.ClassName.Contains(grade))
                    .Include(s => s.Teacher)
                        .ThenInclude(t => t.User)
                    .ToListAsync();

        public async Task<IEnumerable<Subject>> GetAllSubjectsIncludeTeacher()
            => await _context.Subjects
                .Include(s => s.Teacher)
                    .ThenInclude(t => t.User)
                .ToListAsync();

    }
}
