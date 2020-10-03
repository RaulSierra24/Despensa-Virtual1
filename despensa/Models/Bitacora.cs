using System;
using System.Collections.Generic;

namespace despensa.Models
{
    public partial class Bitacora
    {
        public int CodBitacora { get; set; }
        public string Empleado { get; set; }
        public string Accion { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
