using SchoolAttendanceSystem.DAL.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.IRepositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<IEnumerable<Student>> GetStudentsIncludeClasses();
        Task<IEnumerable<Student>> GetPaginatedStudents(int pageNumber, int pageSize);
        Task<IEnumerable<Student>> GetPaginatedStudents(int pageNumber, int pageSize, string name);
        Task<bool> CreateStudentAttendanceState(Student student); 
        Task<Student> GetStudentIncludeClass(string id);
        Task<IEnumerable<Student>> GetStudentsByClassName(string className);
        Task<StudentAttendanceState> GetAttendanceState(Student student, DateTime day);
        Task<IEnumerable<StudentAttendanceState>> GetAttendanceStatesForMonth(Student student, int month);
        Task<IEnumerable<StudentAttendanceState>> GetAttendanceStatesForSemester(Student student);
        Task<string> GetParentName(string studentId);
        Task<IEnumerable<Student>> GetStudentsInsideClassAsync(string className);
    }
}
