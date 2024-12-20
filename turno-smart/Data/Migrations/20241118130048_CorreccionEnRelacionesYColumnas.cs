using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace turno_smart.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionEnRelacionesYColumnas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistorialesMedicos_Estudios_IdEstudio",
                table: "HistorialesMedicos");

            migrationBuilder.DropForeignKey(
                name: "FK_HistorialesMedicos_Medicos_IdMedico",
                table: "HistorialesMedicos");

            migrationBuilder.DropForeignKey(
                name: "FK_HistorialesMedicos_Pacientes_IdPaciente",
                table: "HistorialesMedicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicos_Especialidades_IdEspecialidad",
                table: "Medicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Medicos_IdMedico",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Pacientes_IdPaciente",
                table: "Turnos");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Turnos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DNI",
                table: "Medicos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_DNI",
                table: "AspNetUsers",
                column: "DNI");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_DNI",
                table: "Pacientes",
                column: "DNI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_DNI",
                table: "Medicos",
                column: "DNI",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HistorialesMedicos_Estudios_IdEstudio",
                table: "HistorialesMedicos",
                column: "IdEstudio",
                principalTable: "Estudios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HistorialesMedicos_Medicos_IdMedico",
                table: "HistorialesMedicos",
                column: "IdMedico",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HistorialesMedicos_Pacientes_IdPaciente",
                table: "HistorialesMedicos",
                column: "IdPaciente",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicos_AspNetUsers_DNI",
                table: "Medicos",
                column: "DNI",
                principalTable: "AspNetUsers",
                principalColumn: "DNI",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicos_Especialidades_IdEspecialidad",
                table: "Medicos",
                column: "IdEspecialidad",
                principalTable: "Especialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_AspNetUsers_DNI",
                table: "Pacientes",
                column: "DNI",
                principalTable: "AspNetUsers",
                principalColumn: "DNI",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Medicos_IdMedico",
                table: "Turnos",
                column: "IdMedico",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Pacientes_IdPaciente",
                table: "Turnos",
                column: "IdPaciente",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistorialesMedicos_Estudios_IdEstudio",
                table: "HistorialesMedicos");

            migrationBuilder.DropForeignKey(
                name: "FK_HistorialesMedicos_Medicos_IdMedico",
                table: "HistorialesMedicos");

            migrationBuilder.DropForeignKey(
                name: "FK_HistorialesMedicos_Pacientes_IdPaciente",
                table: "HistorialesMedicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicos_AspNetUsers_DNI",
                table: "Medicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicos_Especialidades_IdEspecialidad",
                table: "Medicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_AspNetUsers_DNI",
                table: "Pacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Medicos_IdMedico",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Pacientes_IdPaciente",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_DNI",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Medicos_DNI",
                table: "Medicos");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_DNI",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "DNI",
                table: "Medicos");

            migrationBuilder.AddForeignKey(
                name: "FK_HistorialesMedicos_Estudios_IdEstudio",
                table: "HistorialesMedicos",
                column: "IdEstudio",
                principalTable: "Estudios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistorialesMedicos_Medicos_IdMedico",
                table: "HistorialesMedicos",
                column: "IdMedico",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistorialesMedicos_Pacientes_IdPaciente",
                table: "HistorialesMedicos",
                column: "IdPaciente",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicos_Especialidades_IdEspecialidad",
                table: "Medicos",
                column: "IdEspecialidad",
                principalTable: "Especialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Medicos_IdMedico",
                table: "Turnos",
                column: "IdMedico",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Pacientes_IdPaciente",
                table: "Turnos",
                column: "IdPaciente",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
