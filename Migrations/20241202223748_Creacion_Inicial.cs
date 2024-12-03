using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropiedadesMinimalApi.Migrations
{
    /// <inheritdoc />
    public partial class Creacion_Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Propiedad",
                keyColumn: "IdPropiedad",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2024, 11, 25, 16, 37, 47, 536, DateTimeKind.Local).AddTicks(6775));

            migrationBuilder.UpdateData(
                table: "Propiedad",
                keyColumn: "IdPropiedad",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2024, 11, 24, 16, 37, 47, 538, DateTimeKind.Local).AddTicks(1694));

            migrationBuilder.UpdateData(
                table: "Propiedad",
                keyColumn: "IdPropiedad",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2024, 11, 25, 16, 37, 47, 538, DateTimeKind.Local).AddTicks(1709));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Propiedad",
                keyColumn: "IdPropiedad",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2024, 11, 25, 16, 33, 41, 714, DateTimeKind.Local).AddTicks(831));

            migrationBuilder.UpdateData(
                table: "Propiedad",
                keyColumn: "IdPropiedad",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2024, 11, 24, 16, 33, 41, 715, DateTimeKind.Local).AddTicks(4239));

            migrationBuilder.UpdateData(
                table: "Propiedad",
                keyColumn: "IdPropiedad",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2024, 11, 25, 16, 33, 41, 715, DateTimeKind.Local).AddTicks(4252));
        }
    }
}
