using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAttendanceSystem.DAL.Migrations
{
    public partial class UpdateParentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Parents",
                newName: "University");

            migrationBuilder.AddColumn<string>(
                name: "ExternalEmail",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Parents",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalEmail",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Parents");

            migrationBuilder.RenameColumn(
                name: "University",
                table: "Parents",
                newName: "State");
        }
    }
}
