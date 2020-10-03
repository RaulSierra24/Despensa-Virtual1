using System;
using System.Collections.Generic;

namespace despensa.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Cliente = new HashSet<Cliente>();
            Empleado = new HashSet<Empleado>();
        }

        public int CodRol { get; set; }
        public string Rol1 { get; set; }

        public virtual ICollection<Cliente> Cliente { get; set; }
        public virtual ICollection<Empleado> Empleado { get; set; }
    }
}
