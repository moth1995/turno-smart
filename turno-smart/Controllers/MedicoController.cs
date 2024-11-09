using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using turno_smart.Interfaces;
using turno_smart.Models;
using turno_smart.ViewModels.MedicoVM;

namespace turno_smart.Controllers
{
    public class MedicoController(
        IMedicoService medicoService,
        IEspecialidadService especialidadService
    ): Controller {

        private readonly IMedicoService _medicoService = medicoService;
        private readonly IEspecialidadService _especialidadService = especialidadService;

        [HttpGet]
		public IActionResult Index(string? filter)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var listMedicoVM = new ListMedicoVM();

			if(!string.IsNullOrEmpty(filter)) {
				var medicos = _medicoService.GetAll(filter);
				listMedicoVM.Medicos = medicos;

			} else {
				var medicos = _medicoService.GetAll();
				listMedicoVM.Medicos = medicos;
			}

			return View(listMedicoVM);
		}

        
        [HttpGet]
		public IActionResult Create()
		{

			var especialidades = _especialidadService.GetAll();

			var createMedicoVM = new CreateMedicoVM
			{
				Nombre = string.Empty,
				Apellido = string.Empty,
				IdEspecialidad = especialidades.FirstOrDefault()?.Id ?? 0,
				Telefono = 0,
				Email = string.Empty,
				Especialidad = especialidades.Select(e => new SelectListItem
				{
					Value = e.Id.ToString(),
					Text = e.Nombre
				}).ToList(),
			};

			return View(createMedicoVM);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateMedicoVM obj)
        {

            ModelState.Remove("Especialidad");
            if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

            try {
                var medicoModel = new Medico()
                {
                    Nombre = obj.Nombre,
                    Apellido = obj.Apellido,
                    Telefono = obj.Telefono,
                    Email = obj.Email,
                    IdEspecialidad = obj.IdEspecialidad,
                };

                _medicoService.Create(medicoModel);
                //TempData["SuccessMessage"] = "Médico creado correctamente.";
                return RedirectToAction(nameof(Index));
            } catch (Exception ex) {
                //TempData["ErrorMessage"] = "Error al intentar crear medico." + ex.Message;
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
                
                var medico = _medicoService.GetById(id);
                if (medico == null) return NotFound();

                var especialidades = _especialidadService.GetAll();

                var medicoModel = new EditMedicoVM
                {
                    Id = medico.Id,
                    Nombre = medico.Nombre,
                    Apellido = medico.Apellido,
                    Telefono = medico.Telefono,
                    Email = medico.Email,
                    Especialidad = especialidades.Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = e.Nombre
                    }).ToList(),

                };

                return View(medicoModel);

            }
            catch (Exception ex)
            {
                //TempData["ErrorMessage"] = "Error: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditMedicoVM obj)
        {
            ModelState.Remove("Especialidad");
            if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

            try {
                var medico = _medicoService.GetById(obj.Id);
                if(medico == null) return NotFound();
                
                medico.Nombre = obj.Nombre ?? medico.Nombre;
                medico.Apellido = obj.Apellido ?? medico.Apellido;
                medico.Email = obj.Email ?? medico.Email;
                medico.Telefono = obj.Telefono ?? medico.Telefono;
                medico.IdEspecialidad = obj.IdEspecialidad;

                _medicoService.Update(medico);
                //TempData["SuccessMessage"] = "Información del médico actualizada correctamente.";
                return RedirectToAction(nameof(Index));
            } catch (Exception ex) {
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
                var medico = _medicoService.GetById(id);
                if (medico == null) return NotFound();

                var model = new DeleteMedicoVM
                {
                    Id = medico.Id,
                    Nombre = medico.Nombre
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

            try {
                var medico = _medicoService.GetById(id);
                if(medico == null) return NotFound();

                _medicoService.Delete(id);
                //TempData["SuccessMessage"] = "Médico eliminado correctamente.";
                return RedirectToAction(nameof(Index));

            } catch (Exception ex) {
                //TempData["ErrorMessage"] = "Error al intentar eliminar el médico" + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        } 
    }
}