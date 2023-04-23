using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TtExam.DataAccess.Migrations
{
    public partial class AddEnumStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Exam",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Exam");
        }
    }
}
