namespace turno_smart.ViewModels.RecepcionistaVM
{
    public class RecepcionistaVM
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public int DNI { get; set; }
        public required string Email { get; set; }
        public short Estado { get; set; }

    }
}

