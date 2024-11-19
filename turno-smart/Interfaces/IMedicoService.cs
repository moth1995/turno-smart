using turno_smart.Models;
namespace turno_smart.Interfaces;

public interface IMedicoService {
  void Create(Medico obj);
  List<Medico> GetAll(string filter);
  List<Medico> GetAll();
  void Update(Medico obj);
  void Delete(int id);
  Medico? GetById(int id);

  List<DateTime> GetDiasDisponibles(int especialidadId);
  List<Medico> GetMedicosDisponibles(int especialidadId, DateTime dia);
  List<TimeSpan> GetHorariosDisponibles(int medicoId, DateTime dia);
  void ConfirmarTurno(int medicoId, DateTime dia, TimeSpan hora);
}