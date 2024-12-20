namespace turno_smart.ViewModels.CreateRecepcionistaVM;

public class CreateRecepcionistaVM {

    public int Id { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public int DNI { get; set; }
    public required string Email { get; set; }
}