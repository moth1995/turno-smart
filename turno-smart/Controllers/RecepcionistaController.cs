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
        public async Task<IActionResult> Create(Recepcionista obj) {
            
            ModelState.Remove("Usuario");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Error al intentar crear recepcionista." + ex.Message;
                    return RedirectToAction(nameof(Index));
                }
            }

            TempData["ErrorMessage"] = "Error al intentar crear recepcionista.";
            return RedirectToAction(nameof(Index));
        }
    }
}