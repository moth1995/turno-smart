using System.ComponentModel.DataAnnotations;

namespace turno_smart.ViewModels.PacienteVM
{
    public class CreatePacienteVM
    {
        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Apellido es obligatorio.")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "La Fecha de nacimiento es obligatoria.")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El DNI es obligatorio.")]
        public int DNI { get; set; }
        [Required(ErrorMessage = "El Domicilio es obligatorio.")]
        public string Domicilio { get; set; }
        [Required(ErrorMessage = "La Ciudad es obligatoria.")]
        public string Ciudad { get; set; }
        [Required(ErrorMessage = "La Provincia es obligatoria.")]
        public string Provincia { get; set; }
        public int Cobertura { get; set; }
        [Required(ErrorMessage = "El Telefono es obligatorio.")]
        public int Telefono { get; set; }
        [Required(ErrorMessage = "El Email es obligatorio.")]
        public string Email { get; set; }
        public short Estado { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
