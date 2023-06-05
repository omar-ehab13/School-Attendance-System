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
    public class ParentRepository : GenericRepository<Parent>, IParentRepository
    {
        public ParentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Parent?> FindParentByNid(string nid) =>
            await _context.Parents.Select(p => p)
            .Where(p => p.Nid == nid)
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<Student>> GetStudentsAsync(Parent parent)
        {
            var _parent = await _context.Parents
                .Include(p => p.Students)
                .FirstOrDefaultAsync(p => p.ParentId == parent.ParentId);

            return _parent.Students;
        }

        public async Task<IEnumerable<Parent>> GetPaginatedParents(int pageNumber, int pageSize)
        {
            // Calculate the number of items to skip
            int itemsToSkip = (pageNumber - 1) * pageSize;

            // Retrieve the parent records from the database, skipping the appropriate number of items
            List<Parent> parents = await _context.Parents
                .Include(p => p.User)
                .OrderBy(p => p.User.UserName)
                .Skip(itemsToSkip)
                .Take(pageSize)
                .ToListAsync();

            return parents;
        }

        public async Task<IEnumerable<Parent>> GetPaginatedParents(int pageNumber, int pageSize, string name)
        {
            // Calculate the number of items to skip
            int itemsToSkip = (pageNumber - 1) * pageSize;

            // Retrieve the parent records from the database, skipping the appropriate number of items
            List<Parent> parents = await _context.Parents
                .Include(p => p.User)
                .Where(p => p.User.FullName.ToLower().Contains(name.ToLower()))
                .OrderBy(p => p.User.UserName)
                .Skip(itemsToSkip)
                .Take(pageSize)
                .ToListAsync();

            return parents;
        }

        public async Task<IEnumerable<Parent>> GetParentsByName(string name)
        {
            List<Parent> parents = await _context.Parents
                .Include(p => p.User)
                .Where(p => p.User.FullName.ToLower().Contains(name.ToLower()))
                .OrderBy(p => p.User.UserName)
                .ToListAsync();

            return parents;
        }
    }
}
