using turno_smart.Models;
namespace turno_smart.Interfaces;

public interface IEstudioService {
  void Create(Estudio obj);
  List<Estudio> GetAll(string filter);
  List<Estudio> GetAll();
  void Update(Estudio obj);
  void Delete(int id);
  Estudio? GetById(int id);
}