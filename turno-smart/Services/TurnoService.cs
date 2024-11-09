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

        public List<Turno> GetAll(string filter)
        {
           var query = from turno in _DBContext.Turnos select turno;
            if (!string.IsNullOrEmpty(filter)) {

                query = query.Where(x => 
                    x.Medico.Nombre.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Paciente.Nombre.Contains(filter, StringComparison.CurrentCultureIgnoreCase)
                )
                .Include(t => t.Medico)
                .Include(t => t.Paciente);
            }

            return [.. query];
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