using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Web.Migrations
{
    public partial class AlterTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DisciplineId",
                table: "Teachers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DisciplineId",
                table: "Teachers",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
