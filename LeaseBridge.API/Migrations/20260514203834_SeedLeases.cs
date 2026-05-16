using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaseBridge.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedLeases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Leases",
                columns: new[] { "LeaseId", "EndDate", "IsActive", "StartDate", "StatusId", "TenantId", "UnitId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, 1 },
                    { 2, new DateTime(2027, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, 2 },
                    { 3, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 5, 3 },
                    { 4, new DateTime(2027, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 6, 4 },
                    { 5, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 7, 5 },
                    { 6, new DateTime(2027, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 8, 6 },
                    { 7, new DateTime(2027, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 9, 7 },
                    { 8, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 10, 8 },
                    { 9, new DateTime(2027, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 11, 9 },
                    { 10, new DateTime(2026, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 12, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Leases",
                keyColumn: "LeaseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Leases",
                keyColumn: "LeaseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Leases",
                keyColumn: "LeaseId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Leases",
                keyColumn: "LeaseId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Leases",
                keyColumn: "LeaseId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Leases",
                keyColumn: "LeaseId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Leases",
                keyColumn: "LeaseId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Leases",
                keyColumn: "LeaseId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Leases",
                keyColumn: "LeaseId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Leases",
                keyColumn: "LeaseId",
                keyValue: 10);
        }
    }
}
