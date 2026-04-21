using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GestorTareas.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FechaCreacion", "Nombre" },
                values: new object[,]
                {
                    { 1, "ana@example.com", new DateTime(2026, 4, 21, 20, 3, 49, 804, DateTimeKind.Utc).AddTicks(834), "Ana García" },
                    { 2, "carlos@example.com", new DateTime(2026, 4, 21, 20, 3, 49, 804, DateTimeKind.Utc).AddTicks(836), "Carlos López" },
                    { 3, "maria@example.com", new DateTime(2026, 4, 21, 20, 3, 49, 804, DateTimeKind.Utc).AddTicks(838), "María Fernández" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
