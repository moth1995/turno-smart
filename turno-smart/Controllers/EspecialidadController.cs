using Microsoft.AspNetCore.Mvc;
using turno_smart.Interfaces;

namespace turno_smart.Controllers
{
    public class EspecialidadController(IEspecialidadService especialidadService): Controller {

        private readonly IEspecialidadService _especialidadService = especialidadService;
    }
}