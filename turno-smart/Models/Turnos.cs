using turno_smart.Data.Migrations;

namespace turno_smart.Models
{
    public class Turnos
    {
        public int Id_Turno { get; set; }
        public int Id_Paciente { get; set; }
        public int Id_Medico { get; set; }
        public DateTime Fecha_Turno { get; set; }
    }
}
