using SchoolAttendanceSystem.BLL.DTOs;
using SchoolAttendanceSystem.BLL.DTOs.Timetable;
using SchoolAttendanceSystem.BLL.DTOs.TimetableDTOs;
using SchoolAttendanceSystem.BLL.IServices;
using SchoolAttendanceSystem.DAL.Constants;
using SchoolAttendanceSystem.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.Services
{
    public class TimetableService : ITimetableService
    {
        private readonly ITimetableRepository _timetableRepository;
        private readonly ITeacherRepository _teacherRepository;

        public TimetableService(ITimetableRepository timetableRepository, ITeacherRepository teacherRepository)
        {
            _timetableRepository = timetableRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<GenericResponse<object>> GetClassesTimesAsync()
        {

            var data = new List<PeriodsTimeDto>
            {
                new() { Index = 0, Name = "Break", StartTime = PeriodsTime.BreakPeriod.StartTime, EndTime = PeriodsTime.BreakPeriod.EndTime},
                new() { Index = 1, Name = "First", StartTime = PeriodsTime.FirstPeriod.StartTime, EndTime = PeriodsTime.FirstPeriod.EndTime},
                new() { Index = 2, Name = "Second", StartTime = PeriodsTime.SecondPeriod.StartTime, EndTime = PeriodsTime.SecondPeriod.EndTime},
                new() { Index = 3, Name = "Third", StartTime = PeriodsTime.ThirdPeriod.StartTime, EndTime = PeriodsTime.ThirdPeriod.EndTime},
                new() { Index = 4, Name = "Forth", StartTime = PeriodsTime.ForthPeriod.StartTime, EndTime = PeriodsTime.ForthPeriod.EndTime},
                new() { Index = 5, Name = "Fifth", StartTime = PeriodsTime.FifthPeriod.StartTime, EndTime = PeriodsTime.FifthPeriod.EndTime},
                new() { Index = 6, Name = "Sixth", StartTime = PeriodsTime.SixthPeriod.StartTime, EndTime = PeriodsTime.SixthPeriod.EndTime},
                new() { Index = 7, Name = "Seventh", StartTime = PeriodsTime.SeventhPeriod.StartTime, EndTime = PeriodsTime.SeventhPeriod.EndTime},
            };

            return new()
            {
                Succeeded = true,
                Status = 200,
                Data = data
            };
        }

        public async Task<GenericResponse<IEnumerable<TeacherTimetableDto>>> GetTiemtableForTeacher(string teacherId)
        {
            try
            {
                var subjects = await _teacherRepository.GetAllSubejects(teacherId);

                if (subjects == null)
                    return new() { Errors = new() { "teacher not teach any subject" } };

                var data = new List<TeacherTimetableDto>();

                foreach (var s in subjects)
                {
                    var periods = await _timetableRepository.GetPeriodsForSubject(s);

                    foreach (var p in periods)
                    {
                        var init = p.PeriodCode.Split("-")[0];
                        var day = GetDayByInitial(init);
                        var periodNo = GetPeriodNoByName(p.PeriodName);

                        data.Add(new() { Day = day,PeriodNo = periodNo, Period = p.PeriodName, SubjectName = s.SubjcetName, SubjectCode = s.SubjectCode });
                    }
                }

                return new()
                {
                    Succeeded = true,
                    Status = 200,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new() { Errors = new() { ex.Message } };
            }
        }

        public async Task<GenericResponse<ClassTimetableDto>> GetTimetableForClassAsync(string className)
        {
            try
            {
                var timetable = new ClassTimetableDto();

                timetable.Sunday = await GetSubjectsForDayAsync(className, "SUN");
                timetable.Monday = await GetSubjectsForDayAsync(className, "MON");
                timetable.Tuesday = await GetSubjectsForDayAsync(className, "TUE");
                timetable.Wednesday = await GetSubjectsForDayAsync(className, "WED");
                timetable.Thursday = await GetSubjectsForDayAsync(className, "THU");

                return new()
                {
                    Succeeded = true,
                    Status = 200,
                    Data = timetable
                };
            }
            catch
            {
                return new() { Errors = new() { "Not Found" } };
            }
        }

        #region Private Methods
        private async Task<DayTimetable> GetSubjectsForDayAsync(string className, string firstSubDay)
        {
            var day = await _timetableRepository.GetDaysForClassAsync(className, firstSubDay);
            var periods = day.Periods;
            var subjects = await _timetableRepository.GetSubjectsForClassAsync(className);

            var dayTimetable = new DayTimetable();

            dayTimetable.First = subjects.Where(s => s.SubjectCode == periods[0].SubjectCode).FirstOrDefault().SubjcetName;
            dayTimetable.Second = subjects.Where(s => s.SubjectCode == periods[1].SubjectCode).FirstOrDefault().SubjcetName;
            dayTimetable.Third = subjects.Where(s => s.SubjectCode == periods[2].SubjectCode).FirstOrDefault().SubjcetName;
            dayTimetable.Forth = subjects.Where(s => s.SubjectCode == periods[3].SubjectCode).FirstOrDefault().SubjcetName;
            dayTimetable.Fifth = subjects.Where(s => s.SubjectCode == periods[4].SubjectCode).FirstOrDefault().SubjcetName;
            dayTimetable.Sixth = subjects.Where(s => s.SubjectCode == periods[5].SubjectCode).FirstOrDefault().SubjcetName;
            dayTimetable.Seventh = subjects.Where(s => s.SubjectCode == periods[6].SubjectCode).FirstOrDefault().SubjcetName;

            return dayTimetable;
        }

        private string? GetDayByInitial(string init)
        {
            switch (init)
            {
                case "SUN":
                    return "Sunday";
                case "MON":
                    return "Monday";
                case "TUE":
                    return "Tuesday";
                case "WED":
                    return "Wednesday";
                case "THU":
                    return "Thursday";
            }

            return null;
        }

        private int GetPeriodNoByName(string periodName)
        {
            switch (periodName)
            {
                case "First":
                    return 1;
                case "Second":
                    return 2;
                case "Third":
                    return 3;
                case "Forth":
                    return 4;
                case "Fifth":
                    return 5;
                case "Sixth":
                    return 6;
                case "Seventh":
                    return 7;
            }

            return 0;
        }
        #endregion
    }
}
