using turno_smart.Models;

namespace turno_smart.ViewModels.EstudioVM
{
    public class ListEstudioVM
    {
        public List<Estudio> Estudios { get; set; } = [];
        public string? Filter { get; set; }
    }
}