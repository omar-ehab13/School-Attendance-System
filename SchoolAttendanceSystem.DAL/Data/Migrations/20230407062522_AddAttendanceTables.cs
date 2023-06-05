using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAttendanceSystem.DAL.Migrations
{
    public partial class AddAttendanceTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentAttendanceStates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfDay = table.Column<DateTime>(type: "Date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAttendanceStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAttendanceStates_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LogTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttendanceStateId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentAttendanceStateId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Log_StudentAttendanceStates_StudentAttendanceStateId",
                        column: x => x.StudentAttendanceStateId,
                        principalTable: "StudentAttendanceStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Log_StudentAttendanceStateId",
                table: "Log",
                column: "StudentAttendanceStateId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendanceStates_StudentId",
                table: "StudentAttendanceStates",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "StudentAttendanceStates");
        }
    }
}
