using turno_smart.Models;
namespace turno_smart.Interfaces;

public interface IRecepcionistaService {
    void Create(Recepcionista obj);
    List<Recepcionista> GetAll(string filter);
    List<Recepcionista> GetAll();
    void Update(Recepcionista obj);
    void Delete(int id);
    Recepcionista? GetById(int id);
}