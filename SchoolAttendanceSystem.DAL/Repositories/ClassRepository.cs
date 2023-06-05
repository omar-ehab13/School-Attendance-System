using SchoolAttendanceSystem.DAL.Entities.Domain;
using SchoolAttendanceSystem.DAL.IRepositories;
using SchoolAttendanceSystem.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SchoolAttendanceSystem.DAL.Repositories
{
    public class ClassRepository : GenericRepository<Class>, IClassRepository
    {
        public ClassRepository(ApplicationDbContext context) : base(context) {  }

        public async Task<IEnumerable<Class>> GetAllPaginatedAsync(int page, int pageSize)
        {
            // Calculate the number of items to skip
            int itemsToSkip = (page - 1) * pageSize;

            List<Class> classes = await _context.Classes
                .OrderBy(c => c.ClassName)
                .Skip(itemsToSkip)
                .Take(pageSize)
                .ToListAsync();

            return classes;
        }

        public async Task<IEnumerable<Class>> GetAllPaginatedAsync(int page, int pageSize, string grade)
        {
            // Calculate the number of items to skip
            int itemsToSkip = (page - 1) * pageSize;

            List<Class> classes = await _context.Classes
                .Where(c => c.Grade == grade)
                .OrderBy(c => c.ClassName)
                .Skip(itemsToSkip)
                .Take(pageSize)
                .ToListAsync();

            return classes;
        }

        public async Task<IEnumerable<Class>> GetClassesByGradeAsync(string grade)
        {
            return await _context.Classes
                .Where(c => c.Grade == grade)
                .ToListAsync();
        }
    }
}
