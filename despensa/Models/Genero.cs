using System;
using System.Collections.Generic;

namespace despensa.Models
{
    public partial class Genero
    {
        public Genero()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int CodGenero { get; set; }
        public string Genero1 { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
