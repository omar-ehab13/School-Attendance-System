using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAttendanceSystem.DAL.Migrations
{
    public partial class AddNidColumnForParentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nid",
                table: "Parents",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nid",
                table: "Parents");
        }
    }
}
