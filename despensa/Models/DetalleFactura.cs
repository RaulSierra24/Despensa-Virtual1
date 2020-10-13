using System;
using System.Collections.Generic;

namespace despensa.Models
{
    public partial class DetalleFactura
    {
        public int CodDetalle { get; set; }
        public int? CodFactura { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Costo { get; set; }
        public int? CodProducto { get; set; }
        public int Cantidad { get; set; }

        public virtual PredidoFactura CodFacturaNavigation { get; set; }
        public virtual Producto CodProductoNavigation { get; set; }
    }
}
