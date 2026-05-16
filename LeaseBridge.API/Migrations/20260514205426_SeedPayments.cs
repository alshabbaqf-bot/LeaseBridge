using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaseBridge.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "Amount", "CreatedAt", "DueDate", "LeaseId", "MethodId", "PaymentDate", "StatusId", "TransactionReference", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 450m, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "TXN-1001", null },
                    { 2, 470m, new DateTime(2026, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, new DateTime(2026, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "TXN-1002", null },
                    { 3, 350m, new DateTime(2026, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, null, 1, "TXN-1003", null },
                    { 4, 360m, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "TXN-1004", null },
                    { 5, 1200m, new DateTime(2026, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 2, null, 4, "TXN-1005", null },
                    { 6, 1250m, new DateTime(2026, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 3, new DateTime(2026, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "TXN-1006", null },
                    { 7, 800m, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 1, null, 1, "TXN-1007", null },
                    { 8, 850m, new DateTime(2026, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 2, new DateTime(2026, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "TXN-1008", null },
                    { 9, 1500m, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 3, null, 3, "TXN-1009", null },
                    { 10, 500m, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 1, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "TXN-1010", null },
                    { 11, 450m, new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "TXN-1011", null },
                    { 12, 470m, new DateTime(2026, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, null, 4, "TXN-1012", null },
                    { 13, 360m, new DateTime(2026, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1, new DateTime(2026, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "TXN-1013", null },
                    { 14, 1250m, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 2, null, 1, "TXN-1014", null },
                    { 15, 850m, new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 3, new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "TXN-1015", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 15);
        }
    }
}
