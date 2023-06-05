using Microsoft.EntityFrameworkCore;
using SchoolAttendanceSystem.DAL.Constants;
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
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Student>> GetPaginatedStudents(int pageNumber, int pageSize)
        {
            int itemsToSkip = (pageNumber - 1) * pageSize;

            List<Student> students = await _context.Students
                .Include(s => s.Class)
                .OrderBy(s => s.FirstName)
                .Skip(itemsToSkip)
                .Take(pageSize)
                .ToListAsync();

            return students;
        }

        public async Task<IEnumerable<Student>> GetPaginatedStudents(int pageNumber, int pageSize, string name)
        {
            int itemsToSkip = (pageNumber - 1) * pageSize;

            List<Student> students = await _context.Students
                .Include(s => s.Class)
                .Where(s => s.FirstName.ToUpper().Contains(name.ToUpper()))
                .OrderBy(s => s.FirstName)
                .Skip(itemsToSkip)
                .Take(pageSize)
                .ToListAsync();

            return students;
        }

        public async Task<bool> CreateStudentAttendanceState(Student student)
        {
            try
            {
                var attend = new StudentAttendanceState
                {
                    StudentId = student.Id,
                    Student = student,
                    Status = StatusTypes.Absent.ToString(),
                    DateOfDay = DateTime.Now
                };

                await _context.StudentAttendanceStates.AddAsync(attend);
                await _context.SaveChangesAsync();

                return true;

            }
            catch
            {
                return false;
            }
        }

        public async Task<StudentAttendanceState> GetAttendanceState(Student student, DateTime day)
        {
            return await _context.StudentAttendanceStates
                .Where(x => x.StudentId == student.Id)
                .Where(x => x.DateOfDay.Day == day.Day
                 && x.DateOfDay.Month == day.Month &&
                x.DateOfDay.Year == day.Year)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<StudentAttendanceState>> GetAttendanceStatesForMonth(Student student, int month)
        {
            var std =  await _context.Students
                    .Where(s => s.Id == student.Id)
                    .Include(s => s.AttendanceStates)
                    .FirstOrDefaultAsync();

            return std.AttendanceStates
                .Where(x => x.DateOfDay.Month == month)
                .OrderBy(a => a.DateOfDay)
                .ToList();
        }

        public async Task<IEnumerable<StudentAttendanceState>> GetAttendanceStatesForSemester(Student student)
        {
            var std = await _context.Students
                .Where(s => s.Id == student.Id)
                .Include(s => s.AttendanceStates)
                .FirstOrDefaultAsync();

            return std.AttendanceStates.OrderBy(a => a.DateOfDay).ToList();
        }

        public async Task<Student> GetStudentIncludeClass(string id)
        {
            return await _context.Students
                .Where(s => s.Id == id)
                .Include(s => s.Class)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsByClassName(string className)
        {
            return await _context.Students
                .Where(s => s.ClassCode == className)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsIncludeClasses()
        {
            return await _context.Students
                .Include(s => s.Class)
                .ToListAsync();
        }

        public async Task<string> GetParentName(string studentId)
        {
            var fullName = await _context.Students
                .Where(s => s.Id == studentId)
                .Include(s => s.Parent)
                    .ThenInclude(p => p.User)
                .Select(s => s.Parent.User.FullName)
                .FirstOrDefaultAsync();

            return fullName;
        }

        public async Task<IEnumerable<Student>> GetStudentsInsideClassAsync(string className)
        {
            var _class = await _context.Classes
                            .Include(c => c.Students)
                                .ThenInclude(s => s.Parent)
                                .ThenInclude(p => p.User)
                            .FirstOrDefaultAsync(c => c.ClassName == className);

            return _class.Students.ToList();
        }
    }
}
