using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.IO;
using System.Net;
using System.Resources;
using System.Text;
using System.ComponentModel;
using Api.Entidad;

namespace Api.Firebase.Integrador
{
    public class PushNotification
    {
        public void ProcesoPush(Entidad.UsuarioDistribuidora entidad)
        {
            Message message;
            List<Entidad.UsuarioDistribuidora> duenioDistribuidora;
            List<Entidad.Usuario> trabajador;
            Negocio.Administracion.UsuarioDistribuidora boUsuarioDistribuidora = new Negocio.Administracion.UsuarioDistribuidora();
            Negocio.Administracion.Usuario boUsuario = new Negocio.Administracion.Usuario();
            string para, titulo, mensaje = string.Empty;

            switch (entidad.Proceso)
            {
                case Entidad.Enums.ProcesoUsuarioDistribuidora.TrabajadorLeeQR:
                    //-Admin de la distribuidora(Notificacion: Title: Configurar Privilegios, Body: Debe asignar privilegios a "el trabajador")
                    //                          (CargaUtil * *Pendiente * * { Nombre Pagina, IdUsuarioDistribuidora}) 

                    //**Busco al dueño de la distribuidora
                    duenioDistribuidora = new List<Entidad.UsuarioDistribuidora>();
                    boUsuarioDistribuidora.ObtenerUsuarioDistribuidora(new Entidad.FiltroUsuarioDistribuidora() { EsAdmin = true, IdDistribuidora = entidad.IdDistribuidora, Includes = new string[] { "Usuario" } }, out duenioDistribuidora);

                    //**Busco al trabajador
                    trabajador = new List<Entidad.Usuario>();
                    boUsuario.ObtenerUsuario(new Entidad.FiltroUsuario() { Id = entidad.IdUsuario }, out trabajador);

                    titulo = "Configurar Privilegios";
                    mensaje = $"Debe asignar privilegios a {trabajador.FirstOrDefault().Nombre} {trabajador.FirstOrDefault().Apellido}";

                    message = new Message()
                    {
                        Token = duenioDistribuidora.FirstOrDefault().Usuario.PushToken,
                        Notification = new Notification()
                        {
                            Title = titulo,
                            Body = mensaje
                        }
                    };

                    System.Threading.Tasks.Task.Run(() => SendPushAsync(message));

                    break;
                case Entidad.Enums.ProcesoUsuarioDistribuidora.TrabajadorEnviaSolicitud:
                    //-Admin de la distribuidora(Notificacion: Title: Solicitud de Vinculación, Body: tiene una nueva solicitud)
                    //                               (CargaUtil * *Pendiente * * { Nombre Pagina, IdUsuarioDistribuidora})
                    //**Busco al dueño de la distribuidora
                    duenioDistribuidora = new List<Entidad.UsuarioDistribuidora>();
                    boUsuarioDistribuidora.ObtenerUsuarioDistribuidora(new Entidad.FiltroUsuarioDistribuidora() { EsAdmin = true, IdDistribuidora = entidad.IdDistribuidora, Includes = new string[] { "Usuario" } }, out duenioDistribuidora);

                    titulo = "Solicitud de Vinculación";
                    mensaje = "tiene una nueva solicitud";

                    message = new Message()
                    {
                        Token = duenioDistribuidora.FirstOrDefault().Usuario.PushToken,
                        Notification = new Notification()
                        {
                            Title = titulo,
                            Body = mensaje
                        }
                    };

                    System.Threading.Tasks.Task.Run(() => SendPushAsync(message));

                    break;
                case Entidad.Enums.ProcesoUsuarioDistribuidora.DistribuidoraLeeQR:
                    //-Trabajador(Notificacion: Title: Vinculación relizada, Body: Se realizo una vinculacion a "Nombre  distribuidora", toca acá para actualizar la información.)
                    //                (CargaUtil { Nombre Pagina}) 

                    //**Busco al trabajador
                    trabajador = new List<Entidad.Usuario>();
                    boUsuario.ObtenerUsuario(new Entidad.FiltroUsuario() { Id = entidad.IdUsuario }, out trabajador);

                    titulo = "Vinculación relizada";
                    mensaje = $"Se realizo una vinculacion a {entidad.Distribuidora.RazonSocial}, toca acá para actualizar la información";

                    message = new Message()
                    {
                        Token = trabajador.FirstOrDefault().PushToken,
                        Notification = new Notification()
                        {
                            Title = titulo,
                            Body = mensaje
                        }
                    };

                    System.Threading.Tasks.Task.Run(() => SendPushAsync(message));
                    break;
                case Entidad.Enums.ProcesoUsuarioDistribuidora.DistribuidoraEnviaSolicitud:
                    //-Trabajador(Notificacion: Title: Solicitud de Vinculación Body: La "Nombre  distribuidora" solicito tu vinculación.)
                    //                (CargaUtil { Nombre Pagina, IdUsuarioDistribuidora})

                    //**Busco al trabajador
                    trabajador = new List<Entidad.Usuario>();
                    boUsuario.ObtenerUsuario(new Entidad.FiltroUsuario() { Id = entidad.IdUsuario }, out trabajador);

                    titulo = "Solicitud de Vinculación";
                    mensaje = $"La {entidad.Distribuidora.RazonSocial} solicito tu vinculación";

                    message = new Message()
                    {
                        Token = trabajador.FirstOrDefault().PushToken,
                        Notification = new Notification()
                        {
                            Title = titulo,
                            Body = mensaje
                        }
                    };

                    System.Threading.Tasks.Task.Run(() => SendPushAsync(message));
                    break;
            }

        }
        public void ProcesoPush(Entidad.PrivilegioUsuarioDistribuidora entidad)
        {
            Message message;
            List<Entidad.Usuario> trabajador;
            string para, titulo, mensaje = string.Empty;
            Negocio.Administracion.UsuarioDistribuidora boUA = new Negocio.Administracion.UsuarioDistribuidora();

            switch (entidad.Proceso)
            {
                case Entidad.Enums.ProcesoPrivilegioUsuarioDistribuidora.AsignaActualiza:
                    //-Trabajador(Notificacion: Title: Privilegios  Body: Se han Actualizado tus privilegios.)
                    //              (CargaUtil { Nombre Pagina})  

                    //**Busco al trabajador
                    var usuarioDistribuidora = new List<UsuarioDistribuidora>();
                    boUA.ObtenerUsuarioDistribuidora(new Entidad.FiltroUsuarioDistribuidora() { Id = entidad.IdUsuarioDistribuidora, Includes = new string[] { "Usuario" } }, out usuarioDistribuidora);

                    titulo = "Privilegios";
                    mensaje = $"Se han Actualizado tus privilegios";

                    message = new Message()
                    {
                        Token = usuarioDistribuidora.FirstOrDefault().Usuario.PushToken,
                        Notification = new Notification()
                        {
                            Title = titulo,
                            Body = mensaje
                        },
                        Data = new Dictionary<string, string>()
                        {
                            { "Accion", "CambioDistribuidora" },
                            { "idDistribuidora", "57" }
                        }
                    };

                    System.Threading.Tasks.Task.Run(() => SendPushAsync(message));
                    break;
            }
        }
        public async void SendPushAsync(Message message)
        {
            try
            {
                if (FirebaseApp.DefaultInstance == null)
                    FirebaseApp.Create(new AppOptions()
                    {
                        Credential = GoogleCredential.FromStream(new MemoryStream(Properties.Resources.GoogleCredentials))
                    });

                await FirebaseMessaging.DefaultInstance.SendAsync(message);
            }
            catch (FirebaseMessagingException fme)
            {
                throw fme;
                fme.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
                ex.ToString();
            }

        }
    }
}
