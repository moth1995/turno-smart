using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace turno_smart.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHistorialMedicoEstudiosEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicos_Especialidades_EspecialidadId",
                table: "Medicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Medicos_MedicoId",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Pacientes_PacienteId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_MedicoId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_PacienteId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Medicos_EspecialidadId",
                table: "Medicos");

            migrationBuilder.DropColumn(
                name: "MedicoId",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "EspecialidadId",
                table: "Medicos");

            migrationBuilder.CreateTable(
                name: "Estudios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistorialesMedicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    IdEstudio = table.Column<int>(type: "int", nullable: false),
                    IdMedico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialesMedicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialesMedicos_Estudios_IdEstudio",
                        column: x => x.IdEstudio,
                        principalTable: "Estudios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistorialesMedicos_Medicos_IdMedico",
                        column: x => x.IdMedico,
                        principalTable: "Medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistorialesMedicos_Pacientes_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_IdMedico",
                table: "Turnos",
                column: "IdMedico");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_IdPaciente",
                table: "Turnos",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_IdEspecialidad",
                table: "Medicos",
                column: "IdEspecialidad");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialesMedicos_IdEstudio",
                table: "HistorialesMedicos",
                column: "IdEstudio");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialesMedicos_IdMedico",
                table: "HistorialesMedicos",
                column: "IdMedico");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialesMedicos_IdPaciente",
                table: "HistorialesMedicos",
                column: "IdPaciente");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicos_Especialidades_IdEspecialidad",
                table: "Medicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Medicos_IdMedico",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Pacientes_IdPaciente",
                table: "Turnos");

            migrationBuilder.DropTable(
                name: "HistorialesMedicos");

            migrationBuilder.DropTable(
                name: "Estudios");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_IdMedico",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_IdPaciente",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Medicos_IdEspecialidad",
                table: "Medicos");

            migrationBuilder.AddColumn<int>(
                name: "MedicoId",
                table: "Turnos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "Turnos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EspecialidadId",
                table: "Medicos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_MedicoId",
                table: "Turnos",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_PacienteId",
                table: "Turnos",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_EspecialidadId",
                table: "Medicos",
                column: "EspecialidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicos_Especialidades_EspecialidadId",
                table: "Medicos",
                column: "EspecialidadId",
                principalTable: "Especialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Medicos_MedicoId",
                table: "Turnos",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Pacientes_PacienteId",
                table: "Turnos",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
