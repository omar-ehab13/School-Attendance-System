using SchoolAttendanceSystem.DAL.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.IRepositories
{
    public interface ITeacherRepository : IGenericRepository<Teacher>
    {
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
        Task<IEnumerable<Subject>> GetAllSubejects(string teacherId);
        Task<Teacher> GetTeacherById(string id);
    }
}
