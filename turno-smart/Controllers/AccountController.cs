using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using turno_smart.Models;
using turno_smart.ViewModels;

namespace turno_smart.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<Usuarios> signInManager;
        private readonly UserManager<Usuarios> userManager;
        public AccountController(ILogger<HomeController> logger, SignInManager<Usuarios> signInManager, UserManager<Usuarios> userManager)
        {
            _logger = logger;
            this.signInManager = signInManager;
            this.userManager = userManager;
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
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email or password is incorrect.");
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
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
                    return RedirectToAction("Index", "Home");
                }
                Usuarios users = new Usuarios
                {
                    Email = model.Email,
                    UserName = model.Email,
                    DNI = dni,
                };

                var result = await userManager.CreateAsync(users, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
