using System.ComponentModel.DataAnnotations;

namespace turno_smart.ViewModels.TurnoVM
{
    public class CreateTurnoVM
    {
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public string MedicoNombre { get; set; }
        public string MedicoEspecialidad { get; set; }
        public List<DateTime> AvailableDates { get; set; } = new List<DateTime>();
        public Dictionary<DateTime, List<TimeSpan>> AvailableSlots { get; set; } = new Dictionary<DateTime, List<TimeSpan>>();

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public string SelectedDate { get; set; }

        [Required(ErrorMessage = "El horario es obligatorio.")]
        public string SelectedTime { get; set; }
        public string MotivoConsulta { get; set; }
    }
}