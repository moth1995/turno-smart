using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace turno_smart.Models
{   
    [Table("Usuarios")]
    public class Usuarios : IdentityUser
    {
        [Key]
        public required int DNI { get; set; }
    }
}
