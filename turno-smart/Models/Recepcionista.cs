using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace turno_smart.Models
{
    [Table("Recepcionistas")]
    public class Recepcionista
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public int DNI { get; set; }
        public required string Email { get; set; }
        public virtual Usuarios Usuario { get; set; }
    }
}
