using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAttendanceSystem.DAL.Migrations
{
    public partial class SeedRolesAndDefaultUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "security",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7de9e0d6-7698-454c-8093-f5cc111bc251", "45259077-663d-4409-ab4b-aab2da1d5d27", "Parent", "PARENT" },
                    { "98f4df00-a720-4794-bacb-921996ae6d22", "294d9109-735e-4481-9754-52c8b4c87ed8", "Admin", "ADMIN" },
                    { "c04651d9-82f3-4096-951c-cfa33d654382", "0fa3dc95-f069-4e0b-9d6e-aa7370bbcb73", "Teacher", "TEACHER" },
                    { "c7dedf49-21a9-4f26-8c00-eadd86eb812f", "929f3c01-f56e-45fb-80e5-59460195b908", "SuperAdmin", "SUPERADMIN" }
                });

            migrationBuilder.InsertData(
                schema: "security",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "31784be6-9506-4e3f-a4d2-a9ba79689376", 0, "be424d00-35a1-4cc8-97de-559fb42cd92e", "superadmin@superadmin.ibnkhaldun", false, "superadmin", false, null, "SUPERADMIN@SUPERADMIN.IBNKHALDUN", "SUPERADMIN@SUPERADMIN.IBNKHALDUN", "AQAAAAEAACcQAAAAEIH6S+TM+CzS8uRLpq7Qo7unTY8cQniHZZ1JDL6VrO0e0eP5D9q8WiPowUWsYm+ffQ==", null, false, "342b6a16-6b50-49a6-8fac-0dcbe477c8a8", false, "superadmin@superadmin.ibnkhaldun" });

            migrationBuilder.InsertData(
                schema: "security",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c7dedf49-21a9-4f26-8c00-eadd86eb812f", "31784be6-9506-4e3f-a4d2-a9ba79689376" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7de9e0d6-7698-454c-8093-f5cc111bc251");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "98f4df00-a720-4794-bacb-921996ae6d22");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c04651d9-82f3-4096-951c-cfa33d654382");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c7dedf49-21a9-4f26-8c00-eadd86eb812f", "31784be6-9506-4e3f-a4d2-a9ba79689376" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c7dedf49-21a9-4f26-8c00-eadd86eb812f");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "Users",
                keyColumn: "Id",
                keyValue: "31784be6-9506-4e3f-a4d2-a9ba79689376");
        }
    }
}
