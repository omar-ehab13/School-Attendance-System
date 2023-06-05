using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAttendanceSystem.DAL.Migrations
{
    public partial class UpdateImageUrlToEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Students");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                schema: "security",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Students");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                schema: "security",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Students",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
