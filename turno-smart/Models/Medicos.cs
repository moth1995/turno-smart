using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace turno_smart.Models
{   
    [Table("Medicos")]
    public class Medico
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public int DNI { get; set; }
        public required int IdEspecialidad { get; set; }
        public int Telefono { get; set; }
        public required string Email { get; set; }
        public string? Reseña { get; set; }
        public string? Imagen { get; set; }
        public virtual Especialidad Especialidad { get; set; }
        public virtual List<Turno> Turnos { get; set; }
        public virtual List<HistorialMedico> HistorialMedico { get; set; }
        public virtual Usuarios Usuario { get; set; }
        public int Matricula { get; set; }
        public string FullName()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}
