using SchoolAttendanceSystem.BLL.DTOs;
using SchoolAttendanceSystem.BLL.DTOs.AttendanceDTOs;
using SchoolAttendanceSystem.BLL.DTOs.AttendanceDTOs.Response;
using SchoolAttendanceSystem.BLL.DTOs.Models;
using SchoolAttendanceSystem.BLL.Extensions.Profiles;
using SchoolAttendanceSystem.BLL.IServices;
using SchoolAttendanceSystem.DAL.Constants;
using SchoolAttendanceSystem.DAL.Entities.Domain;
using SchoolAttendanceSystem.DAL.IRepositories;

namespace SchoolAttendanceSystem.BLL.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IStudentRepository _studentRepository;

        public AttendanceService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<bool> CreateDefaultAttendanceReport()
        {
            try
            {
                var students = await _studentRepository.GetAllAsync();

                foreach (var student in students)
                {
                    if (!await _studentRepository.CreateStudentAttendanceState(student))
                        return false;
                }

                return true;
            }
            catch
            {
                return false;
            }           
        }

        public async Task<GenericResponse<StudentAttendanceModel>> GetStudentAttendance(string studentId, DateTime day)
        {
            var student = await _studentRepository.GetStudentIncludeClass(studentId);

            if (student == null)
                return new() { Errors = new() { "Invalid student id" } };

            var attendanceState = await _studentRepository.GetAttendanceState(student, day);

            if (attendanceState == null)
                return new() { Errors = new() { $"Student no has attendance state at {day}" } };

            var model = attendanceState.ToStudentAttendanceModel();

            return new GenericResponse<StudentAttendanceModel>
            {
                Succeeded = true,
                Status = 200,
                Data = model
            };
        }

        public async Task<StudentAttendanceResponse> GetStudentAttendanceStateForMonth(
            GetAttendanceForMonthDto dto)
        {
            var student = await _studentRepository.GetStudentIncludeClass(dto.studentId);

            if (student == null)
                return new() { Errors = new() { "Invalid student id" } };

            var attends = await _studentRepository.GetAttendanceStatesForMonth(student, dto.month);

            if (attends == null)
                return new() { Errors = new() { "The student has no attendance records for this month" } };

            var model = new List<StudentAttendanceModel>();

            foreach (var attendance in attends)
                model.Add(attendance.ToStudentAttendanceModel());

            var total = attends.Count();
            var present = attends.Where(a => a.Status == StatusTypes.Present.ToString()).Count();
            var absent = attends.Where(a => a.Status == StatusTypes.Absent.ToString()).Count();
            var excused = total - present - absent;

            return new ()
            {
                Succeeded = true,
                Status = 200,
                Total = total,
                Present = present,
                Absent = absent,
                Excused = excused,
                Data = model
            };
        }

        public async Task<SemesterAttendanceResponse> GetStudentAttendanceStateForSemester(string studentId)
        {
            var student = await _studentRepository.GetStudentIncludeClass(studentId);

            if (student == null)
                return new() { Errors = new() { "Not Found" } };

            var attends = await _studentRepository.GetAttendanceStatesForSemester(student);

            if (attends == null)
                return new() { Errors = new() { "This student has no attendance record" } };

            var total = attends.Count();
            var present = attends.Where(a => a.Status == StatusTypes.Present.ToString()).Count();
            var absent = attends.Where(a => a.Status == StatusTypes.Absent.ToString()).Count();
            var excused = total - present - absent;

            return new()
            {
                Succeeded = true,
                Status = 200,
                StartDate = new DateTime(2022,10,1).ToShortDateString().ToString(),
                EndDate = new DateTime(2023, 1, 8).ToShortDateString().ToString(),
                Total = total,
                Present = present,
                Absent = absent,
                Excused = excused
            };
        }

        public async Task<ClassAttendanceResponse> GetClassAttendnaceForDay(string className, DateTime day)
        {
            var students = await _studentRepository.GetStudentsByClassName(className);
            var data = new List<StudentAttendnace>();
            int toatalStudents = students.Count();
            int present = 0, absent = 0, excused = 0;

            // Get student attendance states inside the class
            foreach (var student in students)
            {
                var parentName = await _studentRepository.GetParentName(student.Id);
                var studentName = student.FirstName + " " + parentName;
                var attend = await _studentRepository.GetAttendanceState(student, day);

                data.Add(new()
                {
                    Name = studentName,
                    Status = attend.Status
                });

                if (attend.Status == StatusTypes.Present.ToString()) present++;
                else if (attend.Status == StatusTypes.Absent.ToString()) absent++;
                else excused++;
            }

            return new()
            {
                Succeeded = true,
                Status = 200,
                TotalStudents = toatalStudents,
                Present = present,
                Absent = absent,
                Excused = excused,
                Data = data
            };

        }
    }
}
