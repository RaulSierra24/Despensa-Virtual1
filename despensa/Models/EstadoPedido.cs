using System;
using System.Collections.Generic;

namespace despensa.Models
{
    public partial class EstadoPedido
    {
        public EstadoPedido()
        {
            PredidoFactura = new HashSet<PredidoFactura>();
        }

        public int CodEstado { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<PredidoFactura> PredidoFactura { get; set; }
    }
}
