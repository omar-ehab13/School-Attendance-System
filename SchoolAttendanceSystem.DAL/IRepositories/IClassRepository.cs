using SchoolAttendanceSystem.DAL.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.IRepositories
{
    public interface IClassRepository : IGenericRepository<Class>
    {
        Task<IEnumerable<Class>> GetAllPaginatedAsync(int page, int pageSize);
        Task<IEnumerable<Class>> GetAllPaginatedAsync(int page, int pageSize, string name);
        Task<IEnumerable<Class>> GetClassesByGradeAsync(string grade);
    }
}
