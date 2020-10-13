using System;
using System.Collections.Generic;

namespace despensa.Models
{
    public partial class Comentario
    {
        public int CodComentario { get; set; }
        public string Comentario1 { get; set; }
        public int? CodCliente { get; set; }

        public virtual Usuario CodClienteNavigation { get; set; }
    }
}
