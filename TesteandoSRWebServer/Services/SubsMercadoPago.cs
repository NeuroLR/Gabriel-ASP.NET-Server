using Newtonsoft.Json;
using System.Net.Http.Headers;
using TesteandoSRWebServer.Models;

namespace TesteandoSRWebServer.Services
{
    public class SubsMercadoPago
    {
        static readonly HttpClient httpClient = new HttpClient();

        public async Task<string>? CrearSub(string token, string email)
        {
            SuscripcionModel model = CreateModel(token, email);
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            Uri uri = new("https://api.mercadopago.com/preapproval");
            HttpRequestMessage request = CreateRequest(uri, content);

            using var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return null;
            }
        }

        private SuscripcionModel CreateModel(string token, string email)
        {
            return new SuscripcionModel
            {
                card_token_id = token,
                payer_email = email,
                back_url = "https://www.solucionesrapidas.com.ar/api/autorizacion",
                reason = "Soluciones Rapidas",
                status = "authorized",
                preapproval_plan_id = "2c9380848fde7fc1018fea11bea203f1"
            };
        }

        private HttpRequestMessage CreateRequest(Uri uri, StringContent content)
        {
            return new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = uri,
                Headers =
                {
                    { "Accept", "application/json" },
                    { "Authorization", "Bearer " + Utils.MercadoPagoToken }
                },
                Content = content
            };
        }
    }
}
