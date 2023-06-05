using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAttendanceSystem.DAL.Migrations
{
    public partial class AddBirthDateColumnForStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Students",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Students");
        }
    }
}
