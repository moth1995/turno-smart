namespace turno_smart.ViewModels.PacienteVM
{
    public class CreatePacienteVM
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int DNI { get; set; }
        public required string Domicilio { get; set; }
        public required string Ciudad { get; set; }
        public required string Provincia { get; set; }
        public int Cobertura { get; set; }
        public int Telefono { get; set; }
        public required string Email { get; set; }
        public short Estado { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
