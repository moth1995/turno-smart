using turno_smart.Data;
using turno_smart.Interfaces;
using turno_smart.Models;

namespace turno_smart.Services
{

    class HistorialMedicoService(ApplicationDbContext DBContext) : IHistorialMedicoService
    {
        private readonly ApplicationDbContext _DBContext = DBContext;

        public void Create(HistorialMedico obj)
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

        public List<HistorialMedico> GetAll(string filter)
        {
           var query = from historial in _DBContext.HistorialesMedicos select historial;
            if (!string.IsNullOrEmpty(filter)) {

            var lowerFilter = filter.ToLower();
                query = query.Where(x => 
                    x.Paciente.Nombre.Contains(lowerFilter, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Paciente.Apellido.Contains(lowerFilter, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Paciente.DNI.ToString().Contains(lowerFilter, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Medico.Nombre.Contains(lowerFilter, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Medico.Apellido.Contains(lowerFilter, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Medico.Especialidad.Nombre.Contains(lowerFilter, StringComparison.CurrentCultureIgnoreCase) 
                    //|| x.Estudio.Descripcion.Contains(lowerFilter, StringComparison.CurrentCultureIgnoreCase)
                );
            }

            return [.. query];
        }

        public List<HistorialMedico> GetAll()
        {
            List<HistorialMedico> historiales = [.. (from historial in _DBContext.HistorialesMedicos select historial)];
            return historiales;
        }

        public HistorialMedico? GetById(int id)
        {
            var query = from historial in _DBContext.HistorialesMedicos select historial;
            return query.FirstOrDefault(m => m.Id == id);
        }

        public List<HistorialMedico?> GetByPacienteId(int id)
        {
            var query = from historial in _DBContext.HistorialesMedicos
                        where historial.IdPaciente == id
                        select historial;

            return query.ToList(); // Convierte la consulta a una lista
        }

        public void Update(HistorialMedico obj)
        {
            _DBContext.Update(obj);
            _DBContext.SaveChanges();
        }
    }
}