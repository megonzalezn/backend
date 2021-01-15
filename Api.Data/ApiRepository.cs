using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Api.Entidad;

namespace Api.Data
{
    public class ApiRepository : IApiRepository
    {
        private ApiContext _ctx;
        public ApiRepository(ApiContext ctx)
        {
            _ctx = ctx;
        }
        public DbContextTransaction InitTransaction()
        {
            return _ctx.Database.BeginTransaction();
        }

        #region Usuario
        public List<Usuario> ObtenerUsuarioPorFiltro(FiltroUsuario filtro)
        {
            var tieneIds = filtro.Ids.Length > 0;
            using (var db = new ApiContext())
            {
                //string includeProperties = "lsl";
                //var l = db.MyQueryWithDynamicInclude<Usuario>().Include(includeProperties);
                //var la = db.MyQuery<Usuario>().Include("Department");
                //.Where(u => (filtro.Id == 0 || u.Id == filtro.Id)
                //              && (string.IsNullOrEmpty(filtro.Telefono) || u.Telefono == filtro.Telefono)
                //              && (string.IsNullOrEmpty(filtro.Cedula) || u.Cedula == filtro.Cedula)
                //              && (string.IsNullOrEmpty(filtro.Nombre) || u.Cedula == filtro.Nombre)
                //              && (string.IsNullOrEmpty(filtro.Mail) || u.Mail == filtro.Mail))
                //              .ToList();

                var query = db.Usuario.Where(u => (filtro.Id == 0 || u.Id == filtro.Id)
                                                    && (string.IsNullOrEmpty(filtro.Telefono) || u.Telefono.Equals(filtro.Telefono))
                                                    && (string.IsNullOrEmpty(filtro.Cedula) || u.Cedula.Equals(filtro.Cedula))
                                                    && (string.IsNullOrEmpty(filtro.Nombre) || u.Nombre.Equals(filtro.Nombre))
                                                    && (string.IsNullOrEmpty(filtro.Apellido) || u.Apellido.Equals(filtro.Apellido))
                                                    && (string.IsNullOrEmpty(filtro.Mail) || u.Mail.Equals(filtro.Mail))
                                                    && (!tieneIds || filtro.Ids.Any(f => f == u.Id))
                                                    && (filtro.FechaRegistro == null || u.FechaRegistro == filtro.FechaRegistro)
                                                    && (filtro.EsVerificado == null || u.EsVerificado == filtro.EsVerificado)
                                                    && (filtro.Eliminado == null || u.Eliminado == filtro.Eliminado)
                                                    );
                if (filtro.Includes != null)
                {
                    foreach (string include in filtro.Includes)
                        query = query.Include(include);
                }

                return query.ToList();
                //(from usu in _ctx.Usuario
                //              where (filtro.Id == 0 || usu.Id == filtro.Id)
                //              && (string.IsNullOrEmpty(filtro.Telefono) || usu.Telefono == filtro.Telefono)
                //              && (string.IsNullOrEmpty(filtro.Cedula) || usu.Cedula == filtro.Cedula)
                //              && (string.IsNullOrEmpty(filtro.Nombre) || usu.Cedula == filtro.Nombre)
                //              && (string.IsNullOrEmpty(filtro.Mail) || usu.Mail == filtro.Mail)
                //              select usu).ToList();
            }
        }
        public void InsertarUsuario(Usuario usuario)
        {
            try
            {
                _ctx.Usuario.Add(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarUsuario(Usuario usuario)
        {
            try
            {
                int idUsuario = usuario.Id;
                var _usuarioOriginal = _ctx.Usuario.Where(u => u.Id == idUsuario).ToList().FirstOrDefault();
                usuario.Pin = _usuarioOriginal.Pin;
                usuario.FechaRegistro = _usuarioOriginal.FechaRegistro;

                _ctx.Entry(_usuarioOriginal).CurrentValues.SetValues(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ElminarUsuario(int id)
        {
            try
            {
                var entity = _ctx.Usuario.Find(id);
                if (entity != null)
                {
                    _ctx.Usuario.Remove(entity);
                    return true;
                }
            }
            catch
            {
                //TODO logging
            }
            return false;
        }
        #endregion
        #region Distribuidora
        public List<Distribuidora> ObtenerDistribuidoraPorFiltro(FiltroDistribuidora filtro)
        {
            var tieneIds = filtro.Ids.Length > 0;
            using (var db = new ApiContext())
            {
                var query = db.Distribuidora.Where(d => (!tieneIds || filtro.Ids.Any(f => f == d.Id))
                                                      && (filtro.Id == 0 || d.Id == filtro.Id)
                                                      && (string.IsNullOrEmpty(filtro.Identificador) || d.Identificador.Equals(filtro.Identificador))
                                                      && (string.IsNullOrEmpty(filtro.RazonSocial) || d.RazonSocial.Equals(filtro.RazonSocial))
                                                      && (string.IsNullOrEmpty(filtro.Giro) || d.Giro.Equals(filtro.Giro))
                                                      && (string.IsNullOrEmpty(filtro.Direccion) || d.Direccion.Contains(filtro.Direccion))
                                                      && (string.IsNullOrEmpty(filtro.Mail) || d.Mail.Equals(filtro.Mail))
                                                      && (filtro.Eliminado == null || d.Eliminado == filtro.Eliminado)
                                                      && (filtro.EsVerificado == null || d.EsVerificado == filtro.EsVerificado)
                                                      && (filtro.FechaRegistro == null || d.FechaRegistro == filtro.FechaRegistro));

                if (filtro.Includes != null)
                {
                    foreach (string include in filtro.Includes)
                        query = query.Include(include);
                }

                return query.ToList();
            }

        }
        public void InsertarDistribuidora(Distribuidora distribuidora)
        {
            try
            {
                _ctx.Distribuidora.Add(distribuidora);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarDistribuidora(Distribuidora distribuidora)
        {
            try
            {
                int idDistribuidora = distribuidora.Id;
                var _distribuidoraOriginal = _ctx.Distribuidora.Where(u => u.Id == idDistribuidora).ToList().FirstOrDefault();
                distribuidora.Identificador = _distribuidoraOriginal.Identificador;

                _ctx.Entry(_distribuidoraOriginal).CurrentValues.SetValues(distribuidora);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ElminarDistribuidora(int id)
        {
            try
            {
                var entity = _ctx.Distribuidora.Find(id);
                if (entity != null)
                {
                    _ctx.Distribuidora.Remove(entity);
                    return true;
                }
            }
            catch
            {
                //TODO logging
            }
            return false;
        }
        #endregion
        #region UsuarioDistribuidora
        public List<UsuarioDistribuidora> ObtenerUsuarioDistribuidoraPorFiltro(FiltroUsuarioDistribuidora filtro)
        {
            var tieneIds = filtro.Ids.Length > 0;
            var tieneIdsUsuario = filtro.IdsUsuario.Length > 0;
            var tieneIdsDistribuidora = filtro.IdsDistribuidora.Length > 0;
            using (var db = new ApiContext())
            {
                var query = db.UsuarioDistribuidora.Where(ud => (filtro.Id == 0 || ud.Id == filtro.Id)
                                                             && (filtro.IdUsuario == 0 || ud.IdUsuario == filtro.IdUsuario)
                                                             && (filtro.IdDistribuidora == 0 || ud.IdDistribuidora == filtro.IdDistribuidora)
                                                             && (filtro.AceptaDistribuidora == null || ud.AceptaDistribuidora == filtro.AceptaDistribuidora)
                                                             && (filtro.AceptaUsuario == null || ud.AceptaUsuario == filtro.AceptaUsuario)
                                                             && (filtro.Eliminado == null || ud.Eliminado == filtro.Eliminado)
                                                             && (!tieneIds || filtro.Ids.Any(f => f == ud.Id))
                                                             && (!tieneIdsUsuario || filtro.IdsUsuario.Any(f => f == ud.IdUsuario))
                                                             && (!tieneIdsDistribuidora || filtro.IdsDistribuidora.Any(f => f == ud.IdDistribuidora))
                                                             && (filtro.FechaCreacion == null || ud.FechaCreacion == filtro.FechaCreacion)
                                                             && (string.IsNullOrEmpty(filtro.Alias) || ud.Alias.Equals(filtro.Alias))
                                                             && (filtro.EsAdmin == null || ud.EsAdmin == filtro.EsAdmin));

                if (filtro.Includes != null)
                {
                    foreach (string include in filtro.Includes)
                        query = query.Include(include);
                }

                return query.ToList();
            }
        }
        public void InsertarUsuarioDistribuidora(UsuarioDistribuidora usuarioDistribuidora)
        {
            try
            {
                _ctx.UsuarioDistribuidora.Add(usuarioDistribuidora);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarUsuarioDistribuidora(UsuarioDistribuidora usuarioDistribuidora)
        {
            try
            {
                int idUsuarioDistribuidora = usuarioDistribuidora.Id;
                var _usuarioDistribuidoraOriginal = _ctx.UsuarioDistribuidora.Where(u => u.Id == idUsuarioDistribuidora).ToList().FirstOrDefault();
                _ctx.Entry(_usuarioDistribuidoraOriginal).CurrentValues.SetValues(usuarioDistribuidora);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Privilegio
        public List<Privilegio> ObtenerPrivilegioPorFiltro(FiltroPrivilegio filtro)
        {
            var tieneIds = filtro.Ids.Length > 0;
            using (var db = new ApiContext())
            {
                return db.Privilegio.Where(p => (filtro.Id == 0 || p.Id == filtro.Id)
                                           && (filtro.Eliminado == null || p.Eliminado == filtro.Eliminado)
                                           && (string.IsNullOrEmpty(filtro.Nombre) || p.Nombre.Contains(filtro.Nombre))
                                           && (string.IsNullOrEmpty(filtro.Descripcion) || p.Descripcion.Contains(filtro.Descripcion))
                                           && (!tieneIds || filtro.Ids.Any(f => f == p.Id))).ToList();
            }
        }
        #endregion
        #region PrivilegioUsuarioDistribuidora
        public void InsertarPrivilegioUsuarioDistribuidora(PrivilegioUsuarioDistribuidora privilegioUsuarioDistribuidora)
        {
            try
            {
                _ctx.PrivilegioUsuarioDistribuidora.Add(privilegioUsuarioDistribuidora);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarPrivilegioUsuarioDistribuidora(int IdUsuarioDistribuidora)
        {
            try
            {
                _ctx.PrivilegioUsuarioDistribuidora.RemoveRange(_ctx.PrivilegioUsuarioDistribuidora.Where(p => p.IdUsuarioDistribuidora == IdUsuarioDistribuidora));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PrivilegioUsuarioDistribuidora> ObtenerPrivilegioUsuarioDistribuidoraPorFiltro(FiltroPrivilegioUsuarioDistribuidora filtro)
        {
            using (var db = new ApiContext())
            {
                var query = db.PrivilegioUsuarioDistribuidora.Where(pud => (filtro.IdPrivilegio == 0 || pud.IdPrivilegio == filtro.IdPrivilegio)
                                                                       && (filtro.IdUsuarioDistribuidora == 0 || pud.IdUsuarioDistribuidora == filtro.IdUsuarioDistribuidora));

                return query.ToList();
            }
        }
        #endregion
        #region LoginUsuario
        public bool InsertarLoginUsuario(LoginUsuario loginUsuario)
        {
            try
            {
                _ctx.LoginUsuario.Add(loginUsuario);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public LoginUsuario ObtenerLoginUsuarioPorIdUsuario(int idUsuario)
        {
            using (var db = new ApiContext())
            {
                return db.LoginUsuario.Where(l => l.IdUsuario == idUsuario).FirstOrDefault();
            }
        }
        public void BuscarLoginUsuario(int idUsuario, out string passwordHash, out string salt)
        {
            using (var db = new ApiContext())
            {
                var loginEncontrado = db.LoginUsuario.Where(l => l.IdUsuario == idUsuario).FirstOrDefault();
                if (loginEncontrado is null)
                {
                    passwordHash = null;
                    salt = null;
                    return;
                }

                passwordHash = loginEncontrado.PasswordHash;
                salt = loginEncontrado.Salt;
            }
        }
        public bool ActualizarLoginUsuario(LoginUsuario loginUsuario)
        {
            int idUsuario = loginUsuario.IdUsuario;
            var _usuarioOriginal = _ctx.LoginUsuario.Where(u => u.IdUsuario == idUsuario).ToList().FirstOrDefault();
            _ctx.Entry(_usuarioOriginal).CurrentValues.SetValues(loginUsuario);
            return true;
        }
        #endregion                                              
        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
