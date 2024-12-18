using turno_smart.Models;

namespace turno_smart.ViewModels.HistorialMedicoVM.cs
{
    public class ListHistorialMedicoVM
    {
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public List<HistorialMedico> HistorialesMedicos { get; set; } = [];
        public string? Filter { get; set; } 
    }
}