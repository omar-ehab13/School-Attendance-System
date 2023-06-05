using SchoolAttendanceSystem.DAL.Entities.Domain;
using SchoolAttendanceSystem.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.IRepositories
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        Task<Subject>? GetSubjectIncludeTeacher(string subjectCode);
        Task<IEnumerable<Subject>> GetSubjectsInsideClass(string className);
        Task<IEnumerable<Subject>> GetSubjectsInsideGrade(string grade);
        Task<IEnumerable<Subject>> GetAllSubjectsIncludeTeacher();
    }
}
