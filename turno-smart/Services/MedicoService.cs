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

        public List<Medico> GetAll(string filter)
        {
           var query = from medico in _DBContext.Medicos select medico;
            if (!string.IsNullOrEmpty(filter)) {

            var lowerFilter = filter.ToLower();
                query = query.Where(x => 
                    x.Nombre.Contains(lowerFilter, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Apellido.Contains(lowerFilter, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Especialidad.Nombre.Contains(lowerFilter, StringComparison.CurrentCultureIgnoreCase)
                );
            }

            return [.. query];
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