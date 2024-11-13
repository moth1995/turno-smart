using Microsoft.AspNetCore.Mvc.Rendering;

namespace turno_smart.ViewModels.TurnoVM
{
    public class CreateTurnoVM
    {
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public DateTime FechaTurno { get; set; }
        public List<SelectListItem> Medicos { get; set; } = [];
    }
}