using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace turno_smart.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionDeTableTurnos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MotivoConsulta",
                table: "Turnos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotivoConsulta",
                table: "Turnos");
        }
    }
}
