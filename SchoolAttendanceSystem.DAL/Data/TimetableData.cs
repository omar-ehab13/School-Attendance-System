using Microsoft.EntityFrameworkCore;
using SchoolAttendanceSystem.DAL.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.Data
{
    public class TimetableData
    {
        private readonly ApplicationDbContext _context;

        public TimetableData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Subject>> GenerateSubjectsAsync(string className)
        {
            try
            {
                // validate from className
                if (!await _context.Classes.AnyAsync(c => c.ClassName == className))
                    return null;

                var subjects = new List<Subject>();

                // Add subjects to the list
                await Task.Factory.StartNew(() => subjects.Add(new Subject 
                { SubjcetName = "Arabic", SubjectCode = $"AR-{className}", ClassName = className }));
                await Task.Factory.StartNew(() => subjects.Add(new Subject 
                { SubjcetName = "English", SubjectCode = $"E-{className}", ClassName = className }));
                await Task.Factory.StartNew(() => subjects.Add(new Subject 
                { SubjcetName = "Math", SubjectCode = $"MATH-{className}", ClassName = className }));
                await Task.Factory.StartNew(() => subjects.Add(new Subject 
                { SubjcetName = "Science", SubjectCode = $"SCI-{className}", ClassName = className }));
                await Task.Factory.StartNew(() => subjects.Add(new Subject 
                { SubjcetName = "Social Studies", SubjectCode = $"SOC-{className}", ClassName = className }));
                await Task.Factory.StartNew(() => subjects.Add(new Subject 
                { SubjcetName = "Computer", SubjectCode = $"COM-{className}", ClassName = className }));
                await Task.Factory.StartNew(() => subjects.Add(new Subject 
                { SubjcetName = "Religious Education", SubjectCode = $"REL-{className}", ClassName = className }));
                await Task.Factory.StartNew(() => subjects.Add(new Subject 
                { SubjcetName = "Sports", SubjectCode = $"SP-{className}", ClassName = className }));
                await Task.Factory.StartNew(() => subjects.Add(new Subject 
                { SubjcetName = "Skills", SubjectCode = $"SK-{className}", ClassName = className }));

                await _context.Subjects.AddRangeAsync(subjects);
                await _context.SaveChangesAsync();

                return subjects;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> GenerateClassTimetableTemp(string className)
        {
            try
            {
                var days = _context.Classes
                    .AsNoTracking()
                    .Include(c => c.StudyingDays)
                    .Select(c => c.StudyingDays)
                    .ToList();

                if (days.Count == 0)
                    return false;

                for (int i = 1; i <= 5; i++)
                {
                    var dayCode = await CreateDayCode(i, className);
                    var day = new StudyingDay
                    {
                        DayCode = dayCode,
                        ClassName = className
                    };

                    await _context.StudyingDays.AddAsync(day);

                    for (int j = 1; j <= 7; j++)
                    {
                        var peridCode = await CreatePeriodCode(j, className, dayCode);
                        var periodName = await CreatePeriodName(j);
                        var period = new StudyPeriod
                        {
                            PeriodCode = peridCode,
                            PeriodName = periodName,
                            DayCode = dayCode
                        };

                        var res = await _context.StudyPeriods.AddAsync(period);
                    }
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task<string> CreateDayCode(int index, string className)
        {
            StringBuilder code = new StringBuilder();

            switch (index)
            {
                case 1:
                    code.Append("SUN-");
                    break;
                case 2:
                    code.Append("MON-");
                    break;
                case 3:
                    code.Append("TUE-");
                    break;
                case 4:
                    code.Append("WED-");
                    break;
                case 5:
                    code.Append("THU-");
                    break;
            }

            return code.Append(className).ToString();
        }

        private async Task<string> CreatePeriodCode(int index, string className, string dayCode)
        {
            StringBuilder code = new StringBuilder();
            code.Append(dayCode);

            switch (index)
            {
                case 1:
                    code.Append("-01");
                    break;
                case 2:
                    code.Append("-02");
                    break;
                case 3:
                    code.Append("-03");
                    break;
                case 4:
                    code.Append("-04");
                    break;
                case 5:
                    code.Append("-05");
                    break;
                case 6:
                    code.Append("-06");
                    break;
                case 7:
                    code.Append("-07");
                    break;
            }

            return code.ToString();
        }

        private async Task<string> CreatePeriodName(int index)
        {
            switch (index)
            {
                case 1:
                    return "First";
                case 2:
                    return "Second";
                case 3:
                    return "Third";
                case 4:
                    return "Forth";
                case 5:
                    return "Fifth";
                case 6:
                    return "Sixth";
                case 7:
                    return "Seventh";
            }

            return null;
        }

        private async Task<List<StudyPeriod>> GenerateStudyPeriodsAsync(string className, List<Subject> subjects)
        {
            var studyPeriods = new List<StudyPeriod>();

            // Get the studying days for the class
            var studyingDays = await Task.Factory.StartNew(() =>
                _context.StudyingDays
                .Where(sd => sd.ClassName == className)
                .ToList());

            // Loop through each studying day
            foreach (var day in studyingDays)
            {
                // Loop through each period in the day
                for (int i = 1; i <= 7; i++)
                {
                    // Create a study period with the appropriate code and day reference
                    var studyPeriod = new StudyPeriod
                    {
                        PeriodCode = $"{day.DayCode}-{i}",
                        DayCode = day.DayCode
                    };

                    //// Assign a subject to the study period based on its position in the day
                    //if (i == 1 || i == 6 || i == 7)
                    //{
                    //    studyPeriod.SubjectCode = skills.Code;
                    //}
                    //else if (i == 2 || i == 4 || i == 5)
                    //{
                    //    studyPeriod.SubjectCode = science.Code;
                    //}
                    //else if (i == 3)
                    //{
                    //    studyPeriod.SubjectCode = sports.Code;
                    //}
                    //else
                    //{
                    //    // Alternate between the remaining subjects
                    //    var subjectIndex = (i - 1) / 2;
                    //    var subject = subjects[subjectIndex % (subjects.Count - 2) + 2];
                    //    studyPeriod.SubjectCode = subject.SubjectCode;
                    //}

                    //studyPeriods.Add(studyPeriod);
                }
            }

            return studyPeriods;
        }
    }
}
