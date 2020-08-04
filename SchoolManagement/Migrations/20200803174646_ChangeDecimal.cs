using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Migrations
{
    public partial class ChangeDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Credit",
                table: "Subjects",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Credit",
                table: "Subjects",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
