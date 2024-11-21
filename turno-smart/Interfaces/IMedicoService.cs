using turno_smart.Models;
namespace turno_smart.Interfaces;

public interface IMedicoService {
    void Create(Medico obj);
    Task<List<Medico>> GetAll(string? filter);
    Task<List<Medico>> GetAll(int especialidadId);
    List<Medico> GetAll();
    void Update(Medico obj);
    void Delete(int id);
    Medico? GetById(int id);
    Task<Dictionary<DateTime, List<TimeSpan>>> GetAvailableSlotsAsync(int medicoId, int maxDays, TimeSpan startTime, TimeSpan endTime, TimeSpan slotLenght);
}