using System;
using System.Collections.Generic;

namespace despensa.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int CodRol { get; set; }
        public string Rol1 { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
