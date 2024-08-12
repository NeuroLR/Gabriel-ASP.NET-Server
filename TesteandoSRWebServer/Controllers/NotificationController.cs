using Microsoft.AspNetCore.Mvc;
using TesteandoSRWebServer.Services;

namespace TesteandoSRWebServer.Controllers
{
    [ApiController]
    [Route("api")]
    public class NotificationController : ControllerBase
    {
        [HttpPost("pushNotification")]
        public IActionResult PushNotification()
        {
            try
            {
                Console.WriteLine("se llamo al endpoint pushNotification");
                NotificationManager manager = new(HttpContext);
                manager.SendNotification();
                return Ok("notificacion enviada exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
