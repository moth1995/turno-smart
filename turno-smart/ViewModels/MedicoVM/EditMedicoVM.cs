using Microsoft.AspNetCore.Mvc.Rendering;

namespace turno_smart.ViewModels.MedicoVM
{
    public class EditMedicoVM
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int IdEspecialidad { get; set; }
        public int? Telefono { get; set; }
        public string? Email { get; set;}
        public string? Reseña { get; set; }
        public string? Imagen { get; set; }
        public int? Matricula { get; set; }
        public List<SelectListItem> Especialidad { get; set; } = [];
    }
}