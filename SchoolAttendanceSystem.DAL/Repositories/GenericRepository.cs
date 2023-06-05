using Microsoft.EntityFrameworkCore;
using SchoolAttendanceSystem.DAL.Data;
using SchoolAttendanceSystem.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task<bool> CreateAsync(TEntity entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Delete(TEntity entity)
        {
            try
            {
                dbSet.Remove(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch
            {
                return null!;
            }
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool Update(TEntity oldEntity, TEntity entity)
        {
            try
            {
                _context.Entry(oldEntity).CurrentValues.SetValues(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> CountAsync()
        {
            return await dbSet.CountAsync();
        }
    }
}
