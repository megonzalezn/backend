using Api.Entidad;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;


namespace Api.Web.Mail
{
    public static class Smtp
    {

        public static bool SendMail(Usuario usuario, string origen)
        {
            MailMessage mail = new MailMessage();
            try
            {
                string bodyMail = string.Empty;
                switch (origen)
                {
                    case "nuevo":
                        bodyMail = Properties.Resources.mailRegistro;
                        bodyMail = bodyMail.Replace("[PASS]", Util.Cryptojs.DecryptStringAES(usuario.Pin));
                        break;
                    case "masiva":
                        bodyMail = Properties.Resources.mailMasiva;
                        bodyMail = bodyMail.Replace("[PASS]", Util.Cryptojs.DecryptStringAES(usuario.Pin));
                        break;
                    case "info":
                        bodyMail = Properties.Resources.mailInfo;
                        break;
                    case "recuperarPin":
                        bodyMail = Properties.Resources.mailRegistro;
                        bodyMail = bodyMail.Replace("[PASS]", usuario.Pin);
                        break;
                    //case "validacionUsuario":
                    //    bodyMail = Properties.Resources.mailMasiva;
                    //    bodyMail = bodyMail.Replace("[IMAGEN]", usuario.Documento);
                    //    break;
                }
                
                mail.From = new MailAddress(ConfigurationManager.AppSettings["MailPortal"], "FincApp");
                mail.To.Add(usuario.Mail);

                bodyMail = bodyMail.Replace("[USUARIO]", usuario.Nombre);
                bodyMail = bodyMail.Replace("[CORREO]", usuario.Mail);
                bodyMail = bodyMail.Replace("[URL]", ConfigurationManager.AppSettings["UrlPortal"]);

                SmtpClient client = new SmtpClient();
                client.Host = ConfigurationManager.AppSettings["MailHost"];
                client.Port = 25;
                client.UseDefaultCredentials = false;
                client.EnableSsl = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential("fincapp@tepostulo.cl", "P4n4m4.2020");

                mail.Subject = ConfigurationManager.AppSettings["MailSubject"];

                mail.Body = bodyMail;
                mail.IsBodyHtml = true;

                client.Send(mail);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                mail.Dispose();
            }

            return true;
        }
    }
}