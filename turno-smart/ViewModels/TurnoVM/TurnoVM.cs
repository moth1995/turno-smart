namespace turno_smart.ViewModels.TurnoVM
{
    public class TurnoVM
    {
        public int TurnoId { get; set; }
        public int PacienteId { get; set; }
        public string PacienteNombre { get; set; }
        public string MedicoEspecialidad { get; set; }
        public string MedicoNombre { get; set; }
        public int MedicoId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string MotivoConsulta { get; set; }
        public string Estado { get; set; }
    }
}
