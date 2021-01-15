using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Negocio.Administracion
{
    public class PrivilegioUsuarioDistribuidora : ObjetoDeNegocio
    {
        public void GuardarPrivilegioUsuarioDistribuidora(List<Entidad.PrivilegioUsuarioDistribuidora> items)
        {
            DatosFactory dal = new DatosFactory();
            using (var trx = dal.Datos.InitTransaction())
            {
                try
                {
                    foreach (var item in items)
                    {
                        if (NecesitaGuardar(item))
                        {
                            switch (ElegirAccionDeGuardar(item))
                            {
                                case AccionesDeGuardar.Crear:
                                    dal.Datos.InsertarPrivilegioUsuarioDistribuidora(item);
                                    break;
                                case AccionesDeGuardar.Actualizar:
                                    //dal.Datos.ActualizarPrivilegioUsuarioDistribuidora(item);
                                    break;
                                case AccionesDeGuardar.Borrar:
                                    dal.Datos.EliminarPrivilegioUsuarioDistribuidora(item.IdUsuarioDistribuidora);
                                    break;
                            }
                        }
                    }
                    dal.Datos.SaveAll();
                    trx.Commit();
                }
                catch
                {
                    trx.Rollback();
                }
            }
        }
        public void ObtenerPrivilegioUsuarioDistribuidora(Entidad.FiltroPrivilegioUsuarioDistribuidora filtro, out List<Entidad.PrivilegioUsuarioDistribuidora> entidades)
        {
            DatosFactory dal = new DatosFactory();
            entidades = dal.Datos.ObtenerPrivilegioUsuarioDistribuidoraPorFiltro(filtro);
        }

    }
}
