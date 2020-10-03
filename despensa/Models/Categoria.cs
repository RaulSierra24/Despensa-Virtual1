using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;


namespace despensa.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Producto = new HashSet<Producto>();
        }

        public int CodCategoria { get; set; }
        public string Nombre { get; set; }
        public int? Estado { get; set; }
        public string Imagen { get; set; }

        public virtual EstadoActividad EstadoNavigation { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }

        [NotMapped]
        public IFormFile ImageFie { get; set; }
    }
}
