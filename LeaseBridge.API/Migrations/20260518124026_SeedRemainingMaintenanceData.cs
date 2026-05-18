using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaseBridge.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedRemainingMaintenanceData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Feedback",
                columns: new[] { "FeedbackId", "CreatedAt", "Message", "Rating", "RequestId", "TenantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maintenance team was quick and professional.", 5, 5, 7 },
                    { 2, new DateTime(2026, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Painting quality was very good.", 4, 9, 11 }
                });

            migrationBuilder.InsertData(
                table: "MaintenanceAttachments",
                columns: new[] { "AttachmentId", "FileUrl", "RequestId" },
                values: new object[,]
                {
                    { 1, "https://example.com/leak-photo.jpg", 1 },
                    { 2, "https://example.com/door-lock.jpg", 5 },
                    { 3, "https://example.com/wall-paint.jpg", 9 }
                });

            migrationBuilder.InsertData(
                table: "MaintenanceUpdates",
                columns: new[] { "UpdateId", "CreatedAt", "NewStatusId", "Notes", "OldStatusId", "RequestId", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Assigned to plumbing staff.", 1, 1, 13 },
                    { 2, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Door lock repaired successfully.", 2, 5, 17 },
                    { 3, new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Wall repaint completed.", 3, 9, 16 }
                });

            migrationBuilder.InsertData(
                table: "UnitImages",
                columns: new[] { "ImageId", "ImageUrl", "UnitId" },
                values: new object[,]
                {
                    { 1, "https://images.unsplash.com/photo-1505693416388-ac5ce068fe85", 1 },
                    { 2, "https://images.unsplash.com/photo-1494526585095-c41746248156", 2 },
                    { 3, "https://images.unsplash.com/photo-1484154218962-a197022b5858", 3 },
                    { 4, "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Feedback",
                keyColumn: "FeedbackId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Feedback",
                keyColumn: "FeedbackId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MaintenanceAttachments",
                keyColumn: "AttachmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MaintenanceAttachments",
                keyColumn: "AttachmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MaintenanceAttachments",
                keyColumn: "AttachmentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MaintenanceUpdates",
                keyColumn: "UpdateId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MaintenanceUpdates",
                keyColumn: "UpdateId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MaintenanceUpdates",
                keyColumn: "UpdateId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UnitImages",
                keyColumn: "ImageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UnitImages",
                keyColumn: "ImageId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UnitImages",
                keyColumn: "ImageId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UnitImages",
                keyColumn: "ImageId",
                keyValue: 4);
        }
    }
}
