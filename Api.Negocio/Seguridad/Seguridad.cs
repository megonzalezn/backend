using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Negocio.Seguridad
{
    public class Seguridad
    {
        public bool ValidarUsuario(string login, string password)
        {
            DatosFactory dal = new DatosFactory();
            var usuario = dal.Datos.ObtenerUsuarioPorFiltro(new Entidad.FiltroUsuario { Cedula = login.ToLower() }).FirstOrDefault();
            if (usuario != null)
            {
                string bdPasswordHash;
                string bdSalt;
                //int bdId;
                dal.Datos.BuscarLoginUsuario(usuario.Id, out bdPasswordHash, out bdSalt);

                string passwordYSalt = string.Concat(password, bdSalt);
                string hashedPasswordYSalt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(passwordYSalt, "SHA1");

                return hashedPasswordYSalt.Equals(bdPasswordHash);
            }
            return false;
        }
    }
}
