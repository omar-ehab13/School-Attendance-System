using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAttendanceSystem.DAL.Migrations
{
    public partial class AddClassColForSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyPeriods_Subject_SubjectCode",
                table: "StudyPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Teachers_TeacherId",
                table: "Subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subject",
                table: "Subject");

            migrationBuilder.RenameTable(
                name: "Subject",
                newName: "Subjects");

            migrationBuilder.RenameIndex(
                name: "IX_Subject_TeacherId",
                table: "Subjects",
                newName: "IX_Subjects_TeacherId");

            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                table: "Subjects",
                type: "nvarchar(3)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "SubjectCode");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ClassName",
                table: "Subjects",
                column: "ClassName");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPeriods_Subjects_SubjectCode",
                table: "StudyPeriods",
                column: "SubjectCode",
                principalTable: "Subjects",
                principalColumn: "SubjectCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Classes_ClassName",
                table: "Subjects",
                column: "ClassName",
                principalTable: "Classes",
                principalColumn: "ClassName");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Teachers_TeacherId",
                table: "Subjects",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyPeriods_Subjects_SubjectCode",
                table: "StudyPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Classes_ClassName",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Teachers_TeacherId",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ClassName",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "ClassName",
                table: "Subjects");

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "Subject");

            migrationBuilder.RenameIndex(
                name: "IX_Subjects_TeacherId",
                table: "Subject",
                newName: "IX_Subject_TeacherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subject",
                table: "Subject",
                column: "SubjectCode");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPeriods_Subject_SubjectCode",
                table: "StudyPeriods",
                column: "SubjectCode",
                principalTable: "Subject",
                principalColumn: "SubjectCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Teachers_TeacherId",
                table: "Subject",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
