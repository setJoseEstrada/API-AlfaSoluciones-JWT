using System;
using System.Collections.Generic;

#nullable disable

namespace Alfa.Models
{
    public partial class BecasAlumno
    {
        public int Id { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdBecas { get; set; }

        public virtual Alumno IdAlumnoNavigation { get; set; }
        public virtual Beca IdBecasNavigation { get; set; }
    }
}
