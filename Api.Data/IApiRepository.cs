using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Api.Entidad;

namespace Api.Data
{
    public interface IApiRepository
    {
        #region IDatos Usuario
        DbContextTransaction InitTransaction();
        List<Usuario> ObtenerUsuarioPorFiltro(FiltroUsuario filtro);
        void ActualizarUsuario(Usuario usuario);
        void InsertarUsuario(Usuario usuario);
        #endregion IDatos Usuario
        #region IDatos Distribuidora
        List<Distribuidora> ObtenerDistribuidoraPorFiltro(FiltroDistribuidora filtro);
        void ActualizarDistribuidora(Distribuidora usuario);
        void InsertarDistribuidora(Distribuidora usuario);
        #endregion IDatos Distribuidora
        #region IDatos UsuarioDistribuidora
        List<UsuarioDistribuidora> ObtenerUsuarioDistribuidoraPorFiltro(FiltroUsuarioDistribuidora filtro);
        void InsertarUsuarioDistribuidora(UsuarioDistribuidora usuarioDistribuidora);
        void ActualizarUsuarioDistribuidora(UsuarioDistribuidora usuarioDistribuidora);
        #endregion
        #region IDatos Privilegio
        List<Privilegio> ObtenerPrivilegioPorFiltro(FiltroPrivilegio filtro);
        #endregion
        #region IDatos PrivilegioUsuarioDistribuidora
        //bool ActualizarPrivilegioUsuarioDistribuidora(int IdUsuarioDistribuidora, List<PrivilegioUsuarioDistribuidora> privilegioUsuarioDistribuidora);
        List<PrivilegioUsuarioDistribuidora> ObtenerPrivilegioUsuarioDistribuidoraPorFiltro(FiltroPrivilegioUsuarioDistribuidora filtro);
        void InsertarPrivilegioUsuarioDistribuidora(PrivilegioUsuarioDistribuidora privilegioUsuarioDistribuidora);
        void EliminarPrivilegioUsuarioDistribuidora(int IdUsuarioDistribuidora);
        #endregion        
        #region IDatos LoginUsuario
        bool InsertarLoginUsuario(LoginUsuario loginUsuario);
        bool ActualizarLoginUsuario(LoginUsuario loginUsuario);
        void BuscarLoginUsuario(int idUsuario, out string passwordHash, out string salt);
        LoginUsuario ObtenerLoginUsuarioPorIdUsuario(int idUsuario);
        #endregion              
        bool SaveAll();
    }
}
