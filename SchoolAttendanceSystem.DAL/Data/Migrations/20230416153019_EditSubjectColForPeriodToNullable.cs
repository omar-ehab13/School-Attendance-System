using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAttendanceSystem.DAL.Migrations
{
    public partial class EditSubjectColForPeriodToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyPeriods_StudyingDays_DayCode",
                table: "StudyPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyPeriods_Subject_SubjectCode",
                table: "StudyPeriods");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectCode",
                table: "StudyPeriods",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "DayCode",
                table: "StudyPeriods",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPeriods_StudyingDays_DayCode",
                table: "StudyPeriods",
                column: "DayCode",
                principalTable: "StudyingDays",
                principalColumn: "DayCode");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPeriods_Subject_SubjectCode",
                table: "StudyPeriods",
                column: "SubjectCode",
                principalTable: "Subject",
                principalColumn: "SubjectCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyPeriods_StudyingDays_DayCode",
                table: "StudyPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyPeriods_Subject_SubjectCode",
                table: "StudyPeriods");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectCode",
                table: "StudyPeriods",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DayCode",
                table: "StudyPeriods",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPeriods_StudyingDays_DayCode",
                table: "StudyPeriods",
                column: "DayCode",
                principalTable: "StudyingDays",
                principalColumn: "DayCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPeriods_Subject_SubjectCode",
                table: "StudyPeriods",
                column: "SubjectCode",
                principalTable: "Subject",
                principalColumn: "SubjectCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
