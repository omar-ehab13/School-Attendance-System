using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<bool> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(string id);
        bool Delete(TEntity entity);
        bool Update(TEntity oldEntity, TEntity entity);
        Task SaveChangesAsync();
        Task<int> CountAsync();
    }
}
