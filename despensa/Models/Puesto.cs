using System;
using System.Collections.Generic;

namespace despensa.Models
{
    public partial class Puesto
    {
        public Puesto()
        {
            Empleado = new HashSet<Empleado>();
        }

        public int CodPuesto { get; set; }
        public string Puesto1 { get; set; }

        public virtual ICollection<Empleado> Empleado { get; set; }
    }
}
