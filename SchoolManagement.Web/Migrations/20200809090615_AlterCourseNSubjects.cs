using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Web.Migrations
{
    public partial class AlterCourseNSubjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesWithSubjects_Courses_CourseId",
                table: "CoursesWithSubjects");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CoursesWithSubjects",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesWithSubjects_Courses_CourseId",
                table: "CoursesWithSubjects",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesWithSubjects_Courses_CourseId",
                table: "CoursesWithSubjects");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CoursesWithSubjects",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesWithSubjects_Courses_CourseId",
                table: "CoursesWithSubjects",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
