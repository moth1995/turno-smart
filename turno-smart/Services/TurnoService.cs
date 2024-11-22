using Microsoft.EntityFrameworkCore;
using turno_smart.Data;
using turno_smart.Interfaces;
using turno_smart.Models;

namespace turno_smart.Services
{

    class TurnoService(ApplicationDbContext DBContext) : ITurnoService
    {
        private readonly ApplicationDbContext _DBContext = DBContext;

        public void Create(Turno obj)
        {
            _DBContext.Add(obj);
            _DBContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var turno = GetById(id);

            if (turno != null){
                _DBContext.Remove(turno);
                _DBContext.SaveChanges();
            }
        }

        public async Task<List<Turno>> GetAll(string? filter, int? pacienteId, int? medicoId)
        {
            var query = from turno in _DBContext.Turnos select turno;

            if (pacienteId != null)
            {
                query = query.Where(t =>
                    t.IdPaciente == pacienteId
                );
            }
            else if (medicoId != null) 
            {
                query = query.Where(t =>
                    t.IdMedico == medicoId
                );
            }
            else if (!string.IsNullOrEmpty(filter)) 
            {
                query = query.Where(x =>
                    x.Medico.Nombre.Contains(filter.ToLower()) ||
                    x.Paciente.Nombre.Contains(filter.ToLower())
                );
            }

            return await query.ToListAsync();
        }

        public List<Turno> GetAll()
        {
            List<Turno> turnos = [.. (from turno in _DBContext.Turnos select turno)];
            return turnos;
        }

        public Turno? GetById(int id)
        {
            var query = from turno in _DBContext.Turnos select turno;
            return query.FirstOrDefault(m => m.Id == id);
        }

        public void Update(Turno obj)
        {
            _DBContext.Update(obj);
            _DBContext.SaveChanges();
        }
    }
}