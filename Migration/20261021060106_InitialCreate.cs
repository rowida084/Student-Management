using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Credits = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.CheckConstraint("CK_Student_EnrollmentDate", "EnrollmentDate<=GetDate()");
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "Credits", "Description", "Title" },
                values: new object[,]
                {
                    { 1, 3, "Introduction to C# programming", "C# Programming" },
                    { 2, 3, "Introduction to relational databases", "Database Systems" },
                    { 3, 3, "Working with EF Core and databases", "Entity Framework Core" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "DateOfBirth", "Email", "EnrollmentDate", "FullName" },
                values: new object[,]
                {
                    { 1, new DateTime(2003, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "ahmed.ali@example.com", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ahmed Ali" },
                    { 2, new DateTime(2004, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara.mohamed@example.com", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sara Mohamed" },
                    { 3, new DateTime(2003, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "omar.hassan@example.com", new DateTime(2025, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Omar Hassan" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "CourseId", "StudentId", "EnrollmentDate", "Grade" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 85 },
                    { 2, 1, new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 90 },
                    { 1, 2, new DateTime(2025, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 78 },
                    { 3, 2, new DateTime(2025, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 3, new DateTime(2025, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 88 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
