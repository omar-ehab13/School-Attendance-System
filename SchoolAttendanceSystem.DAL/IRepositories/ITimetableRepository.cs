using SchoolAttendanceSystem.DAL.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.IRepositories
{
    public interface ITimetableRepository
    {
        Task<StudyingDay> GetDaysForClassAsync(string className, string firstSubDayName);
        Task<IEnumerable<Subject>> GetSubjectsForClassAsync(string className);
        Task<IEnumerable<StudyPeriod>> GetPeriodsForSubject(Subject subject);
    }
}
