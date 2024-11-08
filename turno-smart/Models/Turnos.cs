using turno_smart.Data.Migrations;

namespace turno_smart.Models
{
    public class Turnos
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public DateTime FechaTurno { get; set; }
    }
}
