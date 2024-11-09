using turno_smart.Models;
namespace turno_smart.Interfaces;

public interface ITurnoService {
  void Create(Turno obj);
  List<Turno> GetAll(string filter);
  List<Turno> GetAll();
  void Update(Turno obj);
  void Delete(int id);
  Turno? GetById(int id);
}