using System.Web.Http;
using System.Linq;
using Api.Entidad;
using Api.Web.Filters;
using System.Collections.Generic;

namespace Api.Web.Controllers
{
    public partial class AdministracionController
    {
        const string RUTA_Privilegio_GET = "Privilegio/{paramsFiltro?}";
        [Route(RUTA_Privilegio_GET)]
        [HttpGet]
        [FincappExceptionFilter]
        public IHttpActionResult GetPrivilegio([FromUri] string paramsFiltro = null)
        {
            var filtro = ParsearFiltroGenerico<FiltroPrivilegio>(paramsFiltro);

            if (filtro != null)
            {
                List<Entidad.Privilegio> privilegios;
                Negocio.Administracion.Privilegio boPrivilegio = new Negocio.Administracion.Privilegio();

                boPrivilegio.ObtenerPrivilegio(filtro, out privilegios);
                return Ok(privilegios);
            }
            else
                return BadRequest("Filtro Vacío");
        }
    }
}