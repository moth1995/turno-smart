using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using turno_smart.Interfaces;
using turno_smart.Models;
using turno_smart.ViewModels.TurnoVM;

namespace turno_smart.Controllers
{
    public class TurnoController(
        ITurnoService turnoService,
        IMedicoService medicoService,
        IPacienteService pacienteService,
        UserManager<Usuarios> userManager
    ) : Controller {

        private readonly ITurnoService _turnoService = turnoService;
        private readonly IMedicoService _medicoService = medicoService;
        private readonly IPacienteService _pacienteService = pacienteService;
        private readonly UserManager<Usuarios> _userManager = userManager;

        [HttpGet]
        [Authorize(Roles = "Admin,Paciente,Medico")]
        public async Task<IActionResult> Index(string? filter, DateTime? date)
		{

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            DateTime currentDate = date ?? DateTime.Today;

            var turnos = new List<Turno>();
            if (await _userManager.IsInRoleAsync(currentUser, "Paciente"))
            {
                turnos = await _turnoService.GetAll(filter, currentUser.Paciente?.Id, null);
            }
            else if (await _userManager.IsInRoleAsync(currentUser, "Medico"))
            {
                turnos = await _turnoService.GetAll(filter, null, currentUser.Medico?.Id);
                turnos = turnos.Where(t => t.FechaTurno.Date == currentDate.Date).ToList();
            }
            else if (await _userManager.IsInRoleAsync(currentUser, "Admin"))
            {
                turnos = await _turnoService.GetAll(filter, null, null);
                turnos = turnos.Where(t => t.FechaTurno.Date == currentDate.Date).ToList();
            }

            var listTurnos = new ListTurnosVM();

            
            listTurnos.Filter = filter;
            listTurnos.CurrentDate = currentDate;
            listTurnos.Turnos = turnos.Select(t => new TurnoVM
            {
                TurnoId = t.Id,
                PacienteId = t.Paciente.Id,
                MedicoId = t.Medico.Id,
                Estado = t.Estado,
                Fecha = t.FechaTurno,
                Hora = t.FechaTurno.TimeOfDay,
                PacienteNombre = t.Paciente.FullName(),
                MedicoNombre = t.Medico.FullName(),
                MedicoEspecialidad = t.Medico.Especialidad.Nombre,
                MotivoConsulta = t.MotivoConsulta,
                
            }).ToList();

            return View(listTurnos);
        }

        [HttpPost]
        public IActionResult ChangeDate(DateTime date, string direction)
        {
            DateTime newDate = direction == "next" ? date.AddDays(1) : date.AddDays(-1);

            return RedirectToAction("Index", new { date = newDate });
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Paciente")]
        public async Task<IActionResult> Create(int? medicoId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

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
                PacienteId = currentUser.Paciente.Id,
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
        [Authorize(Roles = "Admin,Paciente")]
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