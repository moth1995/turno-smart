using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
//using turno_smart.Models;
using turno_smart.ViewModels;

namespace turno_smart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["LoginModel"] = new Areas.Identity.Pages.Account.LoginModel.InputModel();
            var doctors = new List<DoctorVM>
        {
            new DoctorVM
            {
                Image = "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?auto=format&fit=crop&q=80&w=300&h=300",
                Name = "DRA. SANDRA HERRERA",
                Specialty = "Ginecolog�a",
                Description = "Con m�s de una d�cada de experiencia, la Dra. Herrera es la residente experta en ginecolog�a general y salud de la mujer."
            },
            new DoctorVM
            {
                Image = "https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?auto=format&fit=crop&q=80&w=300&h=300",
                Name = "DR. ARTURO DE LA PE�A",
                Specialty = "Neurolog�a",
                Description = "Como m�dico en jefe de Centro M�dico del Bosque, el Dr. de la Pe�a se especializa en cirug�a."
            },
            new DoctorVM
            {
                Image = "https://images.unsplash.com/photo-1594824476967-48c8b964273f?auto=format&fit=crop&q=80&w=300&h=300",
                Name = "DRA. NATALIA RAMOS",
                Specialty = "Cardiolog�a",
                Description = "La Dra. Ramos cuenta con m�s de 15 a�os de experiencia en el �rea de cardiolog�a."
            },
            new DoctorVM
            {
                Image = "https://images.unsplash.com/photo-1622253692010-333f2da6031d?auto=format&fit=crop&q=80&w=300&h=300",
                Name = "DR. MARCO VALENCIA",
                Specialty = "Pediatr�a",
                Description = "Especializado en el cuidado integral de ni�os y adolescentes, con enfoque en desarrollo infantil."
            },
            new DoctorVM
            {
                Image = "https://images.unsplash.com/photo-1651008376811-b90baee60c1f?auto=format&fit=crop&q=80&w=300&h=300",
                Name = "DRA. CAROLINA MENDEZ",
                Specialty = "Dermatolog�a",
                Description = "Experta en tratamientos dermatol�gicos avanzados y cirug�a dermatol�gica."
            },
            new DoctorVM
            {
                Image = "https://images.unsplash.com/photo-1637059824899-a441006a6875?auto=format&fit=crop&q=80&w=300&h=300",
                Name = "DR. LUIS RODRIGUEZ",
                Specialty = "Traumatolog�a",
                Description = "Especialista en lesiones deportivas y rehabilitaci�n f�sica integral."
            }
        };

            return View(doctors);
        }

        public IActionResult Servicios()
        {
            return View();
        }

        public IActionResult Contacto()
        {
            return View();
        }

        public IActionResult Institucion()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
