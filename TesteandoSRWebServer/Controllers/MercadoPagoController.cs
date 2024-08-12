using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TesteandoSRWebServer.Models;
using TesteandoSRWebServer.Repositories;
using TesteandoSRWebServer.Services;

namespace TesteandoSRWebServer.Controllers
{
    [ApiController]
    [Route("api/mercadopago")]
    public class MercadoPagoController : ControllerBase
    {
        [HttpPost("cliente")]
        public async Task<IActionResult> CreateCliente()
        {
            try
            {
                using StreamReader reader = new(Request.Body);
                string bodyToString = await reader.ReadToEndAsync();
                var body = JsonConvert.DeserializeObject<Dictionary<string, object>>(bodyToString);
                string email = body?["email"]?.ToString();
                string userId = body?["userId"]?.ToString();
                if (email == null || userId == null) return BadRequest("Invalid data");

                var mp = new ClienteMercadoPago(email, userId);
                await mp.Crear();
                return Ok("OK!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Salto el catch!");
                Console.WriteLine(ex.Message);
                return StatusCode(500, "error!");
            }
        }

        [HttpGet("cliente")]
        public IActionResult GetCliente()
        {
            try
            {
                string email = Request.Query["email"];
                if (email == null) throw new ArgumentNullException(nameof(email), "el email es nulo");

                var mp = new ClienteMercadoPago(email, null);
                var result = mp.Search();
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Salto el catch!");
                Console.WriteLine(ex.Message);
                return StatusCode(500, "error!");
            }
        }

        [HttpPost("suscripcion")]
        [EnableCors("test")]
        public async Task<IActionResult> CreateSuscripcion()
        {
            try
            {
                using StreamReader reader = new(Request.Body);
                string bodyToString = await reader.ReadToEndAsync();
                var body = JsonConvert.DeserializeObject<Dictionary<string, object>>(bodyToString);
                string email = body?["email"]?.ToString();
                string token = body?["token"]?.ToString();
                string userId = body?["userId"]?.ToString();

                if (email == null || token == null || userId == null)
                {
                    throw new ArgumentNullException(null, "Hay datos nulos");
                }

                SubsMercadoPago manager = new SubsMercadoPago();
                var response = await manager.CrearSub(token, email);
                if (response != null)
                {
                    var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
                    var result = await new FirestoreExpertRepository(FirestoreDb.Create(Utils.FirestoreId)).SaveSubIdAsync(userId, json["id"].ToString());
                    if (!result)
                    {
                        Console.WriteLine("Ocurrio un error guardando el subId");
                    }
                    return Ok("OK");
                }

                return BadRequest("Failed to create subscription");
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Salto el catch!");
                Console.WriteLine(ex.StackTrace);
                return StatusCode(500, "error!");
            }
        }
    }
}
