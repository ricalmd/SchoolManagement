using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Migrations
{
    public partial class AddDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Field",
                table: "Subjects");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Subjects",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Subjects",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Field",
                table: "Subjects",
                nullable: false,
                defaultValue: "");
        }
    }
}
