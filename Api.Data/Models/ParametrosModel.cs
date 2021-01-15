using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compliance.Data.Entitites;

namespace Compliance.Data.Models
{
    public class ParametrosModel
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Vigente { get; set; }

        public int TipoParametroId { get; set; }
        public TipoParametrosModel Tipo { get; set; }
    }
}
