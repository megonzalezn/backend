using Api.Entidad;
using System;
using System.Collections.Generic;

namespace Api.Negocio.Administracion
{
    public class Usuario : ObjetoDeNegocio
    {
        public void GuardarUsuario(List<Entidad.Usuario> items)
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
                                    dal.Datos.InsertarUsuario(item);
                                    dal.Datos.SaveAll();
                                    //Insert de la clave
                                    LoginUsuario lgn = new LoginUsuario();
                                    lgn.IdUsuario = item.Id;
                                    lgn.Salt = Util.CreateSalt(5);
                                    lgn.PasswordHash = Util.CreatePasswordHash(item.Pin, lgn.Salt);
                                    dal.Datos.InsertarLoginUsuario(lgn);
                                    dal.Datos.SaveAll();
                                    break;
                                case AccionesDeGuardar.Actualizar:
                                    dal.Datos.ActualizarUsuario(item);
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

        public void CambiarPassword(Entidad.Usuario usuario)
        {
            DatosFactory dal = new DatosFactory();
            using (var trx = dal.Datos.InitTransaction())
            {
                try
                {
                    LoginUsuario lgn = new LoginUsuario();
                    lgn = dal.Datos.ObtenerLoginUsuarioPorIdUsuario(usuario.Id);
                    lgn.IdUsuario = usuario.Id;
                    lgn.Salt = Util.CreateSalt(5);
                    lgn.PasswordHash = Util.CreatePasswordHash(usuario.Pin, lgn.Salt);
                    dal.Datos.ActualizarLoginUsuario(lgn);
                    dal.Datos.SaveAll();
                    trx.Commit();
                }
                catch
                {
                    trx.Rollback();
                }
            }
        }
        public void ObtenerUsuario(Entidad.FiltroUsuario filtro, out List<Entidad.Usuario> entidades)
        {
            DatosFactory dal = new DatosFactory();
            entidades = dal.Datos.ObtenerUsuarioPorFiltro(filtro);
        }

       
    }
}
