using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using turno_smart.Interfaces;
using turno_smart.Models;
using turno_smart.ViewModels.EspecialidadVM;
using turno_smart.ViewModels.ProfesionalVM;

namespace turno_smart.Controllers
{
    public class EspecialidadController(IEspecialidadService especialidadService) : Controller
    {

        private readonly IEspecialidadService _especialidadService = especialidadService;

        [HttpGet]
        public ActionResult Index(string searchTerm, string especialidadSeleccionada)
        {
            var model = new ListaEspecialidadVM
            {
                Especialidades = GetEspecialidades(),
                SearchTerm = searchTerm,
                EspecialidadSeleccionada = especialidadSeleccionada
            };

            if (!string.IsNullOrEmpty(searchTerm))
            {
                model.Especialidades = model.Especialidades
                    .Where(e => e.Nombre.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult InformacionProfesional(string nombre)
        {
            var profesional = ObtenerInformacionProfesional(nombre); // Implementa esta función para obtener la información del profesional
            if (profesional == null)
            {
                return NotFound();
            }
            return View(profesional);
        }

        private List<EspecialidadVM> GetEspecialidades()
        {
            return new List<EspecialidadVM>

        {
            new EspecialidadVM
        {
            Nombre = "Cirugía Cardiológica",
            Profesionales = new List<string> { "Dra. María Laura Vega", "Dr. Juan López", "Dr. Eduardo Pérez" },
            Categoria = "CIRUGÍA:"
        },
        new EspecialidadVM
        {
            Nombre = "Cirugía Plástica",
            Profesionales = new List<string> { "Dr. Roberto González", "Dra. Ana María Soto", "Dr. Carlos Ruiz" },
            Categoria = "CIRUGÍA:"
        },
        new EspecialidadVM
        {
            Nombre = "Cirugía Dental",
            Profesionales = new List<string> { "Dr. Jorge Martínez", "Dra. Lucía Sánchez", "Dr. Pablo Díaz" },
            Categoria = "CIRUGÍA:"
        },
        new EspecialidadVM
        {
            Nombre = "Cirugía General",
            Profesionales = new List<string> { "Dr. Alberto Fernández", "Dra. María Pérez", "Dr. Juan Carlos López" },
            Categoria = "CIRUGÍA:"
        },
        new EspecialidadVM
        {
            Nombre = "Cirugía Oftalmológica",
            Profesionales = new List<string> { "Dra. Carmen Rodríguez", "Dr. Luis González", "Dra. Ana María López" },
            Categoria = "CIRUGÍA:"
        },
        new EspecialidadVM
        {
            Nombre = "Cirugía Pediátrica",
            Profesionales = new List<string> { "Dr. Roberto Sánchez", "Dra. Laura Martínez", "Dr. Carlos Pérez" },
            Categoria = "CIRUGÍA:"
        },
        new EspecialidadVM
        {
            Nombre = "Cardiología",
            Profesionales = new List<string> { "Dra. Natalia Ramos", "Dr. Miguel Ángel Torres", "Dra. Laura Martínez" },
            Categoria = "ATENCIÓN GENERAL:"
        },
        new EspecialidadVM
        {
            Nombre = "Gastroenterología",
            Profesionales = new List<string> { "Dr. Ricardo Pérez", "Dra. María González", "Dr. Juan Pablo Sánchez" },
            Categoria = "ATENCIÓN GENERAL:"
        },
        new EspecialidadVM
        {
            Nombre = "Neurología",
            Profesionales = new List<string> { "Dr. Arturo de la Peña", "Dra. Sofía Martínez", "Dr. Gabriel López" },
            Categoria = "ATENCIÓN GENERAL:"
        },
        new EspecialidadVM
        {
            Nombre = "Otorrinolaringología",
            Profesionales = new List<string> { "Dr. Fernando García", "Dra. Ana Rodríguez", "Dr. Carlos Sánchez" },
            Categoria = "ATENCIÓN GENERAL:"
        },
        new EspecialidadVM
        {
            Nombre = "Odontología",
            Profesionales = new List<string> { "Dra. Patricia López", "Dr. Roberto González", "Dra. María Fernández" },
            Categoria = "ATENCIÓN GENERAL:"
        },
        new EspecialidadVM
        {
            Nombre = "Pediatría",
            Profesionales = new List<string> { "Dr. Marco Valencia", "Dra. Carolina Silva", "Dr. Luis Morales" },
            Categoria = "ATENCIÓN GENERAL:"
        },
        new EspecialidadVM
        {
            Nombre = "Dermatología",
            Profesionales = new List<string> { "Dra. Carolina Mendez", "Dr. Andrés Pérez", "Dra. Valentina Ruiz" },
            Categoria = "ATENCIÓN GENERAL:"
        },
        new EspecialidadVM
        {
            Nombre = "Ginecología",
            Profesionales = new List<string> { "Dra. Sandra Herrera", "Dra. Patricia López", "Dr. Martín Rodríguez" },
            Categoria = "ATENCIÓN GENERAL:"
        },
        new EspecialidadVM
        {
            Nombre = "Traumatología",
            Profesionales = new List<string> { "Dr. Luis Rodriguez", "Dra. María Fernández", "Dr. Jorge Sánchez" },
            Categoria = "ATENCIÓN GENERAL:"
        },
        new EspecialidadVM
        {
            Nombre = "Urología",
            Profesionales = new List<string> { "Dr. Héctor González", "Dra. Laura Pérez", "Dr. Ricardo Sánchez" },
            Categoria = "ATENCIÓN GENERAL:"
        }
    };
        }

        private ProfesionalVM ObtenerInformacionProfesional(string nombre)
        {
            // Implementa la lógica para obtener la información del profesional basado en el nombre
            //return profesionales.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            return new ProfesionalVM
            {
                Nombre = nombre,
                Especialidad = "Cirugía Oftalmológica",
                HorarioAtencion = "De Lunes a Domingo, de 8:00h a 23:00h",
                Direccion = "Calle Cualquiera 123, Cualquier Lugar, CP: 12345",
                Telefono = "91-1234-567",
                Email = "hola@sitiogenial.es"
            };
        }
    
    //public async Task<IActionResult> Index(string? filter)
    //{
    //	if (!ModelState.IsValid)
    //	{
    //		return BadRequest(ModelState);
    //	}

    //	var listEspecialidadVM = new ListEspecialidadVM();

    //	var especialidades = await _especialidadService.GetAll(filter);
    //	listEspecialidadVM.Especialidades = especialidades.Select(e => new EspecialidadVM
    //          {
    //              Id = e.Id,
    //              Nombre = e.Nombre,
    //              Descripcion = e.Descripcion,
    //          }).ToList();


    //	return View(listEspecialidadVM);
    //}


    [HttpGet]
        public IActionResult Create()
        {

            var createEspecialidadVM = new CreateEspecialidadVM
            {
                Nombre = string.Empty,
                Descripcion = string.Empty
            };

            return View(createEspecialidadVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateEspecialidadVM obj)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var especialidadModel = new Especialidad()
                {
                    Nombre = obj.Nombre,
                    Descripcion = obj.Descripcion ?? null
                };

                _especialidadService.Create(especialidadModel);
                //TempData["SuccessMessage"] = "Médico creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //TempData["ErrorMessage"] = "Error al intentar crear especialidad." + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var especialidad = _especialidadService.GetById(id);
                if (especialidad == null) return NotFound();

                var especialidades = _especialidadService.GetAll();

                var especialidadModel = new EditEspecialidadVM
                {
                    Id = especialidad.Id,
                    Nombre = especialidad.Nombre,
                    Descripcion = especialidad.Descripcion
                };

                return View(especialidadModel);

            }
            catch (Exception ex)
            {
                //TempData["ErrorMessage"] = "Error: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditEspecialidadVM obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var especialidad = _especialidadService.GetById(obj.Id);
                if (especialidad == null) return NotFound();

                especialidad.Nombre = obj.Nombre ?? especialidad.Nombre;
                especialidad.Descripcion = obj.Descripcion ?? especialidad.Descripcion;

                _especialidadService.Update(especialidad);
                //TempData["SuccessMessage"] = "Información del médico actualizada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //TempData["ErrorMessage"] = "Error al intentar actualizar la información del médico." + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var especialidad = _especialidadService.GetById(id);
                if (especialidad == null) return NotFound();

                var model = new DeleteEspecialidadVM
                {
                    Id = especialidad.Id,
                    Nombre = especialidad.Nombre,
                };

                return View(model);
            }
            catch (Exception ex)
            {
                //TempData["ErrorMessage"] = "Error: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var especialidad = _especialidadService.GetById(id);
                if (especialidad == null) return NotFound();

                _especialidadService.Delete(id);
                //TempData["SuccessMessage"] = "Médico eliminado correctamente.";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                //TempData["ErrorMessage"] = "Error al intentar eliminar el médico" + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

    }
}