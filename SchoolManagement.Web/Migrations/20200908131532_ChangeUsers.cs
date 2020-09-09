using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Web.Migrations
{
    public partial class ChangeUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CourseWithDisciplines",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseWithDisciplines_UserId",
                table: "CourseWithDisciplines",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseWithDisciplines_AspNetUsers_UserId",
                table: "CourseWithDisciplines",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseWithDisciplines_AspNetUsers_UserId",
                table: "CourseWithDisciplines");

            migrationBuilder.DropIndex(
                name: "IX_CourseWithDisciplines_UserId",
                table: "CourseWithDisciplines");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CourseWithDisciplines");
        }
    }
}
