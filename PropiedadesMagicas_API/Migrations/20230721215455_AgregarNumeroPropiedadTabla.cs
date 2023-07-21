using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropiedadesMagicas_API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNumeroPropiedadTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeroPropiedades",
                columns: table => new
                {
                    PropiedadNum = table.Column<int>(type: "int", nullable: false),
                    PropiedadId = table.Column<int>(type: "int", nullable: false),
                    DetallesEspeciales = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroPropiedades", x => x.PropiedadNum);
                    table.ForeignKey(
                        name: "FK_NumeroPropiedades_Propiedades_PropiedadId",
                        column: x => x.PropiedadId,
                        principalTable: "Propiedades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Propiedades",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 7, 21, 18, 54, 55, 689, DateTimeKind.Local).AddTicks(7998), new DateTime(2023, 7, 21, 18, 54, 55, 689, DateTimeKind.Local).AddTicks(7989) });

            migrationBuilder.UpdateData(
                table: "Propiedades",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 7, 21, 18, 54, 55, 689, DateTimeKind.Local).AddTicks(8001), new DateTime(2023, 7, 21, 18, 54, 55, 689, DateTimeKind.Local).AddTicks(8001) });

            migrationBuilder.CreateIndex(
                name: "IX_NumeroPropiedades_PropiedadId",
                table: "NumeroPropiedades",
                column: "PropiedadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroPropiedades");

            migrationBuilder.UpdateData(
                table: "Propiedades",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 7, 19, 23, 25, 32, 664, DateTimeKind.Local).AddTicks(8883), new DateTime(2023, 7, 19, 23, 25, 32, 664, DateTimeKind.Local).AddTicks(8876) });

            migrationBuilder.UpdateData(
                table: "Propiedades",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 7, 19, 23, 25, 32, 664, DateTimeKind.Local).AddTicks(8887), new DateTime(2023, 7, 19, 23, 25, 32, 664, DateTimeKind.Local).AddTicks(8886) });
        }
    }
}
