using Microsoft.AspNetCore.Mvc;
using turno_smart.Interfaces;
using turno_smart.Models;
using turno_smart.ViewModels.HistorialMedicoVM;

namespace turno_smart.Controllers
{
    public class HistorialMedicoController(
        IHistorialMedicoService historialMedicoService,
        IPacienteService pacienteService,
        IMedicoService medicoService,
        IEspecialidadService especialidadService
    ) : Controller
    {

        private readonly IHistorialMedicoService _historialMedicoService = historialMedicoService;
        private readonly IPacienteService _pacienteService = pacienteService;
        private readonly IMedicoService _medicoService = medicoService;
        private readonly IEspecialidadService _especialidadService = especialidadService;

        [HttpGet]
        public IActionResult Index(int pacienteId, int medicoId, string? filter, int? selectedMedicoId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paciente = _pacienteService.GetById(pacienteId);
            var medico = _medicoService.GetById(medicoId);

            if (paciente==null || medico == null)
            {
                return NotFound();
            }

            var listHistorialMedicoVM = new ListHistorialMedicoVM();
            listHistorialMedicoVM.Medico = medico;
            listHistorialMedicoVM.Paciente = paciente;
            listHistorialMedicoVM.Medicos = _medicoService.GetAll();

            listHistorialMedicoVM.HistorialesMedicos = _historialMedicoService.GetAll(filter);

            if (selectedMedicoId != null)
            {
                listHistorialMedicoVM.HistorialesMedicos = listHistorialMedicoVM.HistorialesMedicos.Where(hm => hm.IdMedico == selectedMedicoId).ToList();
            }

            return View(listHistorialMedicoVM);
        }

        [HttpGet]
        public IActionResult Create(int pacienteId, int medicoId)
        {
            var paciente = _pacienteService.GetById(pacienteId);

            if (paciente == null) return NotFound();

            var vm = new CreateHistorialMedicoVM
            {
                IdPaciente = pacienteId,
                IdMedico = medicoId,
                PacienteNombre = paciente.FullName(),
                Fecha = DateTime.Now,
                Sintomas = "",
                Diagnostico = "",
                Tratamiento = "",
                NotasAdicionales = "",
                Prescripciones = "",
                Seguimiento = "",
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateHistorialMedicoVM vm)
        {
            if (!ModelState.IsValid)
            { 
                return View(vm);
            }

            try
            {
                var historialModel = new HistorialMedico()
                {
                    IdPaciente = vm.IdPaciente,
                    IdMedico = vm.IdMedico,
                    Fecha = vm.Fecha,
                    Sintomas = vm.Sintomas,
                    Diagnostico = vm.Diagnostico,
                    Tratamiento = vm.Tratamiento,
                    NotasAdicionales = vm.NotasAdicionales,
                    Prescripciones = vm.Prescripciones,
                    Seguimiento = vm.Seguimiento,
                };

                _historialMedicoService.Create(historialModel);

                TempData["SuccessMessage"] = "Historia médica creada correctamente.";
                return Json(new { redirectUrl = Url.Action("Index", new { pacienteId = vm.IdPaciente, medicoId = vm.IdMedico }) });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al intentar crear historia médica." + ex.InnerException?.Message ?? ex.Message;
                return View(vm);
            }
        }
    }
}