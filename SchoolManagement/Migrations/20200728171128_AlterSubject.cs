using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Migrations
{
    public partial class AlterSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Qualification",
                table: "Subjects",
                newName: "Reference");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Subjects",
                newName: "Workload");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Subjects",
                newName: "Objectiv");

            migrationBuilder.AlterColumn<double>(
                name: "Credit",
                table: "Subjects",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Subjects",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "Workload",
                table: "Subjects",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "Reference",
                table: "Subjects",
                newName: "Qualification");

            migrationBuilder.RenameColumn(
                name: "Objectiv",
                table: "Subjects",
                newName: "Description");

            migrationBuilder.AlterColumn<int>(
                name: "Credit",
                table: "Subjects",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
