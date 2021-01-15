using Api.Web.Models;
using System.Linq;
using System.Net;
using System.Web.Http;
using Api.Web.Filters;
using System.Threading.Tasks;
using Api.Web.Mail;
using Api.Entidad;
using System.Collections.Generic;
using System.Web.WebSockets;

namespace Api.Web.Controllers
{
    public partial class LoginController
    {
        [HttpGet]
        [Route("verificar-conexion")]
        public IHttpActionResult VerificarConexion()
        {
            List<Usuario> usuarios;
            Negocio.Administracion.Usuario boUsuario = new Negocio.Administracion.Usuario();
            boUsuario.ObtenerUsuario(new FiltroUsuario { Cedula = "" }, out usuarios);
            return Ok(true);
        }

        [HttpPost]
        [FincappExceptionFilter]
        [Route("usuario")]
        [AllowAnonymous]
        public IHttpActionResult AutenticarUsuario(LoginModel login)
        {
            if (login == null || string.IsNullOrEmpty(login.NombreUsuario) || string.IsNullOrEmpty(login.Pass))
                return Unauthorized();

            Negocio.Seguridad.Seguridad boSeguridad = new Negocio.Seguridad.Seguridad();
            Negocio.Administracion.Usuario boUsuario = new Negocio.Administracion.Usuario();

            if (boSeguridad.ValidarUsuario(login.NombreUsuario, login.Pass))
            {
                List<Usuario> usuarios;
                boUsuario.ObtenerUsuario(new FiltroUsuario { Cedula = login.NombreUsuario.ToLower(), Includes = new string[] { "UsuarioDistribuidora.Distribuidora" } }, out usuarios);
                var usuario = usuarios.FirstOrDefault();
                usuario.RefreshToken = TokenGenerator.GenerateRefreshToken();
                usuario.PushToken = login.PushToken;
                usuario.EsModificado = true;
                boUsuario.GuardarUsuario(new List<Usuario>() { usuario });
                usuario.Token = TokenGenerator.GenerateTokenJWT(login.NombreUsuario.ToLower());

                //if (!string.IsNullOrEmpty(usuario.PushToken))
                //{
                //Api.Firebase.Integrador.PushNotification push = new Firebase.Integrador.PushNotification();
                //Task.Run(() => push.SendPushAsync(usuario.PushToken, $"{usuario.Nombre} {usuario.Apellido}"));
                return Ok(usuario);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [FincappExceptionFilter]
        [Route("usuario/refresh")]
        [AllowAnonymous]
        public IHttpActionResult RefreshUsuario([FromBody] TokenModel payload)
        {
            Negocio.Administracion.Usuario boUsuario = new Negocio.Administracion.Usuario();
            var principal = TokenGenerator.GetPrincipalFromExpiredToken(payload.ApiToken);
            List<Usuario> usuarios;
            boUsuario.ObtenerUsuario(new FiltroUsuario { Cedula = principal.Identity.Name.ToLower(), Includes = new string[] { "UsuarioDistribuidora.Distribuidora" } }, out usuarios);

            var savedRefreshToken = usuarios[0].RefreshToken;
            if (savedRefreshToken != payload.RefreshToken)
                return Unauthorized();

            var newUsuario = usuarios[0];
            newUsuario.RefreshToken = TokenGenerator.GenerateRefreshToken();
            newUsuario.PushToken = payload.PushToken;
            newUsuario.EsModificado = true;
            boUsuario.GuardarUsuario(new List<Usuario>() { newUsuario });
            newUsuario.Token = TokenGenerator.GenerateTokenJWT(newUsuario.Cedula.ToLower());

            // Envío de Notificación push dummy para validar el funcionamiento. Se envia en cada refresh
            if (!string.IsNullOrEmpty(usuarios[0].PushToken))
            {
                Api.Firebase.Integrador.PushNotification push = new Firebase.Integrador.PushNotification();
                Task.Run(() => push.ProcesoPush(new PrivilegioUsuarioDistribuidora() {
                    Proceso = Entidad.Enums.ProcesoPrivilegioUsuarioDistribuidora.AsignaActualiza,
                    IdUsuarioDistribuidora = 1
                }));
            }
            return Ok(newUsuario);
        }

        [HttpPost]
        [FincappExceptionFilter]
        [Route("usuario/cambiarPass")]
        [AllowAnonymous]
        public IHttpActionResult cambiarPasswordUsuario(LoginModel login)
        {
            if (login == null || string.IsNullOrEmpty(login.NombreUsuario) || string.IsNullOrEmpty(login.Pass) || string.IsNullOrEmpty(login.NewPass))
                return BadRequest("Objeto faltan datos");

            Negocio.Administracion.Usuario boUsuario = new Negocio.Administracion.Usuario();
            Negocio.Seguridad.Seguridad boSeguridad = new Negocio.Seguridad.Seguridad();

            List<Usuario> usuarios;
            boUsuario.ObtenerUsuario(new FiltroUsuario { Cedula = login.NombreUsuario.ToLower() }, out usuarios);

            if (boSeguridad.ValidarUsuario(login.NombreUsuario, login.Pass))
            {
                usuarios[0].Pin = login.NewPass;
                usuarios[0].EsModificado = true;
                boUsuario.CambiarPassword(usuarios[0]);
                return Ok();
            }
            else
            {
                return BadRequest("Contraseña Incorrecta.");
            }
        }

        [HttpPost]
        [FincappExceptionFilter]
        [Route("usuario/recuperarPin")]
        [AllowAnonymous]
        public IHttpActionResult RecuperarPinUsuario(LoginModel login)
        {
            if (login == null || string.IsNullOrEmpty(login.NombreUsuario))
                return BadRequest("Objeto faltan datos");

            Negocio.Administracion.Usuario boUsuario = new Negocio.Administracion.Usuario();
            List<Usuario> usuarios;
            boUsuario.ObtenerUsuario(new FiltroUsuario { Cedula = login.NombreUsuario.ToLower() }, out usuarios);

            if (usuarios?[0].Id > 0)
            {
                var newUsuario = usuarios.FirstOrDefault();
                newUsuario.Pin = Util.Cryptojs.GenerateRandomNo().ToString();
                newUsuario.EsModificado = newUsuario.DebeCambiarClave = true;

                boUsuario.GuardarUsuario(new List<Usuario>() { newUsuario });
                boUsuario.CambiarPassword(newUsuario);
                Smtp.SendMail(newUsuario, "recuperarPin");

                return Ok();
            }
            else
            {
                return BadRequest("Objeto no existe");
            }
        }
    }
}