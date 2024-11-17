using turno_smart.Data;
using turno_smart.Interfaces;
using turno_smart.Models;

namespace turno_smart.Services
{
    class EspecialidadService(ApplicationDbContext DBContext) : IEspecialidadService
    {
        private readonly ApplicationDbContext _DBContext = DBContext;

        public void Create(Especialidad obj)
        {
            _DBContext.Add(obj);
            _DBContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var especialidad = GetById(id);

            if (especialidad != null){
                _DBContext.Remove(especialidad);
                _DBContext.SaveChanges();
            }
        }

        public List<Especialidad> GetAll(string? filter)
        {
           var query = from especialidad in _DBContext.Especialidades select especialidad;
            if (!string.IsNullOrEmpty(filter)) {

            var lowerFilter = filter.ToLower();
                query = query.Where(x => 
                    x.Nombre.Contains(lowerFilter, StringComparison.CurrentCultureIgnoreCase)
                );
            }

            return [.. query];
        }

        public List<Especialidad> GetAll()
        {
            List<Especialidad> medicos = [.. (from especialidad in _DBContext.Especialidades select especialidad)];
            return medicos;
        }

        public Especialidad? GetById(int id)
        {
            var query = from especialidad in _DBContext.Especialidades select especialidad;
            return query.FirstOrDefault(m => m.Id == id);
        }

        public void Update(Especialidad obj)
        {
            _DBContext.Update(obj);
            _DBContext.SaveChanges();
        }
    }
}