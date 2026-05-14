using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaseBridge.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "UnitId", "PropertyId", "RentAmount", "Size", "StatusId", "TypeId", "UnitNumber" },
                values: new object[,]
                {
                    { 1, 1, 450m, 120m, 1, 1, "A101" },
                    { 2, 1, 470m, 125m, 2, 1, "A102" },
                    { 3, 2, 350m, 90m, 1, 2, "B201" },
                    { 4, 2, 360m, 92m, 3, 2, "B202" },
                    { 5, 3, 1200m, 350m, 1, 3, "C301" },
                    { 6, 3, 1250m, 360m, 2, 3, "C302" },
                    { 7, 4, 800m, 200m, 1, 4, "OFF-1" },
                    { 8, 4, 850m, 220m, 4, 4, "OFF-2" },
                    { 9, 5, 1500m, 400m, 1, 3, "V101" },
                    { 10, 6, 500m, 130m, 2, 1, "D401" },
                    { 11, 7, 320m, 85m, 1, 2, "E501" },
                    { 12, 8, 950m, 250m, 3, 4, "COM-1" },
                    { 13, 9, 1800m, 500m, 1, 3, "L101" },
                    { 14, 10, 280m, 70m, 1, 2, "STU-1" },
                    { 15, 11, 650m, 150m, 2, 1, "F601" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 15);
        }
    }
}
