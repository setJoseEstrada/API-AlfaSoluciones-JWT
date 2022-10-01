using System;
using System.Collections.Generic;

#nullable disable

namespace Alfa.Models
{
    public partial class Beca
    {
        public Beca()
        {
            BecasAlumnos = new HashSet<BecasAlumno>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdTipo { get; set; }

        public virtual TipoBeca IdTipoNavigation { get; set; }
        public virtual ICollection<BecasAlumno> BecasAlumnos { get; set; }
    }
}
