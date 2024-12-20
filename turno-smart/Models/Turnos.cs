using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using turno_smart.Data.Migrations;

namespace turno_smart.Models
{
    [Table("Turnos")]
    public class Turno
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public string Estado { get; set; }
        public DateTime FechaTurno { get; set; }
        public string MotivoConsulta { get; set; }
        public virtual Paciente Paciente { get; set; }
        public virtual Medico Medico { get; set; }
    }
}
