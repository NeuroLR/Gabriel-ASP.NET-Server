using FirebaseAdmin.Messaging;
using Newtonsoft.Json;
using TesteandoSRWebServer.Models;

namespace TesteandoSRWebServer.Services
{

    public class NotificationManager
    {
        private NotificationBody? body;

        private readonly Stream reqBody;

        /// <summary>
        /// Clase para manejar las notificaciones de firebase messaging.
        /// La dependencia de firebase messaging deberia estar incluida en el admin sdk de firebase.
        /// </summary>
        /// <param name="context">HttpContext del endpoint desde el cual se crea la instancia de la clase</param>
        public NotificationManager(HttpContext context)
        {
            reqBody = context.Request.Body;

            ParseBody();

            if (body == null)
            {
                Console.WriteLine("El body es nulo");
            }
        }
        /// <summary>
        /// Metodo para enviar la notificacion
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public async void SendNotification()
        {
            try
            {
                if (body == null)
                {
                    throw new ArgumentNullException(nameof(body), "el body no puede ser nulo");
                }
                Message notification = CreateMessage(body);
                await FirebaseMessaging.DefaultInstance.SendAsync(notification);
            }
            catch (Exception e)
            {
                Console.WriteLine("ocurrio un error enviando la notification " + e.Message);
                Console.WriteLine("ocurrio un error enviando la notification " + e.StackTrace);
            }
        }
        /// <summary>
        /// Metodo para crear una instancia de la clase Message, que se envia como parametro para crear la notificacion
        /// </summary>
        /// <param name="msgBody">el body de la solicitud http parseada a la clase NotificationBody</param>
        /// <returns>La instancia de la clase Messaging lista para enviar</returns>
        /// <exception cref="ArgumentNullException"></exception>
        private Message CreateMessage(NotificationBody msgBody)
        {
            string titulo = msgBody.Data.GetValueOrDefault("title") ?? throw new ArgumentNullException();
            string message = msgBody.Data.GetValueOrDefault("message") ?? throw new ArgumentNullException();

            string intentFilter;
            switch (msgBody.Activity)
            {
                case "client":
                    {
                        intentFilter = "proyecto.necessito.activity_client_menu.notification";
                        break;
                    }
                case "worker":
                    {
                        intentFilter = "proyecto.necessito.activity_worker_menu.notification";
                        break;
                    }
                case "message":
                    {
                        intentFilter = "proyecto.necessito.activity_mensajes_splash_screen.notification";
                        break;
                    }
                default:
                    {
                        intentFilter = "proyecto.necessito.activity_main.notification";
                        break;
                    }

            }

            string esProfesional = (!msgBody.Profesional).ToString() ?? "null";
            string receiverId = msgBody.ReceiverId ?? "null";
            string docId = msgBody.DocId ?? "null";
            string userId = msgBody.UserId ?? "null";

            return new Message()
            {
                Token = msgBody.FcmToken,
                Notification = new Notification()
                {
                    Title = titulo,
                    Body = message
                },
                Android = new AndroidConfig()
                {
                    Priority = Priority.High,
                    Notification = new AndroidNotification()
                    {
                        ClickAction = intentFilter
                    }
                },
                Data = new Dictionary<string, string>()
                {
                    { "profesional", esProfesional },
                    { "receiverId", receiverId },
                    { "docId", docId },
                    { "userId",  userId }
                }
            };
        }
        private async void ParseBody()
        {
            StreamReader reader = new(reqBody);
            string bodyToString = await reader.ReadToEndAsync();
            body = JsonConvert.DeserializeObject<NotificationBody>(bodyToString);
        }
    }
}
