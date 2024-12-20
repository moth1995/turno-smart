using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using turno_smart.Models;
using turno_smart.ViewModels.AccountVM;
using turno_smart.Interfaces;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using turno_smart.ViewModels.PacienteVM;

namespace turno_smart.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<Usuarios> _signInManager;
        private readonly UserManager<Usuarios> _userManager;
        private readonly IPacienteService _pacienteService;
        public AccountController(ILogger<HomeController> logger, SignInManager<Usuarios> signInManager, UserManager<Usuarios> userManager, IPacienteService pacienteService)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _pacienteService = pacienteService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            var loginVM = new LoginVM();
            return PartialView("_LoginModal", loginVM);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return Json(new { redirectUrl = Url.Action("Index", "Home") });
                }
                ModelState.AddModelError("", "Email or password is incorrect.");
            }
            
            return PartialView("_LoginModal", model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var registerVM = new RegisterVM();
            return PartialView("_RegistrationModal", registerVM);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                int dni;
                if (!int.TryParse(model.DNI, out dni))
                {
                    ModelState.AddModelError("DNI", "DNI ingresado invalido");
                    return PartialView("_RegistrationModal", model);
                }
                if (!model.AceptoTerminos)
                {
                    ModelState.AddModelError("AceptoTerminos", "Debes aceptar los terminos para registrarte.");
                    return PartialView("_RegistrationModal", model);
                }
                Usuarios users = new Usuarios
                {
                    Email = model.Email,
                    UserName = model.Email,
                    DNI = dni,
                };

                var result = await _userManager.CreateAsync(users, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(users, "Paciente");
                    Paciente paciente = _pacienteService.GetByDNI(users.DNI);
                    if (paciente == null)
                    {
                        paciente = new Paciente()
                        {
                            Nombre = model.Nombre,
                            Apellido = model.Apellido,
                            DNI = dni,
                            FechaNacimiento = model.FechaNacimiento,
                            Email = model.Email,
                            FechaAlta = DateTime.Now,
                            Usuario = users,
                            Ciudad = "",
                            Provincia = "",
                            Domicilio = "",
                            Cobertura = 0,
                            Telefono = 0,
                            Estado = 0,
                        };
                    }
                    _pacienteService.Create(paciente);

                    return PartialView("_RegistrationSuccess", model);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return PartialView("_RegistrationModal", model);
                }
            }
            return PartialView("_RegistrationModal", model);
        }

        [HttpGet]
        public IActionResult Details(string? ActiveTab = "general")
        {
            var user = _userManager.GetUserAsync(User).Result;
            var paciente = _pacienteService.GetByDNI(user.DNI);

            var pacienteVM = new ProfileVM
            {
                Id = paciente.Id,
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                FechaNacimiento = paciente.FechaNacimiento,
                DNI = paciente.DNI,
                Domicilio = paciente.Domicilio,
                Ciudad = paciente.Ciudad,
                Provincia = paciente.Provincia,
                Telefono = paciente.Telefono,
                Email = paciente.Email,
                ActiveTab = ActiveTab,
            };

            return View(pacienteVM);
        }

        public async Task<IActionResult> UpdatePassword(string password)
        {
            try
            {

                if(string.IsNullOrEmpty(password)) {
                    TempData["ErrorMessage"] = "Por favor, agregue una contraseña";
                    return RedirectToAction("Details");
                }

                var user = await _userManager.GetUserAsync(User);
                if(user == null) {
                    return NotFound();
                }

                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, resetToken, password);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Contraseña actualizada correctamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error al actualizar la contraseña.";
                }

                return RedirectToAction("Details");
            }
            catch
            {
                TempData["ErrorMessage"] = "Error al actualizar la contraseña.";
                return RedirectToAction("Details");
            }
        }

        [HttpPost]
        public IActionResult UpdateProfile(ProfileVM model)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ActiveTab");
            if (ModelState.IsValid)
            {
                try
                {
                    var paciente = _pacienteService.GetById(model.Id);
                    paciente.Nombre = model.Nombre;
                    paciente.Apellido = model.Apellido;
                    paciente.FechaNacimiento = model.FechaNacimiento;
                    paciente.DNI = model.DNI;
                    paciente.Domicilio = model.Domicilio;
                    paciente.Ciudad = model.Ciudad;
                    paciente.Provincia = model.Provincia;
                    paciente.Telefono = model.Telefono;
                    paciente.Email = model.Email;

                    _pacienteService.Update(paciente);

                    TempData["SuccessMessage"] = "Datos actualizados correctamente.";
                    return RedirectToAction("Details");
                } catch (Exception ex) {
                    TempData["ErrorMessage"] = "Error al actualizar los datos." + ex.Message;
                    return RedirectToAction("Details");
                }
            } 
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
