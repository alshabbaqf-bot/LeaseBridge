using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaseBridge.API.Migrations
{
    /// <inheritdoc />
    public partial class FixStaffSkills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__StaffSkil__Staff__5FB337D6",
                table: "StaffSkills");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "StaffSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StaffSkills_CategoryId",
                table: "StaffSkills",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffSkills_Category",
                table: "StaffSkills",
                column: "CategoryId",
                principalTable: "MaintenanceCategories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffSkills_Staff",
                table: "StaffSkills",
                column: "StaffId",
                principalTable: "AppUsers",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffSkills_Category",
                table: "StaffSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffSkills_Staff",
                table: "StaffSkills");

            migrationBuilder.DropIndex(
                name: "IX_StaffSkills_CategoryId",
                table: "StaffSkills");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "StaffSkills");

            migrationBuilder.AddForeignKey(
                name: "FK__StaffSkil__Staff__5FB337D6",
                table: "StaffSkills",
                column: "StaffId",
                principalTable: "AppUsers",
                principalColumn: "UserId");
        }
    }
}
