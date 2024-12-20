namespace turno_smart.ViewModels.TurnoVM
{
    public class ListTurnosVM
    {
        public List<TurnoVM> Turnos { get; set; } = [];
        public DateTime CurrentDate { get; set; }
        public string? Filter { get; set; }
    }
}