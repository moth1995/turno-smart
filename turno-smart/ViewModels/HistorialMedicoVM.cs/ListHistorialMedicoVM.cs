using turno_smart.Models;

namespace turno_smart.ViewModels.HistorialMedicoVM.cs
{
    public class ListHistorialMedicoVM
    {
        public List<HistorialMedico> HistorialesMedicos { get; set; } = [];
        public string? Filter { get; set; } 
    }
}