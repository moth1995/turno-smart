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
        public List<SelectListItem> Especialidad { get; set; } = [];
    }
}