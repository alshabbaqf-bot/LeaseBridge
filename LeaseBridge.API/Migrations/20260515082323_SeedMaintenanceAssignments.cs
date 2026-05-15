using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaseBridge.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedMaintenanceAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MaintenanceAssignments",
                columns: new[] { "AssignmentId", "AssignedDate", "RequestId", "StaffId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 13 },
                    { 2, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 14 },
                    { 3, new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 15 },
                    { 4, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 16 },
                    { 5, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 17 },
                    { 6, new DateTime(2026, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 13 },
                    { 7, new DateTime(2026, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 14 },
                    { 8, new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 15 },
                    { 9, new DateTime(2026, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 16 },
                    { 10, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 17 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MaintenanceAssignments",
                keyColumn: "AssignmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MaintenanceAssignments",
                keyColumn: "AssignmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MaintenanceAssignments",
                keyColumn: "AssignmentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MaintenanceAssignments",
                keyColumn: "AssignmentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MaintenanceAssignments",
                keyColumn: "AssignmentId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MaintenanceAssignments",
                keyColumn: "AssignmentId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MaintenanceAssignments",
                keyColumn: "AssignmentId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MaintenanceAssignments",
                keyColumn: "AssignmentId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MaintenanceAssignments",
                keyColumn: "AssignmentId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MaintenanceAssignments",
                keyColumn: "AssignmentId",
                keyValue: 10);
        }
    }
}
