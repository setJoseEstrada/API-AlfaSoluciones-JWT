using System.ComponentModel.DataAnnotations;

namespace Alfa.Models.Request
{
    public class AuthRequest
    {
        [Required]
        public string correo { get; set; }
        [Required]
        public string contrasena { get; set; }
    }
}
