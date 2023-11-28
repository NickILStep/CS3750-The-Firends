using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment1v3.Migrations
{
    /// <inheritdoc />
    public partial class InitialAzureCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    maxPoints = table.Column<int>(type: "int", nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    uploadType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    School = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseNumber = table.Column<int>(type: "int", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditHours = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartRecur = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndRecur = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    startTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    startRecur = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endRecur = table.Column<DateTime>(type: "datetime2", nullable: false),
                    daysOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    courseId = table.Column<int>(type: "int", nullable: false),
                    studSchedId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email_Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_First = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Name_Last = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Birth_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email_Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Link1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Link2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Link3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProfilePicturePath = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudSched",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email_Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseNum = table.Column<int>(type: "int", nullable: false),
                    StudId = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudSched", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Submission",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    maxPoints = table.Column<int>(type: "int", nullable: false),
                    PointsEarned = table.Column<int>(type: "int", nullable: false),
                    submissionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Upload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextBox = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Graded = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submission", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InstructorCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstructorCourse_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorCourse_Instructor_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstructorCourse_CourseId",
                table: "InstructorCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorCourse_InstructorId",
                table: "InstructorCourse",
                column: "InstructorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "InstructorCourse");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "StudSched");

            migrationBuilder.DropTable(
                name: "Submission");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Instructor");
        }
    }
}
