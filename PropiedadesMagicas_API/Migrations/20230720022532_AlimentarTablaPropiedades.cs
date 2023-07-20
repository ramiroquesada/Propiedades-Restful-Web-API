using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropiedadesMagicas_API.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaPropiedades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Propiedades",
                columns: new[] { "Id", "Amenidad", "Detalles", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle de Propiedad Real...", new DateTime(2023, 7, 19, 23, 25, 32, 664, DateTimeKind.Local).AddTicks(8883), new DateTime(2023, 7, 19, 23, 25, 32, 664, DateTimeKind.Local).AddTicks(8876), "", 90, "Propiedad Real", 5, 200.0 },
                    { 2, "", "Detalle de Propiedad Barca...", new DateTime(2023, 7, 19, 23, 25, 32, 664, DateTimeKind.Local).AddTicks(8887), new DateTime(2023, 7, 19, 23, 25, 32, 664, DateTimeKind.Local).AddTicks(8886), "", 30, "Propiedad Barca", 3, 120.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Propiedades",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Propiedades",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
