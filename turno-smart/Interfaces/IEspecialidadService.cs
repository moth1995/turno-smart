using turno_smart.Models;
namespace turno_smart.Interfaces;

public interface IEspecialidadService {
  void Create(Especialidad obj);
  Task<List<Especialidad>> GetAll(string? filter);
  List<Especialidad> GetAll();
  void Update(Especialidad obj);
  void Delete(int id);
  Especialidad? GetById(int id);
}