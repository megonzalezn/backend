using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Entidad
{
    public class Documento
    {
        public int Id { get; set; }
        [ForeignKey("Usuario")]
        public int? IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
        [ForeignKey("Distribuidora")]
        public int? IdDistribuidora { get; set; }
        public virtual Distribuidora Distribuidora { get; set; }
        public string Imagen { get; set; }
        public DateTime FechaVigencia { get; set; }
    }
}
