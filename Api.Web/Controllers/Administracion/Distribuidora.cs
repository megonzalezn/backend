using System;
using System.Web.Http;
using System.Linq;
using Api.Web.Filters;
using Api.Entidad;
using System.Collections.Generic;

namespace Api.Web.Controllers
{

    public partial class AdministracionController
    {
        const string RUTA_Distribuidora_POST = "Distribuidora";
        [Route(RUTA_Distribuidora_POST)]
        [FincappExceptionFilter]
        [HttpPost]
        public IHttpActionResult PostDistribuidora([FromBody] Distribuidora distribuidora)
        {
            if (distribuidora == null || string.IsNullOrEmpty(distribuidora.Identificador) || distribuidora.UsuarioDistribuidora == null)
                return BadRequest("Objeto distribuidora vacio");
            List<Distribuidora> listaDistribuidoraRetornar;
            Negocio.Administracion.Distribuidora boDistribuidora = new Negocio.Administracion.Distribuidora();
            boDistribuidora.ObtenerDistribuidora(new FiltroDistribuidora() { Identificador = distribuidora.Identificador }, out listaDistribuidoraRetornar);

            if (listaDistribuidoraRetornar?.Count == 0)
            {
                distribuidora.FechaRegistro = DateTime.Now;                
                distribuidora.EsNuevo = true;
                if (distribuidora.UsuarioDistribuidora?.Count > 0)
                {
                    distribuidora.UsuarioDistribuidora.ToList().ForEach(f =>
                    {
                        f.FechaCreacion = DateTime.Now;
                        f.FechaAceptacion = DateTime.Now;
                        f.AceptaDistribuidora = true;
                        f.AceptaUsuario = true;
                        f.EsAdmin = true;
                    });
                }

                if (distribuidora.Documentos?.Count > 0)
                    distribuidora.Documentos.ToList().ForEach(f => { f.FechaVigencia = DateTime.Now; });

                boDistribuidora.GuardarDistribuidoras(new List<Distribuidora> { distribuidora });

                //Estructura de la transacción
                //using (var trx = Datos.InitTransaction())
                //{
                //    try
                //    {

                //        Datos.InsertarDistribuidora(distribuidora);
                //        Datos.SaveAll();
                //        Datos.InsertarUsuario(new Usuario() { Cedula = "17-000-000" });

                //        trx.Commit();
                //    }catch
                //    {
                //        trx.Rollback();
                //    }
                //}                    
                //Datos.SaveAll();
                return Ok(distribuidora);
            }
            else
                return BadRequest("Distribuidora existe");
        }
    }
}