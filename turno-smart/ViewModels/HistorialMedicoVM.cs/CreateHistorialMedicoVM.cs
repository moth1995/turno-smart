using Microsoft.AspNetCore.Mvc.Rendering;

namespace turno_smart.ViewModels.HistorialMedicoVM.cs
{
    public class CreateHistorialMedicoVM
    {
        public int IdPaciente { get; set; }
        public int IdEstudio { get; set; }
        public int IdMedico { get; set; }
        public List<SelectListItem> Pacientes { get; set; } = [];
        public List<SelectListItem> Estudios { get; set; } = [];
        public List<SelectListItem> Medicos { get; set; } = [];
    }
}