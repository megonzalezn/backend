using System.Web.Http;
using System.Linq;
using Api.Entidad;
using Api.Web.Filters;
using System.Collections.Generic;
using System.Diagnostics;

namespace Api.Web.Controllers
{
    public partial class AdministracionController
    {
        [Route("privilegiousuariodistribuidora/{idUsuarioDistribuidora}")]
        [HttpGet]
        [FincappExceptionFilter]
        [AllowAnonymous]
        public IHttpActionResult GetPrivilegioUsuarioDistribuidora([FromUri] int idUsuarioDistribuidora)
        {
            if (idUsuarioDistribuidora == 0)
                return BadRequest("Objeto Vacío");

            List<PrivilegioUsuarioDistribuidora> Privilegios = new List<PrivilegioUsuarioDistribuidora>();
            using (var boPrivilegio = new Negocio.Administracion.Privilegio())
            {
                List<Privilegio> privilegios;
                boPrivilegio.ObtenerPrivilegio(new FiltroPrivilegio() { Eliminado = false }, out privilegios);

                foreach (var p in privilegios)
                {
                    Privilegios.Add(new PrivilegioUsuarioDistribuidora()
                    {
                        Id = p.Id,
                        IdPrivilegio = p.Id,
                        Nombre = p.Nombre,
                        Descripcion = p.Descripcion
                    });
                }
                using (var boPud = new Negocio.Administracion.PrivilegioUsuarioDistribuidora())
                {
                    List<PrivilegioUsuarioDistribuidora> privilegioUsuarioDistribuidora;
                    boPud.ObtenerPrivilegioUsuarioDistribuidora(new FiltroPrivilegioUsuarioDistribuidora() { IdUsuarioDistribuidora = idUsuarioDistribuidora }, out privilegioUsuarioDistribuidora);
                    Privilegios.ForEach(p =>
                    {
                        p.Seleccionado = privilegioUsuarioDistribuidora.Any(pr => pr.IdPrivilegio == p.IdPrivilegio);
                        p.IdPrivilegio = p.Id;
                    });
                }
            }
            return Ok(Privilegios);

        }

        const string RUTA_Privilegio_PUT = "PrivilegioUsuarioDistibuidora/{idUsuarioDistribuidora:int}";
        [Route(RUTA_Privilegio_PUT)]
        [FincappExceptionFilter]
        [HttpPut]
        public IHttpActionResult PutPrivilegioUsuarioDistribuidora([FromUri] int idUsuarioDistribuidora, [FromBody] List<PrivilegioUsuarioDistribuidora> privilegios)
        {
            if (privilegios == null || idUsuarioDistribuidora == 0)
                return BadRequest("Objeto Vacío");

            Negocio.Administracion.PrivilegioUsuarioDistribuidora boPUD = new Negocio.Administracion.PrivilegioUsuarioDistribuidora();

            var eliminar = new PrivilegioUsuarioDistribuidora() { IdUsuarioDistribuidora = idUsuarioDistribuidora, EsBorrado = true };
            boPUD.GuardarPrivilegioUsuarioDistribuidora(new List<PrivilegioUsuarioDistribuidora> { eliminar });

            privilegios.ForEach(f => f.EsNuevo = true);
            boPUD.GuardarPrivilegioUsuarioDistribuidora(privilegios);

            return Ok();
        }


    }
}