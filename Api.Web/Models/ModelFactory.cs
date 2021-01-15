using System;
using System.Collections.Generic;
using System.Linq;
using Api.Entidad;
using System.Net.Http;
using System.Web.Http.Routing;
using System.Text;
using System.Threading;
using System.Configuration;

namespace Api.Web.Models
{
    public class ModelFactory
    {
        private UrlHelper _UrlHelper;
        //private IApiRepository _repo;

        public ModelFactory(HttpRequestMessage request)
        {
            _UrlHelper = new UrlHelper(request);
            //_repo = repo;
        }
        
        public UsuarioModel Create(Usuario usuario)
        {
            if (usuario == null) return null;
            return new UsuarioModel()
            {
                Id = usuario.Id,
                Cedula = usuario.Cedula,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Mail = usuario.Mail,
                Telefono = usuario.Telefono,
                RefreshToken = usuario.RefreshToken,
                DebeCambiarClave = usuario.DebeCambiarClave
            };
        }

        public Usuario Parse(UsuarioModel model)
        {
            try
            {
                var usuario = new Usuario()
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    //Pin = string.IsNullOrEmpty(model.Password) ? this.CrearPass() : model.Password,
                    Telefono = model.Telefono,
                    FechaRegistro = DateTime.Now
                };

                return usuario;
            }catch(Exception)
            {
                return null;
            }
        }

        //private string CrearPass()
        //{
        //    StringBuilder sbPass = new StringBuilder();
        //    Random r = new Random();

        //    string Alpha = "abcdefghijkmnopqrstuvwxyzABCDEFGHIJKLMNPQRSTUVWXYZ1234567890";
        //    string Numeros = "1234567890";
        //    for (int i = 0; i < 7; i++)
        //    {
        //        sbPass.Append(Alpha.Substring(r.Next(Alpha.Length), 1));
        //    }
        //    sbPass.Append(Numeros.Substring(r.Next(Numeros.Length), 1));
        //    Api.Data.Util.Cryptojs sAES = new Api.Data.Util.Cryptojs();
        //    return Api.Data.Util.Cryptojs.EncryptToString(sbPass.ToString());
        //}
    }
}
