using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaseBridge.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedInvoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "Amount", "DueDate", "InvoiceNumber", "IsPaid", "IssuedDate", "LeaseId", "PaymentId" },
                values: new object[,]
                {
                    { 1, 450.00m, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-1001", true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, 600.00m, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-1002", false, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2 },
                    { 3, 700.00m, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-1003", false, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 3);
        }
    }
}
