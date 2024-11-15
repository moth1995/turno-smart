using turno_smart.Data;
using turno_smart.Interfaces;
using turno_smart.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using turno_smart.ViewModels.PacienteVM;

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

        public async Task<List<PacienteViewModel>> BuscarPacientes(string nombre, string apellido, int? dni)
        {
            var query = _DBContext.Pacientes.AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(p => p.Nombre.Contains(nombre));
            }
            if (!string.IsNullOrEmpty(apellido))
            {
                query = query.Where(p => p.Apellido.Contains(apellido));
            }
            if (dni.HasValue)
            {
                query = query.Where(p => p.DNI == dni.Value);
            }

            var pacientes = await query.Select(p => new PacienteViewModel
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                FechaNacimiento = p.FechaNacimiento,
                Dni = p.DNI,
                Domicilio = p.Domicilio,
                Ciudad = p.Ciudad,
                Provincia = p.Provincia,
                Cobertura = p.Cobertura,
                Telefono = p.Telefono,
                Email = p.Email,
                Estado = p.Estado,
                FechaAlta = p.FechaAlta,
                FechaBaja = p.FechaBaja
            }).ToListAsync();

            return pacientes;
        }
    }
}