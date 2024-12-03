using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropiedadesMinimalApi.Migrations
{
    /// <inheritdoc />
    public partial class Creacion_Inicial_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Propiedad",
                keyColumn: "IdPropiedad",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Propiedad",
                keyColumn: "IdPropiedad",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Propiedad",
                keyColumn: "IdPropiedad",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Propiedad",
                columns: new[] { "IdPropiedad", "Activa", "Descripcion", "FechaCreacion", "Nombre", "Ubicacion" },
                values: new object[,]
                {
                    { 1, true, "Amplia casa, recien remodelada, de dos planta", new DateTime(2024, 11, 25, 16, 37, 47, 536, DateTimeKind.Local).AddTicks(6775), "Casa en la torres", "Guadalajara" },
                    { 2, true, "Torre de departamentos", new DateTime(2024, 11, 24, 16, 37, 47, 538, DateTimeKind.Local).AddTicks(1694), "Departamentos Bimbo", "Tonala" },
                    { 3, true, "Terreno baldio en zona con alta plusvalia", new DateTime(2024, 11, 25, 16, 37, 47, 538, DateTimeKind.Local).AddTicks(1709), "Terreno baldio uno", "Zapopan" }
                });
        }
    }
}
