using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Api.Web.Controllers
{
    #region AdministracionController
    ///AdministracionController - controlador de servicios para ámbito Administracion.
    //[AutoInvalidateCacheOutput]
    [RoutePrefix("api/Administracion")]
    [Authorize]
    public partial class AdministracionController : BaseApiController
    {
        #region Constructor
        /// Constructor
        public AdministracionController()
        {
        }
        #endregion
    }
    #endregion
    #region ServidorController
    /// ServidorController - controlador de servicios para ámbito Servidor.
    //[AutoInvalidateCacheOutput]
    [RoutePrefix("api/Servidor")]
    [Authorize]
    public partial class ServidorController : BaseApiController
    {
        #region Constructor
        /// Constructor
        public ServidorController()
        {

        }
        #endregion
    }
    #endregion
    #region LoginController
    [RoutePrefix("api/Login")]
    [Authorize]
    public partial class LoginController : BaseApiController
    {
        #region Constructor
        /// Constructor
        public LoginController()
        {

        }
        #endregion
    }
    #endregion
    #region GlobalController
    ///GlobalController - controlador de servicios para ámbito Global.
    //[AutoInvalidateCacheOutput]
    [RoutePrefix("api/Global")]
    [Authorize]
    public partial class GlobalController : BaseApiController
    {
        #region Constructor
        /// Constructor
        public GlobalController()
        {
        }
        #endregion
    }
    #endregion
    #region AbastecimientoController
    [RoutePrefix("api/abastecimiento")]
    [Authorize]
    public partial class AbastecimientoController: BaseApiController
    {
        #region Constructor
        /// Constructor
        public AbastecimientoController()
        {

        }
        #endregion
    }
    #endregion
}