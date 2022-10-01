using System;
using System.Collections.Generic;

#nullable disable

namespace Alfa.Models
{
    public partial class TipoBeca
    {
        public TipoBeca()
        {
            Becas = new HashSet<Beca>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Beca> Becas { get; set; }
    }
}
