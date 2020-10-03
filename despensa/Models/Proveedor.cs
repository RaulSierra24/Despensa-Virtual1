using System;
using System.Collections.Generic;

namespace despensa.Models
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Producto = new HashSet<Producto>();
        }

        public int CodProveedor { get; set; }
        public string Proveedor1 { get; set; }
        public int? CodEstado { get; set; }

        public virtual EstadoActividad CodEstadoNavigation { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
