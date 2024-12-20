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
        public bool IsAdmin { get; set; }
        //public string FirstName { get; set; } // Propiedad para el nombre
        //public string LastName { get; set; } // Propiedad para el apellido

        //public string FullName()
        //{
        //    return $"{FirstName} {LastName}";
        //}

    }
}
