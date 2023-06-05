using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAttendanceSystem.DAL.Migrations
{
    public partial class AddClassEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClassCode",
                table: "Students",
                type: "nvarchar(3)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Code);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassCode",
                table: "Students",
                column: "ClassCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_ClassCode",
                table: "Students",
                column: "ClassCode",
                principalTable: "Classes",
                principalColumn: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_ClassCode",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClassCode",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ClassCode",
                table: "Students");
        }
    }
}
