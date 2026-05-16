using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaseBridge.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedMaintenanceRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MaintenanceRequests",
                columns: new[] { "RequestId", "CategoryId", "CompletedAt", "CreatedAt", "Description", "PriorityId", "StatusId", "TenantId", "TicketNumber", "Title", "UnitId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Water leaking under the sink cabinet.", 2, 1, 3, "MR-1001", "Leaking kitchen sink", 1, null },
                    { 2, 2, null, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bedroom outlets are not working.", 3, 2, 4, "MR-1002", "Power outage in bedroom", 2, null },
                    { 3, 3, null, new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "AC leaking water continuously.", 2, 1, 5, "MR-1003", "Air conditioner leaking", 3, null },
                    { 4, 1, null, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Drain water backing up.", 3, 2, 6, "MR-1004", "Bathroom pipe blockage", 4, null },
                    { 5, 4, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Front door lock jammed.", 2, 3, 7, "MR-1005", "Broken door lock", 5, null },
                    { 6, 2, null, new DateTime(2026, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Living room lights flickering.", 1, 1, 8, "MR-1006", "Flickering lights", 6, null },
                    { 7, 4, null, new DateTime(2026, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kitchen cabinet hinge is loose.", 1, 2, 9, "MR-1007", "Loose cabinet door", 7, null },
                    { 8, 3, null, new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cooling system stopped working.", 3, 1, 10, "MR-1008", "AC not cooling", 8, null },
                    { 9, 4, new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bedroom wall paint peeling.", 1, 3, 11, "MR-1009", "Wall repaint request", 9, null },
                    { 10, 1, null, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Water leaking around toilet base.", 2, 1, 12, "MR-1010", "Toilet leaking", 10, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MaintenanceRequests",
                keyColumn: "RequestId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MaintenanceRequests",
                keyColumn: "RequestId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MaintenanceRequests",
                keyColumn: "RequestId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MaintenanceRequests",
                keyColumn: "RequestId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MaintenanceRequests",
                keyColumn: "RequestId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MaintenanceRequests",
                keyColumn: "RequestId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MaintenanceRequests",
                keyColumn: "RequestId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MaintenanceRequests",
                keyColumn: "RequestId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MaintenanceRequests",
                keyColumn: "RequestId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MaintenanceRequests",
                keyColumn: "RequestId",
                keyValue: 10);
        }
    }
}
