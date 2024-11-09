using turno_smart.Data;
using turno_smart.Interfaces;
using turno_smart.Models;

namespace turno_smart.Services
{

    class PacienteService(ApplicationDbContext DBContext) : IPacienteService
    {
        private readonly ApplicationDbContext _DBContext = DBContext;

        public void Create(Paciente obj)
        {
            _DBContext.Add(obj);
            _DBContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var paciente = GetById(id);

            if (paciente != null){
                _DBContext.Remove(paciente);
                _DBContext.SaveChanges();
            }
        }

        public List<Paciente> GetAll(string filter)
        {
           var query = from paciente in _DBContext.Pacientes select paciente;
            if (!string.IsNullOrEmpty(filter)) {

            var lowerFilter = filter.ToLower();
                query = query.Where(x => 
                    x.Nombre.Contains(lowerFilter, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Apellido.Contains(lowerFilter, StringComparison.CurrentCultureIgnoreCase)
                );
            }

            return [.. query];
        }

        public List<Paciente> GetAll()
        {
            List<Paciente> pacientes = [.. (from paciente in _DBContext.Pacientes select paciente)];
            return pacientes;
        }

        public Paciente? GetById(int id)
        {
            var query = from paciente in _DBContext.Pacientes select paciente;
            return query.FirstOrDefault(m => m.Id == id);
        }

        public void Update(Paciente obj)
        {
            _DBContext.Update(obj);
            _DBContext.SaveChanges();
        }
    }
}