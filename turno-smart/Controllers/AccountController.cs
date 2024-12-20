using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using turno_smart.Models;
using turno_smart.ViewModels.AccountVM;
using turno_smart.Interfaces;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
                    ModelState.AddModelError("", "DNI ingresado invalido");
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
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
