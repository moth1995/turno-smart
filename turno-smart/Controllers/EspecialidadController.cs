using Microsoft.AspNetCore.Mvc;
using Serilog;
using turno_smart.Interfaces;
using turno_smart.Models;
using turno_smart.ViewModels.EspecialidadVM;

namespace turno_smart.Controllers
{
    public class EspecialidadController(IEspecialidadService especialidadService): Controller {

        private readonly IEspecialidadService _especialidadService = especialidadService;

        [HttpGet]
		public async Task<IActionResult> Index(string? filter)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var listEspecialidadVM = new ListEspecialidadVM();

			var especialidades = await _especialidadService.GetAll(filter);
			listEspecialidadVM.Especialidades = especialidades.Select(e => new EspecialidadVM
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Descripcion = e.Descripcion,
            }).ToList();


			return View(listEspecialidadVM);
		}

        
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

            try {
                var especialidadModel = new Especialidad()
                {
                    Nombre = obj.Nombre,
                    Descripcion = obj.Descripcion
                };

                _especialidadService.Create(especialidadModel);
                TempData["SuccessMessage"] = "Especialidad creada correctamente.";
                return RedirectToAction(nameof(Index));
            } catch (Exception ex) {
                TempData["ErrorMessage"] = "Error al intentar crear especialidad." + ex.InnerException?.Message ?? ex.Message;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditEspecialidadVM obj)
        {
            if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

            try {
                var especialidad = _especialidadService.GetById(obj.Id);
                if(especialidad == null) return NotFound();
                
                especialidad.Nombre = obj.Nombre ?? especialidad.Nombre;
                especialidad.Descripcion = obj.Descripcion ?? especialidad.Descripcion;

                _especialidadService.Update(especialidad);
                TempData["SuccessMessage"] = "Información de la especialidad actualizada correctamente.";
                return Json(new { redirectUrl = Url.Action("Index") });
            } catch (Exception ex) {
                TempData["ErrorMessage"] = "Error al intentar actualizar la información de la especialidad." + ex.Message;
                return Json(new { redirectUrl = Url.Action("Index") });
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var especialidad = _especialidadService.GetById(id);
            if (especialidad == null) return NotFound();

            var especialidadModel = new EspecialidadVM
            {
                Id = especialidad.Id,
                Nombre = especialidad.Nombre,
                Descripcion = especialidad.Descripcion
            };

            return View(especialidadModel);
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

                if (especialidad.Medicos.Any())
                {
                    TempData["ErrorMessage"] = "Antes de eliminar esta especialidad desvincule todos los medicos";
                }
                else
                {
                    _especialidadService.Delete(id);
                    TempData["SuccessMessage"] = "Especialidad eliminada correctamente.";
                }
                return Json(new { redirectUrl = Url.Action("Index") });
            }
            catch (Exception ex)
            {
                // Mensaje de error para el usuario
                TempData["ErrorMessage"] = "Ocurrio un error al intentar eliminar especialidad";

                // Registro de detalles técnicos para desarrolladores
                var innerExceptionMessage = ex.InnerException?.Message;
                var detailedErrorMessage = "Error al intentar eliminar la especialidad: " + ex.Message;
                if (innerExceptionMessage != null)
                {
                    detailedErrorMessage += " Detalles: " + innerExceptionMessage;
                }
                Log.Error(detailedErrorMessage);

                return Json(new { redirectUrl = Url.Action("Index") });
            }
        } 

    }
}