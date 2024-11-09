using Microsoft.AspNetCore.Mvc;
using turno_smart.Interfaces;

namespace turno_smart.Controllers
{
    public class TurnoController(
        ITurnoService turnoService,
        IMedicoService medicoService,
        IPacienteService pacienteService
    ): Controller {

        private readonly ITurnoService _turnoService = turnoService;
        private readonly IMedicoService _medicoService = medicoService;
        private readonly IPacienteService _pacienteService = pacienteService;

        public IActionResult Index(string? filter)
		{

            throw new NotImplementedException();
		}
    }
}