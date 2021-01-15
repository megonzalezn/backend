using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Negocio
{
    public static class Util
    {
        #region CreateSalt
        public static string CreateSalt(int size)
        {
            // Generate a cryptographic random number using the cryptographic
            // service provider

            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }
        #endregion
        #region Método CreatePasswordHash(string pwd, string salt)
        public static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = string.Concat(pwd, salt);
            string hashedPwd =
                System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(
                saltAndPwd, "SHA1");
            return hashedPwd;
        }
        #endregion
    }
}
