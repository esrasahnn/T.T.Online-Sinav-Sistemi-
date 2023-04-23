using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TtExam.DataAccess.Migrations
{
    public partial class SentenceLenght : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Point",
                table: "StudentExam",
                type: "decimal(6,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Point",
                table: "StudentExam",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)");
        }
    }
}
