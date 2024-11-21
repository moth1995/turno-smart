using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using turno_smart.Interfaces;
using turno_smart.Models;
using turno_smart.ViewModels.TurnoVM;

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

        [HttpGet]
        public IActionResult Index(string? filter)
		{

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var listTurnos = new ListTurnosVM();

            if(!string.IsNullOrEmpty(filter)) {
                listTurnos.Turnos = _turnoService.GetAll(filter);
            } else {
                listTurnos.Turnos = _turnoService.GetAll();
            }

            return View(listTurnos);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? medicoId)
        {
            if (medicoId == null)
            {
                return RedirectToAction("Index", "Medico");
            }

            var medico = _medicoService.GetById((int)medicoId);
            if (medico == null)
            {
                return NotFound();
            }
            var availableSlots = await _medicoService.GetAvailableSlotsAsync(medico.Id, 60, new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(0, 30, 0));
            var vm = new CreateTurnoVM
            {
                PacienteId = 2,
                MedicoId = medico.Id,
                MedicoNombre = $"{medico.Nombre} {medico.Apellido}",
                MedicoEspecialidad = medico.Especialidad.Nombre,
                AvailableDates = availableSlots.Keys.ToList(),
                AvailableSlots = availableSlots,
                MotivoConsulta = "",
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(CreateTurnoVM vm)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DateTime selectedDateTime;
            if (DateTime.TryParse($"{vm.SelectedDate} {vm.SelectedTime}", out selectedDateTime))
            {
                Console.WriteLine($"Fecha y hora seleccionadas: {selectedDateTime}");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "La fecha y hora seleccionadas no son válidas.");
                return View(vm);
            }
            try
            {
                var turno = new Turno
                {
                    IdPaciente = vm.PacienteId,
                    IdMedico = vm.MedicoId,
                    Estado = "RESERVADO",
                    FechaTurno = selectedDateTime,
                    MotivoConsulta = vm.MotivoConsulta,

                };

                _turnoService.Create(turno);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
		}

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try {
                var turno = _turnoService.GetById(id);

                if(turno == null) return NotFound();

                var vm = new EditTurnoVM
                {
                    Id = turno.Id,
                    IdPaciente = turno.IdPaciente,
                    IdMedico = turno.IdMedico,
                    FechaTurno = turno.FechaTurno,
                    Medicos = _medicoService.GetAll().Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = e.Nombre
                    }).ToList(),
                    Pacientes = _pacienteService.GetAll().Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = e.Nombre
                    }).ToList()
                };

                return View(vm);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Edit(EditTurnoVM vm)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try {
                var turno = _turnoService.GetById(vm.Id);

                if(turno == null) return NotFound();

                turno.IdPaciente = vm.IdPaciente;
                turno.IdMedico = vm.IdMedico;
                turno.FechaTurno = vm.FechaTurno;

                _turnoService.Update(turno);

                return RedirectToAction("Index");
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try {
                var turno = _turnoService.GetById(id);
                if(turno == null) return NotFound();

                return View(turno);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteConfirmed(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try {
                var turno = _turnoService.GetById(id);

                if(turno == null) return NotFound();
                _turnoService.Delete(id);

                return RedirectToAction("Index");
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}