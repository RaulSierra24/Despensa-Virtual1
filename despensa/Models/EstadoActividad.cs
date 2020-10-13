using System;
using System.Collections.Generic;

namespace despensa.Models
{
    public partial class EstadoActividad
    {
        public EstadoActividad()
        {
            Categoria = new HashSet<Categoria>();
            Producto = new HashSet<Producto>();
            Proveedor = new HashSet<Proveedor>();
            Usuario = new HashSet<Usuario>();
        }

        public int CodEstado { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<Categoria> Categoria { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
        public virtual ICollection<Proveedor> Proveedor { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
