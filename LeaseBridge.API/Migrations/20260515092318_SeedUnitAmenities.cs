using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaseBridge.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedUnitAmenities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UnitAmenities",
                columns: new[] { "AmenityId", "UnitId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 4, 1 },
                    { 2, 2 },
                    { 4, 2 },
                    { 1, 3 },
                    { 3, 3 },
                    { 2, 4 },
                    { 3, 4 },
                    { 4, 4 },
                    { 1, 5 },
                    { 4, 6 },
                    { 1, 7 },
                    { 2, 7 },
                    { 3, 8 },
                    { 1, 9 },
                    { 4, 9 },
                    { 2, 10 },
                    { 3, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UnitAmenities",
                keyColumns: new[] { "AmenityId", "UnitId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "UnitAmenities",
                keyColumns: new[] { "AmenityId", "UnitId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "UnitAmenities",
                keyColumns: new[] { "AmenityId", "UnitId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "UnitAmenities",
                keyColumns: new[] { "AmenityId", "UnitId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "UnitAmenities",
                keyColumns: new[] { "AmenityId", "UnitId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "UnitAmenities",
                keyColumns: new[] { "AmenityId", "UnitId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "UnitAmenities",
                keyColumns: new[] { "AmenityId", "UnitId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "UnitAmenities",
                keyColumns: new[] { "AmenityId", "UnitId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "UnitAmenities",
                keyColumns: new[] { "AmenityId", "UnitId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "UnitAmenities",
                keyColumns: new[] { "AmenityId", "UnitId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "UnitAmenities",
                keyColumns: new[] { "AmenityId", "UnitId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "UnitAmenities",
                keyColumns: new[] { "AmenityId", "UnitId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "UnitAmenities",
                keyColumns: new[] { "AmenityId", "UnitId" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "UnitAmenities",
                keyColumns: new[] { "AmenityId", "UnitId" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "UnitAmenities",
                keyColumns: new[] { "AmenityId", "UnitId" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "UnitAmenities",
                keyColumns: new[] { "AmenityId", "UnitId" },
                keyValues: new object[] { 4, 9 });

            migrationBuilder.DeleteData(
                table: "UnitAmenities",
                keyColumns: new[] { "AmenityId", "UnitId" },
                keyValues: new object[] { 2, 10 });

            migrationBuilder.DeleteData(
                table: "UnitAmenities",
                keyColumns: new[] { "AmenityId", "UnitId" },
                keyValues: new object[] { 3, 10 });
        }
    }
}
