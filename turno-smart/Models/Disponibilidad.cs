namespace turno_smart.Models
{
    public class Disponibilidad
    {
        public int Id { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
        public DateTime Dia { get; set; }
        public TimeSpan Hora { get; set; }
    }
}
