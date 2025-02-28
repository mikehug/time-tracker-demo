using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    ScheduleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ClockInTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClockOutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EntryType = table.Column<int>(type: "int", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Task = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeEntries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Email", "EmployeeNumber", "FirstName", "HireDate", "IsActive", "LastName", "Notes", "Phone", "Position" },
                values: new object[,]
                {
                    { 1, "Engineering", "john.doe@example.com", "EMP001", "John", new DateTime(2021, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Doe", null, "555-123-4567", "Software Developer" },
                    { 2, "Marketing", "jane.smith@example.com", "EMP002", "Jane", new DateTime(2020, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Smith", null, "555-987-6543", "Marketing Specialist" },
                    { 3, "Finance", "michael.johnson@example.com", "EMP003", "Michael", new DateTime(2022, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Johnson", null, "555-456-7890", "Financial Analyst" },
                    { 4, "Human Resources", "emily.williams@example.com", "EMP004", "Emily", new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Williams", null, "555-789-0123", "HR Coordinator" },
                    { 5, "Operations", "david.brown@example.com", "EMP005", "David", new DateTime(2019, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Brown", null, "555-234-5678", "Operations Manager" }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "DayOfWeek", "EffectiveDate", "EmployeeId", "EndTime", "ExpirationDate", "IsActive", "Notes", "ScheduleDate", "StartTime" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new TimeSpan(0, 17, 0, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) },
                    { 2, 2, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new TimeSpan(0, 17, 0, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) },
                    { 3, 3, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new TimeSpan(0, 17, 0, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) },
                    { 4, 4, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new TimeSpan(0, 17, 0, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) },
                    { 5, 5, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new TimeSpan(0, 17, 0, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) },
                    { 6, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new TimeSpan(0, 16, 30, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 30, 0, 0) },
                    { 7, 2, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new TimeSpan(0, 16, 30, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 30, 0, 0) },
                    { 8, 3, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new TimeSpan(0, 16, 30, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 30, 0, 0) },
                    { 9, 4, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new TimeSpan(0, 16, 30, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 30, 0, 0) },
                    { 10, 5, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new TimeSpan(0, 16, 30, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 30, 0, 0) },
                    { 11, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new TimeSpan(0, 18, 0, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 0, 0, 0) },
                    { 12, 2, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new TimeSpan(0, 18, 0, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 0, 0, 0) },
                    { 13, 3, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new TimeSpan(0, 18, 0, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 0, 0, 0) },
                    { 14, 4, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new TimeSpan(0, 18, 0, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 0, 0, 0) },
                    { 15, 6, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new TimeSpan(0, 14, 0, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 0, 0, 0) },
                    { 16, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new TimeSpan(0, 17, 0, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) },
                    { 17, 2, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new TimeSpan(0, 17, 0, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) },
                    { 18, 3, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new TimeSpan(0, 17, 0, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) },
                    { 19, 4, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new TimeSpan(0, 17, 0, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) },
                    { 20, 5, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new TimeSpan(0, 17, 0, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) },
                    { 21, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new TimeSpan(0, 12, 0, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0) },
                    { 22, 3, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new TimeSpan(0, 12, 0, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0) },
                    { 23, 5, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new TimeSpan(0, 12, 0, 0, 0), null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "TimeEntries",
                columns: new[] { "Id", "ApprovedBy", "ApprovedDate", "ClockInTime", "ClockOutTime", "EmployeeId", "EntryType", "IsApproved", "Notes", "Project", "Task" },
                values: new object[,]
                {
                    { 100, "System", new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 19, 9, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 19, 18, 0, 0, 0, DateTimeKind.Local), 1, 0, true, "Regular workday for employee 1", null, null },
                    { 102, "System", new DateTime(2025, 2, 22, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 21, 9, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 21, 18, 0, 0, 0, DateTimeKind.Local), 1, 0, true, "Regular workday for employee 1", null, null },
                    { 105, "System", new DateTime(2025, 2, 25, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 24, 9, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 24, 18, 0, 0, 0, DateTimeKind.Local), 1, 0, true, "Regular workday for employee 1", null, null },
                    { 200, "System", new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 19, 19, 0, 0, 0, DateTimeKind.Local), 2, 0, true, "Regular workday for employee 2", null, null },
                    { 201, "System", new DateTime(2025, 2, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 20, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 20, 19, 0, 0, 0, DateTimeKind.Local), 2, 0, true, "Regular workday for employee 2", null, null },
                    { 205, "System", new DateTime(2025, 2, 25, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 24, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 24, 19, 0, 0, 0, DateTimeKind.Local), 2, 0, true, "Regular workday for employee 2", null, null },
                    { 206, "System", new DateTime(2025, 2, 26, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 25, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 25, 19, 0, 0, 0, DateTimeKind.Local), 2, 0, true, "Regular workday for employee 2", null, null },
                    { 300, "System", new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 19, 9, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 19, 17, 0, 0, 0, DateTimeKind.Local), 3, 0, true, "Regular workday for employee 3", null, null },
                    { 301, "System", new DateTime(2025, 2, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 20, 9, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 20, 17, 0, 0, 0, DateTimeKind.Local), 3, 0, true, "Regular workday for employee 3", null, null },
                    { 302, "System", new DateTime(2025, 2, 22, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 21, 9, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 21, 17, 0, 0, 0, DateTimeKind.Local), 3, 0, true, "Regular workday for employee 3", null, null },
                    { 305, "System", new DateTime(2025, 2, 25, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 24, 9, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 24, 17, 0, 0, 0, DateTimeKind.Local), 3, 0, true, "Regular workday for employee 3", null, null },
                    { 306, "System", new DateTime(2025, 2, 26, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 25, 9, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 25, 17, 0, 0, 0, DateTimeKind.Local), 3, 0, true, "Regular workday for employee 3", null, null },
                    { 400, "System", new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 19, 18, 0, 0, 0, DateTimeKind.Local), 4, 0, true, "Regular workday for employee 4", null, null },
                    { 401, "System", new DateTime(2025, 2, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 20, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 20, 18, 0, 0, 0, DateTimeKind.Local), 4, 0, true, "Regular workday for employee 4", null, null },
                    { 402, "System", new DateTime(2025, 2, 22, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 21, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 21, 18, 0, 0, 0, DateTimeKind.Local), 4, 0, true, "Regular workday for employee 4", null, null },
                    { 405, "System", new DateTime(2025, 2, 25, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 24, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 24, 18, 0, 0, 0, DateTimeKind.Local), 4, 0, true, "Regular workday for employee 4", null, null },
                    { 406, "System", new DateTime(2025, 2, 26, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 25, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 25, 18, 0, 0, 0, DateTimeKind.Local), 4, 0, true, "Regular workday for employee 4", null, null },
                    { 500, "System", new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 19, 9, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 19, 19, 0, 0, 0, DateTimeKind.Local), 5, 0, true, "Regular workday for employee 5", null, null },
                    { 501, "System", new DateTime(2025, 2, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 20, 9, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 20, 19, 0, 0, 0, DateTimeKind.Local), 5, 0, true, "Regular workday for employee 5", null, null },
                    { 502, "System", new DateTime(2025, 2, 22, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 21, 9, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 21, 19, 0, 0, 0, DateTimeKind.Local), 5, 0, true, "Regular workday for employee 5", null, null },
                    { 505, "System", new DateTime(2025, 2, 25, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 24, 9, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 24, 19, 0, 0, 0, DateTimeKind.Local), 5, 0, true, "Regular workday for employee 5", null, null },
                    { 506, "System", new DateTime(2025, 2, 26, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 25, 9, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 25, 19, 0, 0, 0, DateTimeKind.Local), 5, 0, true, "Regular workday for employee 5", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_EmployeeId",
                table: "Schedules",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntries_EmployeeId",
                table: "TimeEntries",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "TimeEntries");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
