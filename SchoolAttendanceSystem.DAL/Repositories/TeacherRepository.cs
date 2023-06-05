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
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<IEnumerable<Subject>> GetAllSubejects(string teacherId)
        {
            var teacher = await _context.Teachers
                    .Include(t => t.Subjects)
                    .Where(t => t.Id == teacherId)
                    .FirstOrDefaultAsync();

            return teacher.Subjects;
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync() => 
            await _context.Teachers
            .Include(t => t.User)
            .ToListAsync();

        public async Task<Teacher> GetTeacherById(string id)
            => await _context.Teachers
                .Include(t => t.User)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
    }
}
