﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using turno_smart.Data.Migrations;

namespace turno_smart.Models
{
    [Table("Turnos")]
    public class Turno
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public DateTime FechaTurno { get; set; }
        public virtual Paciente Paciente { get; set; }
        public virtual Medico Medico { get; set; }
    }
}