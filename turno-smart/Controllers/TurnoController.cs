using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using turno_smart.Interfaces;
using turno_smart.Models;
using turno_smart.ViewModels.TurnoVM;

namespace turno_smart.Controllers
{
    public class TurnoController(
        ITurnoService turnoService,
        IMedicoService medicoService,
        UserManager<Usuarios> userManager
    ) : Controller {

        private readonly ITurnoService _turnoService = turnoService;
        private readonly IMedicoService _medicoService = medicoService;
        private readonly UserManager<Usuarios> _userManager = userManager;

        [HttpGet]
        [Authorize(Roles = "Admin, Paciente, Medico, Recepcionista")]
        public async Task<IActionResult> Index(string? filter, DateTime? date)
        {

            if (!ModelState.IsValid)
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
            else if (await _userManager.IsInRoleAsync(currentUser, "Recepcionista"))
            {
                turnos = await _turnoService.GetAll(filter, null, null);
                turnos = turnos.Where(t => t.FechaTurno.Date == currentDate.Date).ToList();
            }
            if (filter == "last")
            {
                turnos = turnos.OrderByDescending(t => t.FechaTurno).Take(1).ToList();
            }
            else if (filter == "first")
            {
                turnos = turnos.OrderBy(t => t.FechaTurno).Take(1).ToList();
            }
            else if (filter == "confirmado" || filter == "reservado" || filter == "cancelado")
            {
                turnos = turnos.Where(t => t.Estado.ToLower() == filter).ToList();
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
        [HttpGet]
        [Authorize(Roles = "Admin, Paciente, Recepcionista")]
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
        [Authorize(Roles = "Admin,Paciente, Recepcionista")]
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
                ModelState.AddModelError(string.Empty, "La fecha y hora seleccionadas no son v�lidas.");
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

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
		}

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try 
            {
                var turno = _turnoService.GetById(id);

                if(turno == null) return NotFound();
                var availableSlots = await _medicoService.GetAvailableSlotsAsync(turno.IdMedico, 60, new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(0, 30, 0));

                var vm = new EditTurnoVM
                {
                    TurnoId = turno.Id,
                    MedicoNombre = turno.Medico.FullName(),
                    MedicoEspecialidad = turno.Medico.Especialidad.Nombre,
                    MotivoConsulta = turno.MotivoConsulta,
                    SelectedDate = turno.FechaTurno.Date.ToString("d"),
                    SelectedTime = turno.FechaTurno.ToString("t"),
                    AvailableDates = availableSlots.Keys.ToList(),
                    AvailableSlots = availableSlots,
                };

                return View(vm);
            } 
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTurnoVM vm)
        {
            DateTime selectedDateTime;

            if (!ModelState.IsValid || !DateTime.TryParse($"{vm.SelectedDate} {vm.SelectedTime}", out selectedDateTime))
            {
                if (vm == null) return NotFound();
                var turno = _turnoService.GetById(vm.TurnoId);

                if (turno == null) return NotFound();
                var availableSlots = await _medicoService.GetAvailableSlotsAsync(turno.IdMedico, 60, new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(0, 30, 0));
                
                ModelState.AddModelError(string.Empty, "La fecha y hora seleccionadas no son v�lidas.");
                var newVM = new EditTurnoVM
                {
                    TurnoId = turno.Id,
                    MedicoNombre = turno.Medico.FullName(),
                    MedicoEspecialidad = turno.Medico.Especialidad.Nombre,
                    MotivoConsulta = turno.MotivoConsulta,
                    SelectedDate = turno.FechaTurno.Date.ToString("d"),
                    SelectedTime = turno.FechaTurno.ToString("t"),
                    AvailableDates = availableSlots.Keys.ToList(),
                    AvailableSlots = availableSlots,
                };

                return View(newVM);
            }
            try
            {
                var turno = _turnoService.GetById(vm.TurnoId);

                if(turno == null) return NotFound();

                turno.FechaTurno = selectedDateTime;
                turno.MotivoConsulta = vm.MotivoConsulta;
                turno.Estado = "RESERVADO";

                _turnoService.Update(turno);

                return Json(new { redirectUrl = Url.Action("Index") });
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult TurnoConfirmed(int id, DateTime date)
        {
            try
            {
                var turno = _turnoService.GetById(id);

                if (turno == null) return NotFound();
                turno.Estado = "CONFIRMADO";
                _turnoService.Update(turno);

                return RedirectToAction("Index", new { date = date });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var turno = _turnoService.GetById(id);
                if(turno == null) return NotFound();
                var vm = new DeleteTurnoVM()
                {
                    Id = turno.Id,
                    PacienteNombre = turno.Paciente.FullName(),
                    MedicoEspecialidad = turno.Medico.Especialidad.Nombre,
                    FechaTurno = turno.FechaTurno,
                };
                return View(vm);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try 
            {
                var turno = _turnoService.GetById(id);

                if(turno == null) return NotFound();
                turno.Estado = "Cancelado";
                _turnoService.Update(turno);

                return Json(new { redirectUrl = Url.Action("Index") });
            } 
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}