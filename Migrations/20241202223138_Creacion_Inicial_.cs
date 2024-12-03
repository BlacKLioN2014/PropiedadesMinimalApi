using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropiedadesMinimalApi.Migrations
{
    /// <inheritdoc />
    public partial class Creacion_Inicial_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Propiedad",
                columns: table => new
                {
                    IdPropiedad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activa = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propiedad", x => x.IdPropiedad);
                });

            migrationBuilder.InsertData(
                table: "Propiedad",
                columns: new[] { "IdPropiedad", "Activa", "Descripcion", "FechaCreacion", "Nombre", "Ubicacion" },
                values: new object[,]
                {
                    { 1, true, "Amplia casa, recien remodelada, de dos planta", new DateTime(2024, 11, 25, 16, 31, 37, 674, DateTimeKind.Local).AddTicks(7331), "Casa en la torres", "Guadalajara" },
                    { 2, true, "Torre de departamentos", new DateTime(2024, 11, 24, 16, 31, 37, 677, DateTimeKind.Local).AddTicks(6050), "Departamentos Bimbo", "Tonala" },
                    { 3, true, "Terreno baldio en zona con alta plusvalia", new DateTime(2024, 11, 25, 16, 31, 37, 677, DateTimeKind.Local).AddTicks(6074), "Terreno baldio uno", "Zapopan" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Propiedad");
        }
    }
}
