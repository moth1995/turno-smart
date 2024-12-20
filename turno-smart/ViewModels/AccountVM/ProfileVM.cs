using Microsoft.AspNetCore.Identity;

namespace turno_smart.ViewModels.AccountVM;

public class ProfileVM {
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public int DNI { get; set; }
    public string Domicilio { get; set; }
    public string Ciudad { get; set; }
    public string Provincia { get; set; }
    public int Telefono { get; set; }
    public required string Email { get; set; }
    public string Password { get; set; }
    public string ActiveTab { get; set; }
}