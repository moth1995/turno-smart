using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace turno_smart.Models
{   
    [Table("Usuarios")]
    public class Usuarios : IdentityUser
    {
        public int DNI { get; set; }
        public virtual Paciente? Paciente { get; set; }
        public virtual Medico? Medico { get; set; }
    }
}
