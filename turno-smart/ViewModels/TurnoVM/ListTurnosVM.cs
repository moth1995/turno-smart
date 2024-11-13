using turno_smart.Models;

namespace turno_smart.ViewModels.TurnoVM
{
    public class ListTurnosVM
    {
        public List<Turno> Turnos { get; set; } = [];
        public string? Filter { get; set; }
    }
}