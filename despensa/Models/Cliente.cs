using System;
using System.Collections.Generic;

namespace despensa.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Comentario = new HashSet<Comentario>();
            PredidoFactura = new HashSet<PredidoFactura>();
        }

        public int CodCliente { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Cui { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Nit { get; set; }
        public string CorreoElectronico { get; set; }
        public DateTime? FecNacimiento { get; set; }
        public string Contrasenia { get; set; }
        public int? CodGenero { get; set; }
        public int? CodRol { get; set; }
        public int? CodEstado { get; set; }

        public virtual EstadoActividad CodEstadoNavigation { get; set; }
        public virtual Genero CodGeneroNavigation { get; set; }
        public virtual Rol CodRolNavigation { get; set; }
        public virtual ICollection<Comentario> Comentario { get; set; }
        public virtual ICollection<PredidoFactura> PredidoFactura { get; set; }
    }
}
