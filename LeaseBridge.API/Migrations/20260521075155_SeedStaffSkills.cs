using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaseBridge.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedStaffSkills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StaffSkills",
                columns: new[] { "SkillId", "StaffId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 5, 1 },
                    { 2, 5, 2 },
                    { 1, 6, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StaffSkills",
                keyColumns: new[] { "SkillId", "StaffId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "StaffSkills",
                keyColumns: new[] { "SkillId", "StaffId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "StaffSkills",
                keyColumns: new[] { "SkillId", "StaffId" },
                keyValues: new object[] { 1, 6 });
        }
    }
}
