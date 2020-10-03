using System;
using System.Collections.Generic;

namespace despensa.Models
{
    public partial class Genero
    {
        public Genero()
        {
            Cliente = new HashSet<Cliente>();
            Empleado = new HashSet<Empleado>();
        }

        public int CodGenero { get; set; }
        public string Genero1 { get; set; }

        public virtual ICollection<Cliente> Cliente { get; set; }
        public virtual ICollection<Empleado> Empleado { get; set; }
    }
}
