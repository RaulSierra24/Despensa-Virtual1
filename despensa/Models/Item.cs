using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace despensa.Models
{
    public class Item
    {
        public Producto Product { get; set; }

        public int Quantity { get; set; }

        public decimal? precio_total_articulo { get; set; }


    }
}
