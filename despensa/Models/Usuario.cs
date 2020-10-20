using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace despensa.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Comentario = new HashSet<Comentario>();
            PredidoFacturaCodClienteNavigation = new HashSet<PredidoFactura>();
            PredidoFacturaCodEmpleadoNavigation = new HashSet<PredidoFactura>();
            Puesto = new HashSet<Puesto>();
        }

        public int CodUsuario { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Contraseña { get; set; }
        public string CodGenero { get; set; }
        public string Cui { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Nit { get; set; }
        public string CorreoElectronico { get; set; }
        public DateTime? FecNac { get; set; }
        public int? CodGeneo { get; set; }
        public int? CodRol { get; set; }
        public int? CodEstado { get; set; }
        public string Grid { get; set; }
        public string ImagenPerfil { get; set; }
        public int? PedidoFavorito { get; set; }




        public virtual EstadoActividad CodEstadoNavigation { get; set; }
        public virtual Genero CodGeneoNavigation { get; set; }
        public virtual Rol CodRolNavigation { get; set; }
        public virtual ICollection<Comentario> Comentario { get; set; }
        public virtual ICollection<Factura> FacturaCodClienteNavigation { get; set; }
        public virtual ICollection<Factura> FacturaCodEmpleadoNavigation { get; set; }
        public virtual ICollection<PredidoFactura> PredidoFacturaCodClienteNavigation { get; set; }
        public virtual ICollection<PredidoFactura> PredidoFacturaCodEmpleadoNavigation { get; set; }
        public virtual ICollection<Puesto> Puesto { get; set; }

        [NotMapped]
        public String ConfirmarContraseña { get; set; }

        [NotMapped]
        public String imagens { get; set; }
    }
}
