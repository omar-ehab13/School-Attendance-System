using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAttendanceSystem.DAL.Migrations
{
    public partial class AddTimetableEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeacherTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Users_Id",
                        column: x => x.Id,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Timetables",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timetables", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Timetables_Classes_ClassName",
                        column: x => x.ClassName,
                        principalTable: "Classes",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    SubjectCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjcetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.SubjectCode);
                    table.ForeignKey(
                        name: "FK_Subject_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyingDays",
                columns: table => new
                {
                    DayCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyingDays", x => x.DayCode);
                    table.ForeignKey(
                        name: "FK_StudyingDays_Timetables_ClassName",
                        column: x => x.ClassName,
                        principalTable: "Timetables",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyPeriods",
                columns: table => new
                {
                    PeriodCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PeriodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPeriods", x => x.PeriodCode);
                    table.ForeignKey(
                        name: "FK_StudyPeriods_StudyingDays_DayCode",
                        column: x => x.DayCode,
                        principalTable: "StudyingDays",
                        principalColumn: "DayCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyPeriods_Subject_SubjectCode",
                        column: x => x.SubjectCode,
                        principalTable: "Subject",
                        principalColumn: "SubjectCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudyingDays_ClassName",
                table: "StudyingDays",
                column: "ClassName");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPeriods_DayCode",
                table: "StudyPeriods",
                column: "DayCode");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPeriods_SubjectCode",
                table: "StudyPeriods",
                column: "SubjectCode");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_TeacherId",
                table: "Subject",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyPeriods");

            migrationBuilder.DropTable(
                name: "StudyingDays");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Timetables");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
