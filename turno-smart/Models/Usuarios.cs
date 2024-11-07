using Microsoft.AspNetCore.Identity;

namespace turno_smart.Models
{
    public class Usuarios : IdentityUser
    {
        public int DNI { get; set; }
    }
}
