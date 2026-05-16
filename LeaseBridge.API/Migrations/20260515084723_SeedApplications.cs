using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaseBridge.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedApplications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "ApplicationId", "ApplicationDate", "CreatedAt", "StatusId", "TenantId", "UnitId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 11, null },
                    { 2, new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, 12, null },
                    { 3, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 5, 13, null },
                    { 4, new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6, 14, null },
                    { 5, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, 15, null },
                    { 6, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 8, 11, null },
                    { 7, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 9, 12, null },
                    { 8, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 10, 13, null },
                    { 9, new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 11, 14, null },
                    { 10, new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 12, 15, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "ApplicationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "ApplicationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "ApplicationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "ApplicationId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "ApplicationId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "ApplicationId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "ApplicationId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "ApplicationId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "ApplicationId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "ApplicationId",
                keyValue: 10);
        }
    }
}
