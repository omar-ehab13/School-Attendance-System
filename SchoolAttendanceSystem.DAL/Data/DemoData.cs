using Bogus;
using Microsoft.EntityFrameworkCore;
using SchoolAttendanceSystem.DAL.Constants;
using SchoolAttendanceSystem.DAL.Entities.Domain;
using System.Text;

namespace SchoolAttendanceSystem.DAL.Data
{
    public class DemoData
    {
        private readonly ApplicationDbContext _context;

        public DemoData(ApplicationDbContext context)
        {
                _context = context;
        }

        public async Task<bool> GenerateAsync(string studentId = "ed0967b3-f848-4fef-ab6d-e0d199728b85")
        {
            var existingAttendanceStates = await _context.StudentAttendanceStates
                                    .AsNoTracking()
                                    .Where(s => s.StudentId == studentId)
                                    .ToListAsync();

            if (existingAttendanceStates.Any())
            {
                return false;
            }

            var faker = new Faker();

            var presentProb = 0.88;
            var absentProb = 0.10;
            var excusedProb = 0.02;

            var startDate = new DateTime(2022, 10, 1);
            var endDate = new DateTime(2023, 1, 8);

            var days = (endDate - startDate).Days;
            var dates = Enumerable.Range(0, days)
                        .Select(i => startDate.AddDays(i))
                        .Where(date => date.DayOfWeek != DayOfWeek.Friday && date.DayOfWeek != DayOfWeek.Saturday)
                        .ToArray();

            var studentAttends = dates.Select(date => new StudentAttendanceState
            {
                Id = Guid.NewGuid().ToString(),
                StudentId = studentId,
                Status = faker.Random.Double() switch
                {
                    var p when p < presentProb => StatusTypes.Present.ToString(),
                    var p when p < presentProb + absentProb => StatusTypes.Absent.ToString(),
                    _ => StatusTypes.Excused.ToString()
                },
                DateOfDay = date
            }).ToList();

            await _context.StudentAttendanceStates.AddRangeAsync(studentAttends);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Subject>> GetSubjectsAsync()
        {
            return await _context.Subjects.ToListAsync();
        }
        
    }
}
