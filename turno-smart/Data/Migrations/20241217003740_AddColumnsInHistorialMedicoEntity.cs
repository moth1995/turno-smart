using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace turno_smart.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsInHistorialMedicoEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Diagnostico",
                table: "HistorialesMedicos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "HistorialesMedicos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NotasAdicionales",
                table: "HistorialesMedicos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sintomas",
                table: "HistorialesMedicos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tratamiento",
                table: "HistorialesMedicos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diagnostico",
                table: "HistorialesMedicos");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "HistorialesMedicos");

            migrationBuilder.DropColumn(
                name: "NotasAdicionales",
                table: "HistorialesMedicos");

            migrationBuilder.DropColumn(
                name: "Sintomas",
                table: "HistorialesMedicos");

            migrationBuilder.DropColumn(
                name: "Tratamiento",
                table: "HistorialesMedicos");
        }
    }
}
