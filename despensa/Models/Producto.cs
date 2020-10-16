using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace despensa.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleFactura = new HashSet<DetalleFactura>();
        }

        public int CodProducto { get; set; }
        public string Nombre { get; set; }
        public DateTime? FecCaducidad { get; set; }
        public decimal? PrecioCosto { get; set; }
        public decimal? PrecioVenta { get; set; }
        public string Imagen { get; set; }
        public string Peso { get; set; }
        public int? CodEstado { get; set; }
        public int? Cantidad { get; set; }
        public int? CodProveedor { get; set; }
        public int? CodMarca { get; set; }
        public int? CodCategoria { get; set; }

        public virtual Categoria CodCategoriaNavigation { get; set; }
        public virtual EstadoActividad CodEstadoNavigation { get; set; }
        public virtual Marca CodMarcaNavigation { get; set; }
        public virtual Proveedor CodProveedorNavigation { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFactura { get; set; }


        [NotMapped]
        public IFormFile ImageFie { get; set; }
    }
}
