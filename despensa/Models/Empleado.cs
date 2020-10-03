using System;
using System.Collections.Generic;

namespace despensa.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            PredidoFactura = new HashSet<PredidoFactura>();
        }

        public int CodEmpleado { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Contraseña { get; set; }
        public int? CodGenero { get; set; }
        public string Cui { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Direccion { get; set; }
        public int? CodPuesto { get; set; }
        public int? CodEstado { get; set; }
        public int? CodRol { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        public virtual EstadoActividad CodEstadoNavigation { get; set; }
        public virtual Genero CodGeneroNavigation { get; set; }
        public virtual Puesto CodPuestoNavigation { get; set; }
        public virtual Rol CodRolNavigation { get; set; }
        public virtual ICollection<PredidoFactura> PredidoFactura { get; set; }
    }
}
