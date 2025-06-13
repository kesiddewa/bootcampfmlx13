using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KlatenUniversityWebApp.Migrations
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
                    CourseID = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Credits = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Major = table.Column<string>(type: "TEXT", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    EnrollmentID = table.Column<Guid>(type: "TEXT", nullable: false),
                    CourseID = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentID = table.Column<int>(type: "INTEGER", nullable: false),
                    Grade = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.EnrollmentID);
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseID", "Credits", "Title" },
                values: new object[,]
                {
                    { 1001, 3, "Introduction to Computer Science" },
                    { 1045, 4, "Calculus" },
                    { 1050, 3, "Chemistry" },
                    { 2001, 4, "Data Structures" },
                    { 2021, 3, "Composition" },
                    { 2042, 4, "Literature" },
                    { 3001, 3, "Database Systems" },
                    { 3141, 4, "Trigonometry" },
                    { 4022, 3, "Microeconomics" },
                    { 4041, 3, "Macroeconomics" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "ID", "Address", "DateOfBirth", "Email", "EnrollmentDate", "Major", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Jl. Sudirman No. 123, Klaten", new DateTime(1987, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "carson.alexander@klaten.edu", new DateTime(2005, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Computer Science", "Carson Alexander", "081234567890" },
                    { 2, "Jl. Ahmad Yani No. 45, Klaten", new DateTime(1984, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "meredith.alonso@klaten.edu", new DateTime(2002, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mathematics", "Meredith Alonso", "081234567891" },
                    { 3, "Jl. Diponegoro No. 67, Klaten", new DateTime(1985, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "arturo.anand@klaten.edu", new DateTime(2003, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Physics", "Arturo Anand", "081234567892" },
                    { 4, "Jl. Gajah Mada No. 89, Klaten", new DateTime(1984, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "gytis.barzdukas@klaten.edu", new DateTime(2002, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chemistry", "Gytis Barzdukas", "081234567893" },
                    { 5, "Jl. Pemuda No. 12, Klaten", new DateTime(1984, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "yan.li@klaten.edu", new DateTime(2002, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Engineering", "Yan Li", "081234567894" },
                    { 6, "Jl. Veteran No. 34, Klaten", new DateTime(1983, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "peggy.justice@klaten.edu", new DateTime(2001, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Biology", "Peggy Justice", "081234567895" },
                    { 7, "Jl. Kartini No. 56, Klaten", new DateTime(1985, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "laura.norman@klaten.edu", new DateTime(2003, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Psychology", "Laura Norman", "081234567896" },
                    { 8, "Jl. Pahlawan No. 78, Klaten", new DateTime(1987, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "nino.olivetto@klaten.edu", new DateTime(2005, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Economics", "Nino Olivetto", "081234567897" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "EnrollmentID", "CourseID", "Grade", "StudentID" },
                values: new object[,]
                {
                    { new Guid("0ced5e71-57c2-414c-9f56-3ab4ce760b6a"), 1050, 0, 1 },
                    { new Guid("26a7d773-e5a2-4b21-a712-5cc8a0a4891e"), 3001, 0, 8 },
                    { new Guid("39d02583-0bf8-4899-ac9d-a16fd1b8baed"), 4022, 2, 1 },
                    { new Guid("45df5326-e0a1-4eee-9e7e-c00e133090ba"), 4041, 1, 1 },
                    { new Guid("4ef453fe-f0c7-464a-8f4f-688083b68caa"), 1050, null, 3 },
                    { new Guid("70a8af7f-236a-469e-b143-4f39c373584e"), 3141, 0, 7 },
                    { new Guid("861fcec0-c015-4ddb-b057-59417e128b69"), 1001, 0, 1 },
                    { new Guid("9207f636-eac2-430e-82fe-a0a95f3fa23d"), 4022, 5, 4 },
                    { new Guid("9e19d0ce-34a6-438e-9860-11a8cf71b05a"), 2001, 1, 1 },
                    { new Guid("a0bf0403-1829-4927-a878-e765bfe68bab"), 1045, 1, 2 },
                    { new Guid("a7a15a0e-c65a-41f3-a9cb-ae4361bb5512"), 2001, null, 5 },
                    { new Guid("a85ec596-bf35-4340-a674-35d9d373fd36"), 1050, null, 4 },
                    { new Guid("c34ddae6-0e83-4a21-bb39-6a83557b39b7"), 1001, 1, 3 },
                    { new Guid("c57f8e61-e61d-4e86-bae3-ddec23fa2bae"), 2021, 2, 2 },
                    { new Guid("cf0d4566-9b13-48a5-8059-f40f8250d778"), 1045, null, 6 },
                    { new Guid("dccf8670-2ddd-4beb-a437-3f5ee915f223"), 4041, 2, 5 },
                    { new Guid("e904c09c-53e2-47dc-a56f-68531abdea16"), 3141, 5, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseID",
                table: "Enrollments",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentID",
                table: "Enrollments",
                column: "StudentID");
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
