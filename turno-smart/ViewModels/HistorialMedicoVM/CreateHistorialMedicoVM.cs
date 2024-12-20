namespace turno_smart.ViewModels.HistorialMedicoVM
{
    public class CreateHistorialMedicoVM
    {
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public string PacienteNombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Sintomas { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public string NotasAdicionales { get; set; }
        public string Prescripciones { get; set; }
        public string Seguimiento { get; set; }
    }
}