using Microsoft.EntityFrameworkCore;
using turno_smart.Data;
using turno_smart.Interfaces;
using turno_smart.Models;

namespace turno_smart.Services
{
    class MedicoService(ApplicationDbContext DBContext) : IMedicoService
    {
        private readonly ApplicationDbContext _DBContext = DBContext;

        public void Create(Medico obj)
        {
            _DBContext.Add(obj);
            _DBContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var medico = GetById(id);

            if (medico != null){
                _DBContext.Remove(medico);
                _DBContext.SaveChanges();
            }
        }

        public async Task<List<Medico>> GetAll(string? filter)
        {
           var query = from medico in _DBContext.Medicos select medico;
            if (!string.IsNullOrEmpty(filter)) {

                string lowerFilter = filter.ToLower();
                query = query.Where(x => 
                    x.Nombre.ToLower().Contains(lowerFilter) ||
                    x.Apellido.ToLower().Contains(lowerFilter) ||
                    x.Especialidad.Nombre.ToLower().Contains(lowerFilter)
                );
            }

            return await query.ToListAsync();
        }

        public List<Medico> GetAll()
        {
            List<Medico> medicos = [.. (from medico in _DBContext.Medicos select medico)];
            return medicos;
        }

        public async Task<List<Medico>> GetAll(int especialidadId)
        {
            var query = from medico in _DBContext.Medicos select medico;
            query = query.Where(x =>
                x.Especialidad.Id == especialidadId
            );

            return await query.ToListAsync();
        }

        public Medico? GetById(int id)
        {
            var query = from medico in _DBContext.Medicos select medico;
            return query.FirstOrDefault(m => m.Id == id);
        }

        public void Update(Medico obj)
        {
            _DBContext.Update(obj);
            _DBContext.SaveChanges();
        }

        public async Task<Dictionary<DateTime, List<TimeSpan>>> GetAvailableSlotsAsync(int medicoId, int maxDays, TimeSpan startTime, TimeSpan endTime, TimeSpan slotLenght)
        {
            // Obtén al médico por ID
            var medico = await _DBContext.Medicos
                .FirstOrDefaultAsync(m => m.Id == medicoId);

            if (medico == null)
            {
                throw new Exception("Médico no encontrado.");
            }

            // Obtener los turnos existentes del médico en el rango de días solicitado
            var turnos = await _DBContext.Turnos
                .Where(t => t.IdMedico == medicoId && t.FechaTurno >= DateTime.Today && t.FechaTurno <= DateTime.Today.AddDays(maxDays))
                .ToListAsync();

            // Crear un diccionario para almacenar las fechas y los horarios disponibles
            var availableSlots = new Dictionary<DateTime, List<TimeSpan>>();

            // Para cada día en el rango de maxDays
            for (int i = 0; i < maxDays; i++)
            {
                var date = DateTime.Today.AddDays(i).Date;
                var availableTimes = new List<TimeSpan>();

                for (var currentTime = startTime; currentTime < endTime; currentTime = currentTime.Add(slotLenght))
                {
                    // Comprobar si el turno ya está reservado en esa fecha y hora
                    var isSlotTaken = turnos.Any(t => t.FechaTurno.Date == date.Date && t.FechaTurno.TimeOfDay == currentTime);

                    if (!isSlotTaken)
                    {
                        availableTimes.Add(currentTime);
                    }
                }

                // Solo agregar al diccionario si hay horarios disponibles
                if (availableTimes.Count > 0)
                {
                    availableSlots[date] = availableTimes;
                }
            }

            return availableSlots;
        }
    }
}