using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace turno_smart.Models
{   
    [Table("Medicos")]
    public class Medico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }

        [Display(Name = "Especialidad")]
        [ForeignKey(nameof(Especialidad))]
        public required int IdEspecialidad { get; set; }
        public int Telefono { get; set; }
        public required string Email { get; set; }
        public virtual Especialidad Especialidad { get; set; }
        public virtual List<Turno> Turnos { get; set; }
        public virtual List<HistorialMedico> HistorialMedico { get; set; }
    }
}
