using Api.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Api.Web.Controllers
{
    public partial class ServidorController
    {
        const string RUTA_ObtenerFechaHora = "FechaHora";
        /// <summary>
        /// ObtenerFechaHoraGET - Implementa verbo GET para recurso ObtenerFechaHora
        /// </summary>
        //[SwaggerResponse(HttpStatusCode.OK)]
        //[SwaggerResponse(HttpStatusCode.InternalServerError)]
        //[IgnoreCacheOutput]
        [HttpGet]
        [FincappExceptionFilter]
        [Route(RUTA_ObtenerFechaHora)]
        public IHttpActionResult FechaHoraGET()
        {
            return Ok(DateTime.Now);
        }
    }
}