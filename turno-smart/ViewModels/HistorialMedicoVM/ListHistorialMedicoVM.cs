using turno_smart.Models;

namespace turno_smart.ViewModels.HistorialMedicoVM
{
    public class ListHistorialMedicoVM
    {
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public List<HistorialMedico> HistorialesMedicos { get; set; } = [];
        public int? SelectedMedicoId { get; set; }
        public List<Medico> Medicos { get; set; }
        public string? Filter { get; set; } 
    }
}