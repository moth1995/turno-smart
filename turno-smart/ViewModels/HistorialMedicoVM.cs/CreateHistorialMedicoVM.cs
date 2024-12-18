using Microsoft.AspNetCore.Mvc.Rendering;
using turno_smart.Models;

namespace turno_smart.ViewModels.HistorialMedicoVM.cs
{
    public class CreateHistorialMedicoVM
    {
        public int IdPaciente { get; set; }
        public int IdEstudio { get; set; }
        public int IdMedico { get; set; }
        public DateTime Fecha { get; set; }
        public string Sintomas { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public string NotasAdicionales { get; set; }
        public Paciente Paciente { get; set; }
        public List<SelectListItem> Estudios { get; set; } = [];
        public Medico Medico { get; set; }
    }
}