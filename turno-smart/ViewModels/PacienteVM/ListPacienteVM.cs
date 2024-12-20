using turno_smart.Models;

namespace turno_smart.ViewModels.PacienteVM;

public class ListPacienteVM
{
    public List<PacienteVM> Pacientes { get; set; } = [];
    public string? Filter { get; set; }
}