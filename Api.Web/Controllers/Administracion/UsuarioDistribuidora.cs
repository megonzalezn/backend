using System;
using System.Web.Http;
using Api.Web.Models;
using System.Linq;
using Api.Web.Filters;
using Api.Entidad;
using System.Collections.Generic;

namespace Api.Web.Controllers
{
    public partial class AdministracionController
    {
        const string RUTA_DistribuidoraSolicitudIngreso_POST = "SolicitudIngreso";
        [Route(RUTA_DistribuidoraSolicitudIngreso_POST)]
        [FincappExceptionFilter]
        [HttpPost]
        public IHttpActionResult PostSolicitudIngreso([FromBody] SolicitudIngresoModel solicitudIngreso)
        {
            if (solicitudIngreso == null || solicitudIngreso.IdUsuario == 0 || string.IsNullOrEmpty(solicitudIngreso.IdentificadorDistribuidora))
                return BadRequest("faltan datos");

            List<Distribuidora> resultadoDistribuidora;
            Negocio.Administracion.Distribuidora boDistribuidora = new Negocio.Administracion.Distribuidora();
            boDistribuidora.ObtenerDistribuidora(new FiltroDistribuidora() { Identificador = solicitudIngreso.IdentificadorDistribuidora }, out resultadoDistribuidora);

            if (resultadoDistribuidora != null)
            {
                List<UsuarioDistribuidora> relacion;
                Negocio.Administracion.UsuarioDistribuidora boUsuarioDistribuidora = new Negocio.Administracion.UsuarioDistribuidora();
                int idUsuario = solicitudIngreso.IdUsuario;
                int idDistribuidora = resultadoDistribuidora[0].Id;
                boUsuarioDistribuidora.ObtenerUsuarioDistribuidora(new FiltroUsuarioDistribuidora { IdUsuario = idUsuario, IdDistribuidora = idDistribuidora }, out relacion);

                if (relacion == null)
                {
                    var nuevaRelacion = new UsuarioDistribuidora() { AceptaUsuario = true, IdUsuario = idUsuario, IdDistribuidora = idDistribuidora, FechaCreacion = DateTime.Now, EsNuevo = true };
                    boUsuarioDistribuidora.GuardarUsuarioDistribuidora(new List<UsuarioDistribuidora> { nuevaRelacion });
                    return Ok();
                }
                else
                {
                    if (relacion[0].AceptaDistribuidora)
                        return BadRequest("relacion ya existe");
                    else
                        return BadRequest("relacion pendiente de aprobación");
                }
            }
            else
            {
                return BadRequest("distribuidora no existe");
            }
        }

        const string RUTA_UsuarioDistribuidora_GET = "UsuarioDistribuidora/{paramsFiltro?}";
        [Route(RUTA_UsuarioDistribuidora_GET)]
        [FincappExceptionFilter]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetUsuarioDistribuidora([FromUri] string paramsFiltro = null)
        {
            var filtro = ParsearFiltroGenerico<FiltroUsuarioDistribuidora>(paramsFiltro);
            
            if (filtro != null)
            {
                List<UsuarioDistribuidora> relacionUsuarioDistribuidora;
                Negocio.Administracion.UsuarioDistribuidora boUsuarioDistribuidora = new Negocio.Administracion.UsuarioDistribuidora();
                boUsuarioDistribuidora.ObtenerUsuarioDistribuidora(filtro, out relacionUsuarioDistribuidora);

                return Ok(relacionUsuarioDistribuidora);
            }
            else
                return BadRequest("Filtro vacio");
        }

        const string RUTA_UsuarioDistribuidora_PUT = "UsuarioDistribuidora";
        [Route(RUTA_UsuarioDistribuidora_PUT)]
        [FincappExceptionFilter]
        [HttpPut]
        public IHttpActionResult PutUsuarioDistribuidora([FromBody] UsuarioDistribuidora usuarioDistribuidora)
        {
            if (usuarioDistribuidora == null || usuarioDistribuidora.Id == 0)
                return BadRequest("Objeto vacio");

            Negocio.Administracion.UsuarioDistribuidora boUsuarioDistribuidora = new Negocio.Administracion.UsuarioDistribuidora();
            usuarioDistribuidora.EsModificado = true;

            boUsuarioDistribuidora.GuardarUsuarioDistribuidora(new List<UsuarioDistribuidora> { usuarioDistribuidora } );
            return Ok();
        }

        const string RUTA_UsuarioDistribuidora_POST = "Vinculacion";
        [Route(RUTA_UsuarioDistribuidora_POST)]
        [FincappExceptionFilter]
        [HttpPost]
        public IHttpActionResult PostVinculacion([FromBody] UsuarioDistribuidora usuarioDistribuidora)
        {
            if (usuarioDistribuidora == null)
                return BadRequest("Objeto vacio");

            Negocio.Administracion.UsuarioDistribuidora boUsuarioDistribuidora = new Negocio.Administracion.UsuarioDistribuidora();
            boUsuarioDistribuidora.VinculacionUsuarioDistribuidora(usuarioDistribuidora);
           
            return Ok();
        }
    }
}