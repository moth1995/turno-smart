using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace turno_smart.Models
{
    [Table("Pacientes")]
    public class Paciente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        public int DNI { get; set; }
        public required string Domicilio { get; set; }
        public required string Ciudad { get; set; }
        public required string Provincia { get; set; }
        public int Cobertura { get; set; }
        public int Telefono { get; set; }
        public required string Email { get; set; }
        public short Estado { get; set; }
        
        [Display(Name= "Fecha de alta")]
        public DateTime FechaAlta { get; set; }

        [Display(Name= "Fecha de baja")]
        public DateTime? FechaBaja { get; set; } // Nullable por si el paciente no tiene fecha de baja

        public virtual List<Turno> Turnos { get; set; }
        public virtual List<HistorialMedico> HistorialMedico { get; set; }
    }
}
