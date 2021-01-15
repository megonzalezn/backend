using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Negocio.Administracion
{
    public class UsuarioDistribuidora : ObjetoDeNegocio
    {
        public void GuardarUsuarioDistribuidora(List<Entidad.UsuarioDistribuidora> items)
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
                                    dal.Datos.InsertarUsuarioDistribuidora(item);
                                    break;
                                case AccionesDeGuardar.Actualizar:
                                    dal.Datos.ActualizarUsuarioDistribuidora(item);
                                    break;
                                case AccionesDeGuardar.Borrar:
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
        public void ObtenerUsuarioDistribuidora(Entidad.FiltroUsuarioDistribuidora filtro, out List<Entidad.UsuarioDistribuidora> entidades)
        {
            DatosFactory dal = new DatosFactory();
            entidades = dal.Datos.ObtenerUsuarioDistribuidoraPorFiltro(filtro);
        }

        public void VinculacionUsuarioDistribuidora(Entidad.UsuarioDistribuidora usuarioDistribuidora)
        {
            Negocio.Administracion.Usuario boUsuario = new Negocio.Administracion.Usuario();
            Negocio.Administracion.Distribuidora boDistribuidora = new Negocio.Administracion.Distribuidora();
            Negocio.Administracion.UsuarioDistribuidora boUD = new Negocio.Administracion.UsuarioDistribuidora();

            if (usuarioDistribuidora.Usuario?.Id == 0)
            {
                List<Entidad.Usuario> usuarios;
                boUsuario.ObtenerUsuario(new Entidad.FiltroUsuario() { Cedula = usuarioDistribuidora.Usuario.Cedula }, out usuarios);
                usuarioDistribuidora.IdUsuario = usuarios.FirstOrDefault().Id;
            }

            if (usuarioDistribuidora.Distribuidora?.Id == 0)
            {
                List<Entidad.Distribuidora> distribuidoras;
                boDistribuidora.ObtenerDistribuidora(new Entidad.FiltroDistribuidora() { Identificador = usuarioDistribuidora.Distribuidora.Identificador }, out distribuidoras);
                usuarioDistribuidora.IdDistribuidora = distribuidoras.FirstOrDefault().Id;
            }

            usuarioDistribuidora.FechaCreacion = DateTime.Now;
            usuarioDistribuidora.EsNuevo = true;

            if (usuarioDistribuidora.AceptaUsuario && usuarioDistribuidora.AceptaDistribuidora)
                usuarioDistribuidora.FechaAceptacion = DateTime.Now;

            boUD.GuardarUsuarioDistribuidora(new List<Entidad.UsuarioDistribuidora> { usuarioDistribuidora });



        }
    }
}
