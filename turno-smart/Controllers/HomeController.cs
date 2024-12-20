using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using turno_smart.Data;
using turno_smart.Models;
using turno_smart.ViewModels;
using turno_smart.ViewModels.HomeVM;

namespace turno_smart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context; 

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Obtener un centro médico (el primer registro)
            var centroMedico = await _context.CentroMedico.FirstOrDefaultAsync();

            var random = new Random();

            // Traer todos los médicos de la base de datos
            var allDoctors = await _context.Medicos
                .Include(m => m.Especialidad) // Cargar especialidades relacionadas
                .ToListAsync();

            // Seleccionar aleatoriamente 6 médicos
            var doctors = allDoctors
                .OrderBy(m => random.Next()) // Ordenar aleatoriamente en memoria
                .Take(6) // Tomar solo 6 médicos
                .Select(m => new DoctorVM
                {
                    Image = m.Imagen ?? "",
                    Name = m.FullName(),
                    Specialty = m.Especialidad != null ? m.Especialidad.Nombre : "Sin Especialidad",
                    Description = m.Reseña ?? "Sin descripción disponible"
                })
                .ToList();

            // Crear un ViewModel para enviar ambos datos a la vista
            var viewModel = new HomeVM
            {
                NombreCentroMedico = centroMedico?.Nombre ?? "Centro Médico",
                LemaCentroMedico = centroMedico?.Lema ?? "Donde su salud es primero",
                Doctors = doctors
            };

            return View(viewModel);

        }

        public IActionResult Servicios()
        {
            return View();
        }

        public IActionResult Contacto()
        {
            // Obtén los datos del centro médico
            var centroMedico = _context.CentroMedico.FirstOrDefault();

            var viewModel = new ContactoViewModel
            {
                Direccion = centroMedico?.Direccion ?? "Dirección no disponible",
                Correo = centroMedico?.Correo ?? "mail@example.com",
                Telefono = centroMedico?.Telefono ?? "Teléfono no disponible"
            };

            return View(viewModel);
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
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GestionSitioWeb()
        {
            var centroMedico = _context.CentroMedico.FirstOrDefault();

            if (centroMedico == null)
            {
                centroMedico = new CentroMedico();
                centroMedico.Nombre = "Centro Médico";
                centroMedico.Lema = "Donde su salud es primero";
                centroMedico.Direccion = "Dirección no disponible";
                centroMedico.Correo = "mail@example.com";
                centroMedico.Telefono = "Teléfono no disponible";

                _context.CentroMedico.Add(centroMedico);
            }

            return View(centroMedico);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult GestionSitioWeb(CentroMedico model)
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            _context.Update(model);
            _context.SaveChanges();

            return View(model);
        }
    }
}
