namespace turno_smart.Models
{
    public class Pacientes
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Dni { get; set; }
        public string Domicilio { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public int Cobertura { get; set; }
        public int Telefono { get; set; }
        public string Email { get; set; }
        public short Estado { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; } // Nullable por si el paciente no tiene fecha de baja
    }
}
