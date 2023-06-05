using SchoolAttendanceSystem.DAL.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.IRepositories
{
    public interface IParentRepository : IGenericRepository<Parent>
    {
        Task<Parent> FindParentByNid(string nid);
        Task<IEnumerable<Student>> GetStudentsAsync(Parent parent);
        public Task<IEnumerable<Parent>> GetPaginatedParents(int pageNumber, int pageSize);
        public Task<IEnumerable<Parent>> GetPaginatedParents(int pageNumber, int pageSize, string name);
        public Task<IEnumerable<Parent>> GetParentsByName(string name);
    }
}
