using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using turno_smart.Interfaces;
using turno_smart.Models;
using turno_smart.ViewModels.EstudioVM;
using turno_smart.ViewModels.HistorialMedicoVM.cs;

namespace turno_smart.Controllers
{
    public class HistorialMedicoController(
        IHistorialMedicoService historialMedicoService,
        IEstudioService estudioService,
        IPacienteService pacienteService,
        IMedicoService medicoService
    ) : Controller
    {

        private readonly IHistorialMedicoService _historialMedicoService = historialMedicoService;
        private readonly IEstudioService _estudioService = estudioService;
        private readonly IPacienteService _pacienteService = pacienteService;
        private readonly IMedicoService _medicoService = medicoService;

        [HttpGet]
        public IActionResult Index(string? filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var listHistorialMedicoVM = new ListHistorialMedicoVM();

            if (!string.IsNullOrEmpty(filter))
            {
                listHistorialMedicoVM.HistorialesMedicos = _historialMedicoService.GetAll(filter);

            }
            else
            {
                listHistorialMedicoVM.HistorialesMedicos = _historialMedicoService.GetAll();
            }

            return View(listHistorialMedicoVM);
        }

        [HttpGet]
        public IActionResult Create(int pacienteId, int medicoId)
        {
            var model = new CreateHistorialMedicoVM
            {
                Estudios = _estudioService.GetAll().Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Descripcion }).ToList(),
                Paciente = _pacienteService.GetById(pacienteId),
                Medico = _medicoService.GetById(medicoId)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateHistorialMedicoVM obj)
        {

            ModelState.Remove("Paciente");
            ModelState.Remove("Medico");
            ModelState.Remove("Estudio");

            if (!ModelState.IsValid)
            { 
                // Si el modelo no es válido, mantener los datos en el formulario para corregirlos
                return View(obj);
            }

            try
            {
                var historialModel = new HistorialMedico()
                {
                    IdEstudio = obj.IdEstudio,
                    IdPaciente = obj.IdPaciente,
                    IdMedico = obj.IdMedico,
                    Fecha = obj.Fecha,
                    Sintomas = obj.Sintomas,
                    Diagnostico = obj.Diagnostico,
                    Tratamiento = obj.Tratamiento,
                    NotasAdicionales = obj.NotasAdicionales
                };

                _historialMedicoService.Create(historialModel);

                TempData["SuccessMessage"] = "Historia médica creada correctamente.";
                return RedirectToAction("Details", new { pacienteId = obj.IdPaciente, medicoId = obj.IdMedico });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al intentar crear historia médica." + ex.InnerException?.Message ?? ex.Message;
                return View(obj);
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
                var historial = _historialMedicoService.GetById(id);
                if (historial == null) return NotFound();

                var historialModel = new EditHistorialMedicoVM
                {
                    Id = historial.Id,
                    IdEstudio = historial.IdEstudio,
                    IdPaciente = historial.IdPaciente,
                    IdMedico = historial.IdMedico,
                    Estudios = _estudioService.GetAll().Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Descripcion }).ToList(),
                    Pacientes = _pacienteService.GetAll().Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nombre }).ToList(),
                    Medicos = _medicoService.GetAll().Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }).ToList()
                };

                return View(historialModel);

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.InnerException?.Message ?? ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditHistorialMedicoVM obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var historial = _historialMedicoService.GetById(obj.Id);
                if (historial == null) return NotFound();

                historial.IdEstudio = obj.IdEstudio;
                historial.IdPaciente = obj.IdPaciente;
                historial.IdMedico = obj.IdMedico;

                _historialMedicoService.Update(historial);
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
                var historial = _historialMedicoService.GetById(id);
                if (historial == null) return NotFound();

                var model = new DeleteHistorialMedicoVM
                {
                    Id = historial.Id,
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
                var historial = _historialMedicoService.GetById(id);
                if (historial == null) return NotFound();

                _historialMedicoService.Delete(id);
                //TempData["SuccessMessage"] = "Médico eliminado correctamente.";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                //TempData["ErrorMessage"] = "Error al intentar eliminar el médico" + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Details(int pacienteId, int medicoId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var paciente = _pacienteService.GetById(pacienteId);
                if(paciente == null) return NotFound();

                var medico = _medicoService.GetById(medicoId);
                if(medico == null) return NotFound();

                var historial = _historialMedicoService.GetByPacienteId(paciente.Id);
                var listHistorialMedicoVM = new ListHistorialMedicoVM
                {
                    Paciente = paciente,
                    Medico = medico,
                    
                };

                if (historial == null || !historial.Any())
                {
                    listHistorialMedicoVM.HistorialesMedicos = [];
                    return View(listHistorialMedicoVM);
                }

                listHistorialMedicoVM.HistorialesMedicos = historial.Select(hm => new HistorialMedico
                {
                    Id = hm.Id,
                    Paciente = hm.Paciente,
                    Medico = hm.Medico,
                    Estudio = hm.Estudio,
                    Diagnostico = hm.Diagnostico,
                    Fecha = hm.Fecha,
                    Sintomas = hm.Sintomas,
                    Tratamiento = hm.Tratamiento,
                    NotasAdicionales = hm.NotasAdicionales
                }).ToList();

               return View(listHistorialMedicoVM);

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}