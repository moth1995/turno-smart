using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace turno_smart.Models
{
    [Table("Pacientes")]
    public class Paciente
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int DNI { get; set; }
        public string Domicilio { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public int Cobertura { get; set; }
        public int Telefono { get; set; }
        public required string Email { get; set; }
        public short Estado { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; } // Nullable por si el paciente no tiene fecha de baja
        public virtual List<Turno> Turnos { get; set; }
        public virtual List<HistorialMedico> HistorialMedico { get; set; }
        public virtual Usuarios Usuario { get; set; }

        public string FullName()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}
