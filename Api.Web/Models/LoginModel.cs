using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Web.Models
{
    public class LoginModel
    {
        public string NombreUsuario { get; set; }
        public string Pass { get; set; }
        public string NewPass { get; set; }
        public string PushToken { get; set; }
    }

    public class TokenModel
    {
        public string ApiToken { get; set; }
        public string RefreshToken { get; set; }
        public string PushToken { get; set; }
    }
}