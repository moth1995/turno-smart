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
        public IActionResult Create()
        {

            var medicos = _medicoService.GetAll();

            var vm = new CreateTurnoVM
            {
                IdPaciente = 0,
                IdMedico = 0,
                FechaTurno = DateTime.Now,
                Medicos = medicos.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nombre
                }).ToList(),

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

            try
            {
                var turno = new Turno
                {
                    IdPaciente = vm.IdPaciente,
                    IdMedico = vm.IdMedico,
                    FechaTurno = vm.FechaTurno
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