using Compliance.Data.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Compliance.Data.Models
{
    public class TipoParametrosModel
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Vigente { get; set; }

        public ICollection<ParametrosModel> Parametros { get; set; }
    }
}
