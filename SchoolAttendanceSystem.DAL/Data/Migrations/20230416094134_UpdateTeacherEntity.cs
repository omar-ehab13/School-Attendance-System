using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAttendanceSystem.DAL.Migrations
{
    public partial class UpdateTeacherEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeacherTitle",
                table: "Teachers",
                newName: "Specialize");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "Specialize",
                table: "Teachers",
                newName: "TeacherTitle");
        }
    }
}
