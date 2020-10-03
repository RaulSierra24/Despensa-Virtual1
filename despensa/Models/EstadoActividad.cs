using System;
using System.Collections.Generic;

namespace despensa.Models
{
    public partial class EstadoActividad
    {
        public EstadoActividad()
        {
            Categoria = new HashSet<Categoria>();
            Cliente = new HashSet<Cliente>();
            Empleado = new HashSet<Empleado>();
            Producto = new HashSet<Producto>();
            Proveedor = new HashSet<Proveedor>();
        }

        public int CodEstado { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<Categoria> Categoria { get; set; }
        public virtual ICollection<Cliente> Cliente { get; set; }
        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
        public virtual ICollection<Proveedor> Proveedor { get; set; }
    }
}
