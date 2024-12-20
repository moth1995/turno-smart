using turno_smart.Data;
using turno_smart.Interfaces;
using turno_smart.Models;

namespace turno_smart.Services
{

    class EstudioService(ApplicationDbContext DBContext) : IEstudioService
    {
        private readonly ApplicationDbContext _DBContext = DBContext;

        public void Create(Estudio obj)
        {
            _DBContext.Add(obj);
            _DBContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var estudio = GetById(id);

            if (estudio != null){
                _DBContext.Remove(estudio);
                _DBContext.SaveChanges();
            }
        }

        public List<Estudio> GetAll(string filter)
        {
           var query = from estudio in _DBContext.Estudios select estudio;
            if (!string.IsNullOrEmpty(filter)) {

            var lowerFilter = filter.ToLower();
                query = query.Where(x => 
                    x.Descripcion.Contains(lowerFilter, StringComparison.CurrentCultureIgnoreCase)
                );
            }

            return [.. query];
        }

        public List<Estudio> GetAll()
        {
            List<Estudio> estudios = [.. (from estudio in _DBContext.Estudios select estudio)];
            return estudios;
        }

        public Estudio? GetById(int id)
        {
            var query = from estudio in _DBContext.Estudios select estudio;
            return query.FirstOrDefault(m => m.Id == id);
        }

        public void Update(Estudio obj)
        {
            _DBContext.Update(obj);
            _DBContext.SaveChanges();
        }
    }
}