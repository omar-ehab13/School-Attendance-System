using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolAttendanceSystem.DAL.Data;
using SchoolAttendanceSystem.DAL.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using SchoolAttendanceSystem.DAL.IRepositories;
using SchoolAttendanceSystem.DAL.Repositories;

namespace SchoolAttendanceSystem.DAL.Extensions
{
    public static class DALServiceCollectionExtension
    {
        public static IServiceCollection AddDataAccessService(this IServiceCollection services, IConfiguration configuration)
        {
            // Add database
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Add Identity
            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddRepositoryDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IParentRepository, ParentRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ITimetableRepository, TimetableRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();

            services.AddScoped<DemoData>();
            services.AddScoped<TimetableData>();

            return services;
        }
    }
}
