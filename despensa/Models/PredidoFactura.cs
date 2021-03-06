﻿using System;
using System.Collections.Generic;

namespace despensa.Models
{
    public partial class PredidoFactura
    {
        public PredidoFactura()
        {
            DetalleFactura = new HashSet<DetalleFactura>();
        }

        public int CodFactura { get; set; }
        public DateTime? FecEmision { get; set; }
        public decimal? TotalVendido { get; set; }
        public decimal? TotalCosto { get; set; }
        public int? CodEmpleado { get; set; }
        public int? CodCliente { get; set; }
        public int? CodEstado { get; set; }
        public string Direccion_entrega { get; set; }

        public virtual Usuario CodClienteNavigation { get; set; }
        public virtual Usuario CodEmpleadoNavigation { get; set; }
        public virtual EstadoPedido CodEstadoNavigation { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFactura { get; set; }
    }
}
