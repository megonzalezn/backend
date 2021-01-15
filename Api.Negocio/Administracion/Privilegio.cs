using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Negocio.Administracion
{
    public class Privilegio : ObjetoDeNegocio
    {
        public void ObtenerPrivilegio(Entidad.FiltroPrivilegio filtro, out List<Entidad.Privilegio> entidades)
        {
            DatosFactory dal = new DatosFactory();
            entidades = dal.Datos.ObtenerPrivilegioPorFiltro(filtro);
        }
    }
}
