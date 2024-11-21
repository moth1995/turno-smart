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
    }
}