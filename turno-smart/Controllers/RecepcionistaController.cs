using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using turno_smart.Interfaces;
using turno_smart.Models;
using turno_smart.Services;
using turno_smart.ViewModels.CreateRecepcionistaVM;
using turno_smart.ViewModels.RecepcionistaVM;

namespace turno_smart.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RecepcionistaController(
        IRecepcionistaService recepcionistaService,
        IMedicoService medicoService,
        UserManager<Usuarios> userManager
    ) : Controller {

        private readonly IRecepcionistaService _recepcionistaService = recepcionistaService;
        private readonly IMedicoService _medicoService = medicoService;
        private readonly UserManager<Usuarios> _userManager = userManager;

        [HttpGet]
        public IActionResult Index(string? filter)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

            var ListRecepcionistaVM = new ListRecepcionistaVM
            {
                Filter = filter,
                Recepcionistas = []
            };

            List<Recepcionista> recepcionistas = [];

            try
            {
                if(filter == null)
                {
                    recepcionistas = _recepcionistaService.GetAll();
                } else {
                    recepcionistas = _recepcionistaService.GetAll(filter);
                }

                ListRecepcionistaVM.Recepcionistas = recepcionistas.Select(r => new RecepcionistaVM
                {
                    Id = r.Id,
                    Nombre = $"{r.Nombre} {r.Apellido}",
                    Email = r.Email,
                    DNI = r.DNI,
                }).ToList();

                return View(ListRecepcionistaVM);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
		}

        [HttpGet]
        public IActionResult Create() {
           var paciente = new CreateRecepcionistaVM
            {
                Nombre = string.Empty,
                Apellido = string.Empty,
                DNI = 0,
                Email = string.Empty,
            };

            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRecepcionistaVM obj) {
            
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            Usuarios user = new Usuarios
            {
                Email = obj.Email,
                UserName = obj.Email,
                DNI = obj.DNI,
            };

            var result = await _userManager.CreateAsync(user, "Recep123!");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Recepcionista");
                try
                {
                    var recepcionista = new Recepcionista()
                    {
                        Nombre = obj.Nombre,
                        Apellido = obj.Apellido,
                        DNI = obj.DNI,
                        Email = obj.Email,
                    };

                    _recepcionistaService.Create(recepcionista);
                    TempData["SuccessMessage"] = "Recepcionista creado correctamente.";
                    return Json(new { redirectUrl = Url.Action("Index") });
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Error al intentar crear recepcionista." + ex.Message;
                    return Json(new { redirectUrl = Url.Action("Index") });
                }
            }

            TempData["ErrorMessage"] = "Error al intentar crear recepcionista.";
            return Json(new { redirectUrl = Url.Action("Index") });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try {
                var recepcionista = _recepcionistaService.GetById(id);
                if (recepcionista == null) return NotFound();

                var recepcionistaVM = new EditRecepcionistaVM
                {
                    Id = recepcionista.Id,
                    Nombre = recepcionista.Nombre,
                    Apellido = recepcionista.Apellido,
                    DNI = recepcionista.DNI,
                    Email = recepcionista.Email,
                };

                return View(recepcionistaVM);

            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditConfirmed(Recepcionista obj)
        {
            ModelState.Remove("Usuario");
            ModelState.Remove("DNI");
            ModelState.Remove("Email");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try {
                var recepcionista = _recepcionistaService.GetById(obj.Id);
                if (recepcionista == null) return NotFound();

                recepcionista.Nombre = obj.Nombre;
                recepcionista.Apellido = obj.Apellido;

                _recepcionistaService.Update(recepcionista);

                TempData["SuccessMessage"] = "Recepcionista actualizado correctamente.";
                return Json(new { redirectUrl = Url.Action("Index") });

            } catch (Exception ex) {
                TempData["ErrorMessage"] = "Error al intentar actualizar recepcionista." + ex.Message;
                return Json(new { redirectUrl = Url.Action("Index") });
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try {
                var recepcionista = _recepcionistaService.GetById(id);
                if (recepcionista == null) return NotFound();

                var recepcionistaVM = new DeleteRecepcionistaVM
                {
                    Id = recepcionista.Id,
                    Nombre = recepcionista.Nombre,
                    Apellido = recepcionista.Apellido,
                    DNI = recepcionista.DNI,
                };

                return View(recepcionistaVM);

            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try {
                var recepcionista = _recepcionistaService.GetById(id);
                if (recepcionista == null) return NotFound();

                await _userManager.DeleteAsync(recepcionista.Usuario);
                _recepcionistaService.Delete(id);

                TempData["SuccessMessage"] = "Recepcionista eliminado correctamente.";
                return Json(new { redirectUrl = Url.Action("Index") });

            } catch (Exception ex) {
                TempData["ErrorMessage"] = "Error al intentar eliminar recepcionista." + ex.Message;
                return Json(new { redirectUrl = Url.Action("Index") });
            }
        }
    }
}