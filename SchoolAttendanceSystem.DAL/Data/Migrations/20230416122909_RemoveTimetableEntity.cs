using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAttendanceSystem.DAL.Migrations
{
    public partial class RemoveTimetableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyingDays_Timetables_ClassName",
                table: "StudyingDays");

            migrationBuilder.DropTable(
                name: "Timetables");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyingDays_Classes_ClassName",
                table: "StudyingDays",
                column: "ClassName",
                principalTable: "Classes",
                principalColumn: "ClassName",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyingDays_Classes_ClassName",
                table: "StudyingDays");

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

            migrationBuilder.AddForeignKey(
                name: "FK_StudyingDays_Timetables_ClassName",
                table: "StudyingDays",
                column: "ClassName",
                principalTable: "Timetables",
                principalColumn: "ClassName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
