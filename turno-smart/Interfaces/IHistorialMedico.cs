using turno_smart.Models;
namespace turno_smart.Interfaces;

public interface IHistorialMedicoService {
  void Create(HistorialMedico obj);
  List<HistorialMedico> GetAll(string filter);
  List<HistorialMedico> GetAll();
  void Update(HistorialMedico obj);
  void Delete(int id);
  HistorialMedico? GetById(int id);
  List<HistorialMedico>? GetByPacienteId(int id);
}