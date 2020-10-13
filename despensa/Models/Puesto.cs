using System;
using System.Collections.Generic;

namespace despensa.Models
{
    public partial class Puesto
    {
        public int CodPuesto { get; set; }
        public string Puesto1 { get; set; }
        public int? CodEmpleado { get; set; }

        public virtual Usuario CodEmpleadoNavigation { get; set; }
    }
}
