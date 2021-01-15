using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Entidad
{
    public abstract class BasePersistente
    {
        [NotMapped]
        [DefaultValue(false)]
        public bool EsNuevo { get; set; }
        [NotMapped]
        [DefaultValue(false)]
        public bool EsModificado { get; set; }
        [NotMapped]
        [DefaultValue(false)]
        public bool EsBorrado { get; set; }
    }
}
