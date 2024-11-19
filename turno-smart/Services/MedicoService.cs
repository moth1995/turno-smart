using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using turno_smart.Data;
using turno_smart.Interfaces;
using turno_smart.Models;

namespace turno_smart.Services
{
    class MedicoService : IMedicoService
    {
        private readonly ApplicationDbContext _DBContext ;
        //private readonly ApplicationDbContext _context;
        public MedicoService(ApplicationDbContext context)
        {
            _DBContext = context;
        }


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

        public List<DateTime> GetDiasDisponibles(int especialidadId)
        {
            // Implementación para obtener los días disponibles
            //return _DBContext.Medicos
            //    .Where(m => m.EspecialidadId == especialidadId)
            //    .SelectMany(m => m.Disponibilidades)
            //    .Select(d => d.Dia)
            //    .Distinct()
            //    .ToList();
            // Hardcodear días disponibles
            return new List<DateTime>
             {
                 new DateTime(2024, 11, 20),
                 new DateTime(2024, 11, 21),
                 new DateTime(2024, 11, 22)
             };


        }

        public List<Medico> GetMedicosDisponibles(int especialidadId, DateTime dia)
        {
            // Implementación para obtener los médicos disponibles
            //return _DBContext.Medicos
            //    .Where(m => m.EspecialidadId == especialidadId && m.Disponibilidades.Any(d => d.Dia == dia))
            //    .ToList();
            // Hardcodear médicos disponibles
            // Hardcodear médicos disponibles
            return new List<Medico>
    {
        new Medico { Id = 1, Nombre = "Dr. Juan Pérez", EspecialidadId = especialidadId, Apellido = "Pérez", Disponibilidades = new List<Disponibilidad>(), Email = "juan@hotmail.com" },
        new Medico { Id = 2, Nombre = "Dra. María López", EspecialidadId = especialidadId, Apellido = "López", Disponibilidades = new List<Disponibilidad>(), Email = "Maria@hotmial.com" }
    };

        }

        public List<TimeSpan> GetHorariosDisponibles(int medicoId, DateTime dia)
        {
            // Implementación para obtener los horarios disponibles
            //return _DBContext.Disponibilidades
            //    .Where(d => d.MedicoId == medicoId && d.Dia == dia)
            //    .Select(d => d.Hora)
            //    .ToList();

                // Hardcodear horarios disponibles
             return new List<TimeSpan>
             {
                new TimeSpan(9, 0, 0),
                new TimeSpan(10, 0, 0),
                new TimeSpan(11, 0, 0)
             };
        }

        public void ConfirmarTurno(int medicoId, DateTime dia, TimeSpan hora)
        {
            // Implementación para confirmar el turno
            //hardcodeo de paciente
            int defaultPacienteId = 1; // Id del paciente por defecto

            var turno = new Turno
            {
                IdMedico = medicoId,
                IdPaciente = defaultPacienteId,
                Dia = dia,
                Hora = hora
            };
            _DBContext.Turnos.Add(turno);
            _DBContext.SaveChanges();
        }
    }
}