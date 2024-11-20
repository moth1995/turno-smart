using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace turno_smart.Models
{
    [Table("HistorialesMedicos")]
    public class HistorialMedico
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdEstudio { get; set; }
        public int IdMedico { get; set; }
        public virtual Paciente Paciente { get; set; }
        public virtual Estudio Estudio { get; set; }
        public virtual Medico Medico { get; set; }
    }

}
