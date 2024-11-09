using turno_smart.Models;
namespace turno_smart.ViewModels.MedicoVM;

public class ListMedicoVM {

    public List<Medico> Medicos { get; set; } = [];

    public string? Filter { get; set; }

}