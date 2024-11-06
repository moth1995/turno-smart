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
                Specialty = "Ginecología",
                Description = "Con más de una década de experiencia, la Dra. Herrera es la residente experta en ginecología general y salud de la mujer."
            },
            new DoctorVM
            {
                Image = "https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?auto=format&fit=crop&q=80&w=300&h=300",
                Name = "DR. ARTURO DE LA PEÑA",
                Specialty = "Neurología",
                Description = "Como médico en jefe de Centro Médico del Bosque, el Dr. de la Peña se especializa en cirugía."
            },
            new DoctorVM
            {
                Image = "https://images.unsplash.com/photo-1594824476967-48c8b964273f?auto=format&fit=crop&q=80&w=300&h=300",
                Name = "DRA. NATALIA RAMOS",
                Specialty = "Cardiología",
                Description = "La Dra. Ramos cuenta con más de 15 años de experiencia en el área de cardiología."
            },
            new DoctorVM
            {
                Image = "https://images.unsplash.com/photo-1622253692010-333f2da6031d?auto=format&fit=crop&q=80&w=300&h=300",
                Name = "DR. MARCO VALENCIA",
                Specialty = "Pediatría",
                Description = "Especializado en el cuidado integral de niños y adolescentes, con enfoque en desarrollo infantil."
            },
            new DoctorVM
            {
                Image = "https://images.unsplash.com/photo-1651008376811-b90baee60c1f?auto=format&fit=crop&q=80&w=300&h=300",
                Name = "DRA. CAROLINA MENDEZ",
                Specialty = "Dermatología",
                Description = "Experta en tratamientos dermatológicos avanzados y cirugía dermatológica."
            },
            new DoctorVM
            {
                Image = "https://images.unsplash.com/photo-1637059824899-a441006a6875?auto=format&fit=crop&q=80&w=300&h=300",
                Name = "DR. LUIS RODRIGUEZ",
                Specialty = "Traumatología",
                Description = "Especialista en lesiones deportivas y rehabilitación física integral."
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
