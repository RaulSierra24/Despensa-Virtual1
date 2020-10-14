using System;
using System.Collections.Generic;

namespace despensa.Models
{
    public partial class Factura
    {
        public int CodFactura { get; set; }
        public DateTime? Fecha { get; set; }
        public int? CodEmpleado { get; set; }
        public int? CodCliente { get; set; }

        public virtual Usuario CodClienteNavigation { get; set; }
        public virtual Usuario CodEmpleadoNavigation { get; set; }
    }
}
