using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TtExam.DataAccess.Migrations
{
    public partial class InitialMigrationsRepair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StundentNumber",
                table: "Student",
                newName: "StudentNumber");

            migrationBuilder.RenameColumn(
                name: "IdentyNumber",
                table: "Student",
                newName: "IdentityNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Student_IdentyNumber",
                table: "Student",
                newName: "IX_Student_IdentityNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentNumber",
                table: "Student",
                newName: "StundentNumber");

            migrationBuilder.RenameColumn(
                name: "IdentityNumber",
                table: "Student",
                newName: "IdentyNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Student_IdentityNumber",
                table: "Student",
                newName: "IX_Student_IdentyNumber");
        }
    }
}
