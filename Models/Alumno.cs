using System;
using System.Collections.Generic;

#nullable disable

namespace Alfa.Models
{
    public partial class Alumno
    {
        public Alumno()
        {
            BecasAlumnos = new HashSet<BecasAlumno>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Genero { get; set; }
        public int Edad { get; set; }

        public virtual ICollection<BecasAlumno> BecasAlumnos { get; set; }
    }
}
