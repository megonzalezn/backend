using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Entidad
{
    public class LoginUsuario
    {
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public bool Eliminado { get; set; }
    }
}
