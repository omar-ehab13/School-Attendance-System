using SchoolAttendanceSystem.BLL.IServices;
using SchoolAttendanceSystem.DAL.Constants;
using SchoolAttendanceSystem.DAL.Entities.Domain;
using SchoolAttendanceSystem.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogRepository _logRepositry;

        public ReaderService(IStudentRepository studentRepository, ILogRepository logRepository)
        {
            _studentRepository = studentRepository;
            _logRepositry = logRepository;
        }

        public async Task<bool> ReadAsync(string studentId)
        {
            try
            {
                // make log for a student related with today attendance state record
                var student = await _studentRepository.GetByIdAsync(studentId);
                var attend = await _studentRepository.GetAttendanceState(student, DateTime.Now);
                var lastLog = await _logRepositry.GetLastLogForStudent(attend); // TODO: Update this query to optimized one

                string action = "";

                // if its log the first one in the day:
                // so the student has became presented
                if (lastLog == null)
                {
                    var newAttend = new StudentAttendanceState
                    {
                        Id = attend.Id,
                        DateOfDay = attend.DateOfDay,
                        StudentId = attend.StudentId,
                        Student = attend.Student,
                        Status = StatusTypes.Present.ToString()
                    };
                    _logRepositry.UpdateAttendanceState(newAttend);
                    await _logRepositry.SaveChangesAsync();
                    action = ActionTypes.In.ToString();
                }
                // if the last log action to out of the calss: change it to in the class
                else if (lastLog.Action == ActionTypes.Out.ToString())
                    action = ActionTypes.In.ToString();

                else action = ActionTypes.Out.ToString();

                // create new log
                var log = new Log
                {
                    StudentAttendanceState = attend,
                    AttendanceStateId = attend.Id,
                    LogTime = DateTime.Now,
                    Action = action
                };

                await _logRepositry.CreateAsync(log);
                await _logRepositry.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
