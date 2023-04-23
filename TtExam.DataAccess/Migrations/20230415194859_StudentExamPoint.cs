using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TtExam.DataAccess.Migrations
{
    public partial class StudentExamPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Point",
                table: "StudentExam",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Point",
                table: "StudentExam");
        }
    }
}
