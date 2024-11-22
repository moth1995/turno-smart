using turno_smart.Data;
using turno_smart.Interfaces;
using turno_smart.Models;

namespace turno_smart.Services
{

    public class PacienteService(ApplicationDbContext DBContext) : IPacienteService
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
                    x.Nombre.ToLower().Contains(lowerFilter) ||
                    x.Apellido.ToLower().Contains(lowerFilter) ||
                    x.DNI.ToString().Contains(lowerFilter)
                );
            }

            return query.ToList();
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

        public Paciente? GetByDNI(int dni)
        {
            var query = from paciente in _DBContext.Pacientes select paciente;
            return query.FirstOrDefault(p => p.DNI == dni);
        }

        public void Update(Paciente obj)
        {
            _DBContext.Update(obj);
            _DBContext.SaveChanges();
        }
    }
}