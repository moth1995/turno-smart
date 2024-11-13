namespace turno_smart.ViewModels.TurnoVM
{
    public class DeleteTurnoVM
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public DateTime FechaTurno { get; set; }
    }
}