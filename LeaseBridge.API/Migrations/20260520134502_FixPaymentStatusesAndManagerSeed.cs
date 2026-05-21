using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaseBridge.API.Migrations
{
    /// <inheritdoc />
    public partial class FixPaymentStatusesAndManagerSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1,
                column: "StatusId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 2,
                column: "StatusId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 4,
                column: "StatusId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 6,
                column: "StatusId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 8,
                column: "StatusId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 9,
                column: "StatusId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 10,
                column: "StatusId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 11,
                column: "StatusId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 13,
                column: "StatusId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 15,
                column: "StatusId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 1,
                column: "ManagerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 2,
                column: "ManagerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 3,
                column: "ManagerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 4,
                column: "ManagerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 5,
                column: "ManagerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 6,
                column: "ManagerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 7,
                column: "ManagerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 8,
                column: "ManagerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 9,
                column: "ManagerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 10,
                column: "ManagerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 11,
                column: "ManagerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 12,
                column: "ManagerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 13,
                column: "ManagerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 14,
                column: "ManagerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 15,
                column: "ManagerId",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1,
                column: "StatusId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 2,
                column: "StatusId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 4,
                column: "StatusId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 6,
                column: "StatusId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 8,
                column: "StatusId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 9,
                column: "StatusId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 10,
                column: "StatusId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 11,
                column: "StatusId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 13,
                column: "StatusId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 15,
                column: "StatusId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 1,
                column: "ManagerId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 2,
                column: "ManagerId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 3,
                column: "ManagerId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 4,
                column: "ManagerId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 5,
                column: "ManagerId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 6,
                column: "ManagerId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 7,
                column: "ManagerId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 8,
                column: "ManagerId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 9,
                column: "ManagerId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 10,
                column: "ManagerId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 11,
                column: "ManagerId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 12,
                column: "ManagerId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 13,
                column: "ManagerId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 14,
                column: "ManagerId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 15,
                column: "ManagerId",
                value: 13);
        }
    }
}
