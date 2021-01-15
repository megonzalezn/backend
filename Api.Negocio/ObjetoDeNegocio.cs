using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Negocio
{
    public class ObjetoDeNegocio: IDisposable
    {
        #region Metodo NecesitaGuardar()
        public static bool NecesitaGuardar(Entidad.BasePersistente entidad)
        {
            if (entidad.EsNuevo && entidad.EsBorrado)
                return false;
            if (!entidad.EsNuevo && !entidad.EsBorrado && !entidad.EsModificado)
                return false;
            return true;
        }
        #endregion

        #region Metodo ElegirAccionDeGuardar()
        public static AccionesDeGuardar ElegirAccionDeGuardar(Entidad.BasePersistente entidad)
        {
            if (!NecesitaGuardar(entidad))
                return AccionesDeGuardar.Nada;
            if (entidad.EsNuevo)
                return AccionesDeGuardar.Crear;
            if (entidad.EsBorrado)
                return AccionesDeGuardar.Borrar;
            return AccionesDeGuardar.Actualizar;
        }
        #endregion

        private readonly IDisposable _dispose;
        public void Dispose()
        {
            _dispose?.Dispose();
        }
    }

    #region Enum AccionesDeGuardar
    public enum AccionesDeGuardar
    {
        Nada,
        Crear,
        Actualizar,
        Borrar
    }
    #endregion
}
