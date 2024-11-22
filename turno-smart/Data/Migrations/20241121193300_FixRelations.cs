using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace turno_smart.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicoId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DNI",
                table: "AspNetUsers",
                column: "DNI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MedicoId",
                table: "AspNetUsers",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PacienteId",
                table: "AspNetUsers",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Medicos_MedicoId",
                table: "AspNetUsers",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Pacientes_PacienteId",
                table: "AspNetUsers",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Medicos_MedicoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Pacientes_PacienteId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DNI",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MedicoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PacienteId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MedicoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "AspNetUsers");
        }
    }
}
