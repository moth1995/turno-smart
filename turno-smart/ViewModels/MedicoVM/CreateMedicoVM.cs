using Microsoft.AspNetCore.Mvc.Rendering;

namespace turno_smart.ViewModels.MedicoVM
{
    public class CreateMedicoVM
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required int IdEspecialidad { get; set; }
        public required int DNI {  get; set; }
        public required int Telefono { get; set; }
        public required string Email { get; set;}
        public required string Reseña { get; set; }
        public required string Imagen { get; set; }
        public required int Matricula { get; set; }
        public List<SelectListItem> Especialidad { get; set; } = [];
    }
}