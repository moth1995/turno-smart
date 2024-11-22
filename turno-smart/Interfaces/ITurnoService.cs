using turno_smart.Models;
namespace turno_smart.Interfaces;

public interface ITurnoService {
    void Create(Turno obj);
    Task<List<Turno>> GetAll(string? filter, int? pacienteId, int? medicoId);
    List<Turno> GetAll();
    void Update(Turno obj);
    void Delete(int id);
    Turno? GetById(int id);
}