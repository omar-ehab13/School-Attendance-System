using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAttendanceSystem.DAL.Migrations
{
    public partial class AddImageColToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                schema: "security",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                schema: "security",
                table: "Users");
        }
    }
}
