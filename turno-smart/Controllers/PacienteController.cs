using Microsoft.AspNetCore.Mvc;
using turno_smart.Interfaces;

namespace turno_smart.Controllers
{
    
    public class PacienteController(IPacienteService pacienteService) : Controller {

        private readonly IPacienteService _pacienteService = pacienteService;
    }
}