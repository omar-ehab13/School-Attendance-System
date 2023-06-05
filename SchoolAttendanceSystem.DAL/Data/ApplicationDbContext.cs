using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolAttendanceSystem.DAL.Constants;
using SchoolAttendanceSystem.DAL.Entities.Auth;
using SchoolAttendanceSystem.DAL.Entities.Domain;

namespace SchoolAttendanceSystem.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Parent>? Parents { get; set; }
        public DbSet<Student>? Students { get; set; }
        public DbSet<Class>? Classes { get; set; }
        public DbSet<StudentAttendanceState>? StudentAttendanceStates { get; set; }
        public DbSet<Log>? Logs { get; set; }
        public DbSet<Teacher>? Teachers { get; set; }
        public DbSet<StudyingDay>? StudyingDays { get; set; }
        public DbSet<StudyPeriod>? StudyPeriods { get; set; }
        public DbSet<Subject>? Subjects { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override async void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //// Composite Key
            //builder.Entity<StudentAttendanceState>()
            //    .HasKey(p => new { p.StudentId, p.DateOfDay });

            builder.Entity<User>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");

            // seeding roles
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = RolesId.SuperAdminRoleId,
                Name = RoleTypes.SuperAdmin.ToString(),
                NormalizedName = RoleTypes.SuperAdmin.ToString().ToUpper(),
                ConcurrencyStamp = RolesId.SuperAdminConcurrencyStamp
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = RolesId.AdminRoleId,
                Name = RoleTypes.Admin.ToString(),
                NormalizedName = RoleTypes.Admin.ToString().ToUpper(),
                ConcurrencyStamp = RolesId.AdminConcurrencyStamp
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = RolesId.ParentRoleId,
                Name = RoleTypes.Parent.ToString(),
                NormalizedName = RoleTypes.Parent.ToString().ToUpper(),
                ConcurrencyStamp = RolesId.ParentConcurrencyStamp
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = RolesId.TeacherRoleId,
                Name = RoleTypes.Teacher.ToString(),
                NormalizedName = RoleTypes.Teacher.ToString().ToUpper(),
                ConcurrencyStamp = RolesId.TeacherConcurrencyStamp
            });

            // Seeding default super admin user
            builder.Entity<User>().HasData(new User
            {
                Id = Defaults.DefaultSuperAdminId,
                UserName = Defaults.DefaultSuperAdminEmail,
                NormalizedUserName = Defaults.DefaultSuperAdminEmail.ToUpper(),
                Email = Defaults.DefaultSuperAdminEmail,
                NormalizedEmail = Defaults.DefaultSuperAdminEmail.ToUpper(),
                FullName = Defaults.DefaultSuperAdminName,
                PasswordHash = Defaults.DefaultPasswordHash,
                ConcurrencyStamp = Defaults.DefaultUserConcurrencyStamp,
                SecurityStamp = Defaults.DefaultUserSecurityStamp
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = Defaults.DefaultSuperAdminId,
                RoleId = RolesId.SuperAdminRoleId
            });
        }
    }
}
