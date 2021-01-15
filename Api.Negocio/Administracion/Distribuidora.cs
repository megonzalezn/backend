using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Negocio.Administracion
{
    public class Distribuidora: ObjetoDeNegocio
    {
        public void GuardarDistribuidoras(List<Entidad.Distribuidora> distribuidoras)
        {
            DatosFactory dal = new DatosFactory();
            using (var trx = dal.Datos.InitTransaction())
            {
                try
                {
                    foreach (var distribuidora in distribuidoras)
                    {
                        if (NecesitaGuardar(distribuidora))
                        {
                            switch (ElegirAccionDeGuardar(distribuidora))
                            {
                                case AccionesDeGuardar.Crear:
                                    dal.Datos.InsertarDistribuidora(distribuidora);
                                    dal.Datos.SaveAll();
                                    break;
                                case AccionesDeGuardar.Actualizar:
                                    dal.Datos.ActualizarDistribuidora(distribuidora);
                                    dal.Datos.SaveAll();
                                    break;
                                case AccionesDeGuardar.Borrar:
                                    break;
                            }
                        }
                    }
                    trx.Commit();
                }
                catch
                {
                    trx.Rollback();
                }
            }
        }
        public void ObtenerDistribuidora(Entidad.FiltroDistribuidora filtro, out List<Entidad.Distribuidora> entidades)
        {
            DatosFactory dal = new DatosFactory();
            entidades = dal.Datos.ObtenerDistribuidoraPorFiltro(filtro);
        }
    }
}
