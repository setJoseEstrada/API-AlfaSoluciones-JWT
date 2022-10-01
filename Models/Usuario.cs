using System;
using System.Collections.Generic;

#nullable disable

namespace Alfa.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string Nombre { get; set; }
    }
}
