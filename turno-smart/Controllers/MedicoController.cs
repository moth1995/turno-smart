using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using turno_smart.Interfaces;
using turno_smart.Models;
using turno_smart.Services;
using turno_smart.ViewModels.MedicoVM;

namespace turno_smart.Controllers
{
    public class MedicoController(
        IMedicoService medicoService,
        IEspecialidadService especialidadService,
        UserManager<Usuarios> userManager
    ) : Controller {

        private readonly IMedicoService _medicoService = medicoService;
        private readonly IEspecialidadService _especialidadService = especialidadService;
        private readonly UserManager<Usuarios> _userManager = userManager;

        [HttpGet]
		public async Task<IActionResult> Index(string? filter, int? especialidadId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
            List<Medico> medicos;
            if (especialidadId != null)
            {
                medicos = await _medicoService.GetAll((int)especialidadId);
            }
            else
            {
                medicos = await _medicoService.GetAll(filter);
            }

            var listMedicoVM = new ListMedicoVM();

            var medicosVM = medicos.Select(m => new MedicoVM
            {
                Id = m.Id,
                Nombre = $"{m.Nombre} {m.Apellido}",
                Especialidad = m.Especialidad.Nombre
            }).ToList();

            var medicosVMSorted = medicosVM.OrderBy(m => m.Especialidad).ToList();

            listMedicoVM.Medicos = medicosVMSorted;

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
                DNI = 0,
                Email = string.Empty,
                Reseña = string.Empty, 
                Imagen = string.Empty,
                Matricula = 0,
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
        public async Task<IActionResult> Create(CreateMedicoVM model)
        {
            ModelState.Remove("Especialidad");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Usuarios user = new Usuarios
            {
                Email = model.Email,
                UserName = model.Email,
                DNI = model.DNI,
            };

            var result = await _userManager.CreateAsync(user, "NuevoMedic0!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Medico");
                try
                {
                    var medico = new Medico()
                    {
                        Nombre = model.Nombre,
                        Apellido = model.Apellido,
                        Telefono = model.Telefono,
                        DNI = model.DNI,
                        Email = model.Email,
                        IdEspecialidad = model.IdEspecialidad,
                        Reseña = model.Reseña,      // Nueva propiedad
                        Imagen = model.Imagen,      // Nueva propiedad (URL)
                        Matricula = model.Matricula
                    };

                    _medicoService.Create(medico);
                    TempData["SuccessMessage"] = "Médico creado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Error al intentar crear medico." + ex.Message;
                    return RedirectToAction(nameof(Index));
                }
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            var especialidades = _especialidadService.GetAll();
            model.Especialidad = especialidades.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Nombre
            }).ToList();

            return View(model);
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
                    IdEspecialidad = medico.IdEspecialidad,
                    Reseña = medico.Reseña,       // Asignar valor existente
                    Imagen = medico.Imagen,
                    Matricula = medico.Matricula,// Asignar valor existente
                    Especialidad = especialidades.Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = e.Nombre
                    }).ToList(),
                };
                return Json(medicoModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromBody] EditMedicoVM obj)
        {
            ModelState.Remove("Especialidad");
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Modelo no válido" });
            }

            try
            {
                var medico = _medicoService.GetById(obj.Id);
                if (medico == null) return NotFound(new { success = false, message = "Médico no encontrado" });

                medico.Nombre = obj.Nombre ?? medico.Nombre;
                medico.Apellido = obj.Apellido ?? medico.Apellido;
                medico.Email = obj.Email ?? medico.Email;
                medico.Telefono = obj.Telefono ?? medico.Telefono;
                medico.IdEspecialidad = obj.IdEspecialidad;
                medico.Reseña = obj.Reseña ?? medico.Reseña;
                medico.Imagen = obj.Imagen ?? medico.Imagen;
                medico.Matricula = obj.Matricula ?? medico.Matricula;

                _medicoService.Update(medico);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Error interno del servidor: " + ex.Message });
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