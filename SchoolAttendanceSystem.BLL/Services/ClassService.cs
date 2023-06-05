using SchoolAttendanceSystem.BLL.DTOs;
using SchoolAttendanceSystem.BLL.DTOs.ClassDTOs;
using SchoolAttendanceSystem.BLL.DTOs.StudentDTOs;
using SchoolAttendanceSystem.BLL.Extensions.Profiles;
using SchoolAttendanceSystem.BLL.Filters;
using SchoolAttendanceSystem.BLL.IServices;
using SchoolAttendanceSystem.DAL.Entities.Domain;
using SchoolAttendanceSystem.DAL.IRepositories;

namespace SchoolAttendanceSystem.BLL.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepositroy;
        private readonly IStudentRepository _studentRepository;

        public ClassService(IClassRepository classRepositroy, IStudentRepository studentRepository)
        {
            _classRepositroy = classRepositroy;
            _studentRepository = studentRepository;
        }

        public async Task<GenericResponse<object>> CreateClassAsync(ClassModel dto)
        {
            var newClass = dto.ToClassEntity();

            if (!await _classRepositroy.CreateAsync(newClass))
                return new GenericResponse<object> { Errors = new() { "Error while creating a new class in DB" } };

            await _classRepositroy.SaveChangesAsync();

            return new GenericResponse<object>
            {
                Succeeded = true,
                Status = 201
            };
        }

        public async Task<GenericResponse<object>> DeleteClassAsync(string className)
        {
            var clas = await _classRepositroy.GetByIdAsync(className);

            if (clas == null)
                return new() { Errors = new() { "Not found" } };

            if (!_classRepositroy.Delete(clas))
                return new() { Errors = new() { "error in deleting data" } };

            await _classRepositroy.SaveChangesAsync();

            return new()
            {
                Succeeded = true,
                Status = 200,
                Message = "Deleted"
            };
        }

        public async Task<PagedResponse<IEnumerable<ClassModel>>> GetAllClassesAsync(PaginationClassFilter pagination)
        {
            IEnumerable<Class> classes;
            int totalRecords;

            if (pagination.Grade == null)
            {
                classes = await _classRepositroy.GetAllPaginatedAsync(pagination.PageNumber, pagination.PageSize);
                var allData = await _classRepositroy.GetAllAsync();
                totalRecords = allData.Count();
            }
            else
            {
                classes = await _classRepositroy.GetAllPaginatedAsync(pagination.PageNumber, pagination.PageSize, pagination.Grade);
                var allData = await _classRepositroy.GetClassesByGradeAsync(pagination.Grade);
                totalRecords = allData.Count();
            }

            if (classes == null)
            {
                return new PagedResponse<IEnumerable<ClassModel>>
                {
                    Errors = new() { "Not Found" }
                };
            }

            var classModels = new List<ClassModel>();

            foreach (var c in classes)
                classModels.Add(new() { Class = c.ClassName, Grade = c.Grade});

            return new PagedResponse<IEnumerable<ClassModel>>
            {
                Succeeded = true,
                Status = 200,
                Message = "Succeeded",
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalRecords = totalRecords,
                Data = classModels
            };
        }

        public async Task<GenericResponse<List<string>>> GetClassesInsideGrade(string grade)
        {
            var classes = await _classRepositroy.GetAllAsync();

            if (classes == null)
            {
                return new GenericResponse<List<string>>
                {
                    Errors = new() { "Invalid Grade!" }
                };
            }

            classes = classes.Where(c => c.Grade == grade).ToList();
            var classGrade = new List<string>();

            foreach (var c in classes)
                classGrade.Add(c.ClassName);

            return new GenericResponse<List<string>>
            {
                Succeeded = true,
                Status = 200,
                Message = $"Get classes related to {grade} grade",
                Data = classGrade
            };
        }

        public async Task<GenericResponse<IEnumerable<StudentModel>>> GetStudentsInsideClassAsync(string className)
        {
            try
            {
                var students = await _studentRepository.GetStudentsInsideClassAsync(className);

                if (students == null)
                    return new() { Errors = new() { "this class not contains students" } };

                var studentsDto = students.Select(s => s.ToStudentDTO(s.Parent, s.Parent.User));

                return new()
                {
                    Succeeded = true,
                    Status = 200,
                    Data = studentsDto
                };
            }
            catch
            {
                return new() { Errors = new() { "this class not contains students" } };
            }
        }

        public async Task<GenericResponse<ClassModel>> UpdateClassAsync(UpdateClass model)
        {
            var clas = await _classRepositroy.GetByIdAsync(model.OldClassName);

            if (clas == null)
                return new() { Errors = new() { "Not found" } };

            var updatedClass = new Class { ClassName = model.NewClassName, Grade = model.NewGrade };

            if (!_classRepositroy.Update(clas, updatedClass))
                return new() { Errors = new() { "error in updating data" } };

            await _classRepositroy.SaveChangesAsync();

            return new()
            {
                Succeeded = true,
                Status = 200,
                Message = "Updated",
                Data = new() { Class = updatedClass.ClassName, Grade = updatedClass.Grade }
            };
        }
    }
}
