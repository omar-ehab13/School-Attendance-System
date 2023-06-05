using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SchoolAttendanceSystem.BLL.Helpers;
using SchoolAttendanceSystem.BLL.IServices;
using SchoolAttendanceSystem.BLL.Services;
using System.Text;

namespace SchoolAttendanceSystem.BLL.Extensions
{
    public static class BLLServiceCollectionExtension
    {
        public static IServiceCollection AddBusinessDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IParentService, ParentService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IClassService, ClassService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddTransient<IReaderService, ReaderService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddScoped<ITimetableService, TimetableService>();
            services.AddScoped<ISubjectService, SubjectService>();

            return services;
        }

        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWT>(configuration.GetSection("JWT"));

            var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                //ValidIssuer = configuration["JWT:Issuer"],
                //ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key),
            };

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddSingleton(tokenValidationParameters);

            return services;
        }
    }
}
