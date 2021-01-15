using System;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;
using Api.Web.Filters;
using Api.Entidad;

namespace Api.Web.Controllers
{
    public partial class AdministracionController
    {
        const string RUTA_Usuarios_GET = "Usuario/{paramsFiltro?}";
        [Route(RUTA_Usuarios_GET)]
        [FincappExceptionFilter]
        [HttpGet]
        public IHttpActionResult GetUsuario([FromUri] string paramsFiltro = null)
        {
            var filtro = ParsearFiltroGenerico<FiltroUsuario>(paramsFiltro);
            if (filtro != null)
            {
                Negocio.Administracion.Usuario boUsuario = new Negocio.Administracion.Usuario();
                List<Usuario> listaUsuarioRetornar = new List<Usuario>();
                boUsuario.ObtenerUsuario(filtro, out listaUsuarioRetornar);
                return Ok(listaUsuarioRetornar);
            }
            else
                return BadRequest("Filtro vacio");
        }

        const string RUTA_Usuarios_POST = "Usuario";
        [Route(RUTA_Usuarios_POST)]
        [AllowAnonymous]
        [FincappExceptionFilter]
        [HttpPost]
        public IHttpActionResult PostUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Cedula))
                return BadRequest("Objeto faltan datos");

            Negocio.Administracion.Usuario boUsuario = new Negocio.Administracion.Usuario();
            List<Usuario> usuarios;
            boUsuario.ObtenerUsuario(new FiltroUsuario() { Cedula = usuario.Cedula }, out usuarios);
            if (usuarios.Count == 0)
            {
                usuario.FechaRegistro = DateTime.Now;
                usuario.EsNuevo = true;
                if (usuario.Documentos != null && usuario.Documentos.Any())
                    usuario.Documentos.ToList().ForEach(f => f.FechaVigencia = DateTime.Now);

                boUsuario.GuardarUsuario(new List<Usuario>() { usuario });

                return Ok(usuario);
            }
            else
                return BadRequest("Objeto ya existe");
        }

        //const string RUTA_Usuarios_PUT = "Usuario";
        //[Route(RUTA_Usuarios_PUT)]
        //[FincappExceptionFilter]
        //[HttpPut]
        //public IHttpActionResult PutUsuario([FromBody] Usuario usuario)
        //{
        //    if (usuario == null && usuario.Id == 0)
        //        return BadRequest("Objeto vacio");
        //    Negocio.Administracion.Usuario boUsuario = new Negocio.Administracion.Usuario();
        //    Datos.ActualizarUsuario(usuario);
        //    //Datos.SaveAll();

        //    return Ok();
        //}
    }
}