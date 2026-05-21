using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaseBridge.API.Migrations
{
    /// <inheritdoc />
    public partial class ImproveInvoiceArchitecture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Invoices");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "InvoiceStatus",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceStatus", x => x.StatusId);
                });

            migrationBuilder.InsertData(
                table: "InvoiceStatus",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Paid" },
                    { 3, "Overdue" }
                });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 1,
                column: "StatusId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 2,
                column: "StatusId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 3,
                column: "StatusId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_StatusId",
                table: "Invoices",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Status",
                table: "Invoices",
                column: "StatusId",
                principalTable: "InvoiceStatus",
                principalColumn: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Status",
                table: "Invoices");

            migrationBuilder.DropTable(
                name: "InvoiceStatus");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_StatusId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Invoices");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 1,
                column: "IsPaid",
                value: true);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 2,
                column: "IsPaid",
                value: false);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 3,
                column: "IsPaid",
                value: false);
        }
    }
}
