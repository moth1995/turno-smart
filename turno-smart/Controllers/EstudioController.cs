using Microsoft.AspNetCore.Mvc;
using turno_smart.Interfaces;
using turno_smart.Models;
using turno_smart.ViewModels.EstudioVM;

namespace turno_smart.Controllers
{
    public class EstudioController(IEstudioService estudioService): Controller {

        private readonly IEstudioService _estudioService = estudioService;

        [HttpGet]
		public IActionResult Index(string? filter)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var listEstudioVM = new ListEstudioVM();

			if(!string.IsNullOrEmpty(filter)) {
				listEstudioVM.Estudios = _estudioService.GetAll(filter);

			} else {
				listEstudioVM.Estudios = _estudioService.GetAll();
			}

			return View(listEstudioVM);
		}

        
        [HttpGet]
		public IActionResult Create()
		{

			var createEstudioVM = new CreateEstudioVM
			{
              Descripcion = string.Empty   
			};

			return View(createEstudioVM);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateEstudioVM obj)
        {

            if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

            try {
                var estudioModel = new Estudio()
                {
                    Descripcion = obj.Descripcion
                };

                _estudioService.Create(estudioModel);
                //TempData["SuccessMessage"] = "Médico creado correctamente.";
                return RedirectToAction(nameof(Index));
            } catch (Exception ex) {
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
                var estudio = _estudioService.GetById(id);
                if (estudio == null) return NotFound();

                var especialidadModel = new EditEstudioVM
                {
                    Id = estudio.Id,
                    Descripcion = estudio.Descripcion
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
        public IActionResult Edit(EditEstudioVM obj)
        {
            if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

            try {
                var estudio = _estudioService.GetById(obj.Id);
                if(estudio == null) return NotFound();
                
                estudio.Descripcion = obj.Descripcion ?? estudio.Descripcion;

                _estudioService.Update(estudio);
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
                var estudio = _estudioService.GetById(id);
                if (estudio == null) return NotFound();

                var model = new DeleteEstudioVM
                {
                    Id = estudio.Id,
                    Descripcion = estudio.Descripcion,
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
                var estudio = _estudioService.GetById(id);
                if(estudio == null) return NotFound();

                _estudioService.Delete(id);
                //TempData["SuccessMessage"] = "Médico eliminado correctamente.";
                return RedirectToAction(nameof(Index));

            } catch (Exception ex) {
                //TempData["ErrorMessage"] = "Error al intentar eliminar el médico" + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        } 

    }
}