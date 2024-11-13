using Microsoft.AspNetCore.Mvc.Rendering;

namespace turno_smart.ViewModels.TurnoVM
{
    public class EditTurnoVM
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public DateTime FechaTurno { get; set; }
        public List<SelectListItem> Medicos { get; set; } = [];
        public List<SelectListItem> Pacientes { get; set; } = [];
    }
}