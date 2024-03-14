using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.DataAccessLayer.Migrations
{
    public partial class CourseImprovements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseTeacher",
                table: "Courses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CourseTeacherEmail",
                table: "Courses",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EditDeleteCoursePIN",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaxNumberOfAtendees",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseTeacher",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseTeacherEmail",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "EditDeleteCoursePIN",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MaxNumberOfAtendees",
                table: "Courses");
        }
    }
}
