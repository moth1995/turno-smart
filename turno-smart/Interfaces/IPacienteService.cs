using turno_smart.Models;
namespace turno_smart.Interfaces;

public interface IPacienteService {
  void Create(Paciente obj);
  List<Paciente> GetAll(string filter);
    Paciente? GetByDNI(int dni);
  List<Paciente> GetAll();
  void Update(Paciente obj);
  void Delete(int id);
  Paciente? GetById(int id);
}