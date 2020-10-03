using System;
using System.Collections.Generic;

namespace despensa.Models
{
    public partial class Marca
    {
        public Marca()
        {
            Producto = new HashSet<Producto>();
        }

        public int CodMarca { get; set; }
        public string Marca1 { get; set; }

        public virtual ICollection<Producto> Producto { get; set; }
    }
}
