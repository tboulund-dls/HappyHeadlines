using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SubscriberService.Migrations
{
    /// <inheritdoc />
    public partial class PopulateSubscriberTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SubscriberTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("3cbad589-81b4-49c8-a48a-1cee835ea267"), "DAILY" },
                    { new Guid("9a24ad3e-4e3d-4f9b-953d-4c2b4f45abaa"), "NEWSSTREAM" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubscriberTypes",
                keyColumn: "Id",
                keyValue: new Guid("3cbad589-81b4-49c8-a48a-1cee835ea267"));

            migrationBuilder.DeleteData(
                table: "SubscriberTypes",
                keyColumn: "Id",
                keyValue: new Guid("9a24ad3e-4e3d-4f9b-953d-4c2b4f45abaa"));
        }
    }
}
