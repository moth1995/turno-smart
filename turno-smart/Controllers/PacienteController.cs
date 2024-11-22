using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using turno_smart.Interfaces;
using turno_smart.Models;
using turno_smart.Services;
using turno_smart.ViewModels.PacienteVM;

namespace turno_smart.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PacienteController(IPacienteService pacienteService, UserManager<Usuarios> userManager) : Controller {

        private readonly IPacienteService _pacienteService = pacienteService;
        private readonly UserManager<Usuarios> _userManager = userManager;

        [HttpGet]
        public IActionResult Index(string? filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var listPacienteVM = new ListPacienteVM();

            var pacientes = _pacienteService.GetAll(filter);
            listPacienteVM.Pacientes = pacientes
                .Select(p=> new PacienteViewModel
                {
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    Dni = p.DNI,
                    Domicilio = p.Domicilio,
                    FechaNacimiento = p.FechaNacimiento,
                    Provincia = p.Provincia,
                    Ciudad = p.Ciudad,
                    Cobertura = p.Cobertura,
                    Telefono = p.Telefono,
                    Email = p.Email,
                }).ToList();

            return View(listPacienteVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var paciente = new CreatePacienteVM
            {
                Nombre = string.Empty,
                Apellido = string.Empty,
                FechaNacimiento = DateTime.Now,
                DNI = 0,
                Domicilio = string.Empty,
                Ciudad = string.Empty,
                Provincia = string.Empty,
                Cobertura = 0,
                Telefono = 0,
                Email = string.Empty,
                Estado = 0,
                FechaAlta = DateTime.Now
            };

            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePacienteVM obj)
        {

            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            try 
            {
                Usuarios user = new Usuarios
                {
                    Email = obj.Email,
                    UserName = obj.Email,
                    DNI = obj.DNI,
                };

                var result = await _userManager.CreateAsync(user, "NuevoPacient3!");
                if (result.Succeeded)
                {

                    var pacienteModel = new Paciente()
                    {
                        Nombre = obj.Nombre,
                        Apellido = obj.Apellido,
                        FechaNacimiento = obj.FechaNacimiento,
                        DNI = obj.DNI,
                        Domicilio = obj.Domicilio,
                        Ciudad = obj.Ciudad,
                        Provincia = obj.Provincia,
                        Cobertura = obj.Cobertura,
                        Telefono = obj.Telefono,
                        Email = obj.Email,
                        Estado = obj.Estado,
                        FechaAlta = obj.FechaAlta
                    };

                    _pacienteService.Create(pacienteModel);

                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try {

                var paciente = _pacienteService.GetById(id);
                if (paciente == null) return NotFound();

                var pacienteVM = new EditPacienteVM
                {
                    Id = paciente.Id,
                    Nombre = paciente.Nombre,
                    Apellido = paciente.Apellido,
                    FechaNacimiento = paciente.FechaNacimiento,
                    DNI = paciente.DNI,
                    Domicilio = paciente.Domicilio,
                    Ciudad = paciente.Ciudad,
                    Provincia = paciente.Provincia,
                    Cobertura = paciente.Cobertura,
                    Telefono = paciente.Telefono,
                    Email = paciente.Email,
                    Estado = paciente.Estado,
                    FechaAlta = paciente.FechaAlta
                };

                return View(pacienteVM);

            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditPacienteVM obj)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try {
                var paciente = _pacienteService.GetById(obj.Id);
                if(paciente == null) return NotFound();

                paciente.Nombre = obj.Nombre;
                paciente.Apellido = obj.Apellido;
                paciente.FechaNacimiento = obj.FechaNacimiento;
                paciente.DNI = obj.DNI;
                paciente.Domicilio = obj.Domicilio;
                paciente.Ciudad = obj.Ciudad;
                paciente.Provincia = obj.Provincia;
                paciente.Cobertura = obj.Cobertura;
                paciente.Telefono = obj.Telefono;
                paciente.Email = obj.Email;
                paciente.Estado = obj.Estado;
                paciente.FechaAlta = obj.FechaAlta;

                _pacienteService.Update(paciente);

                return RedirectToAction("Index");

            } catch (Exception ex) {
                return BadRequest(ex.Message);
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
                var paciente = _pacienteService.GetById(id);
                if (paciente == null) return NotFound();

                var pacienteVM = new DeletePacienteVM
                {
                    Id = paciente.Id,
                    Nombre = paciente.Nombre,
                    Apellido = paciente.Apellido,
                };

                return View(pacienteVM);

            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try {
                var paciente = _pacienteService.GetById(id);
                if (paciente == null) return NotFound();

                _pacienteService.Delete(id);

                return RedirectToAction("Index");

            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    } 
}