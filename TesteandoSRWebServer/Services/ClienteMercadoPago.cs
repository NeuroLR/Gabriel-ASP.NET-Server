using Google.Cloud.Firestore;
using MercadoPago.Client;
using MercadoPago.Client.Customer;
using MercadoPago.Config;
using MercadoPago.Resource;
using MercadoPago.Resource.Customer;
using TesteandoSRWebServer.Interfaces;
using TesteandoSRWebServer.Models;
using TesteandoSRWebServer.Repositories;

namespace TesteandoSRWebServer.Services
{
    public class ClienteMercadoPago : IClienteMP
    {
        private readonly string? email;
        private readonly string? userId;

        public Customer? Customer { get; private set; }

        public ResultsResourcesPage<Customer>? SearchResults { get; private set; }

        public bool CreateClientResult { get; private set; }

        public ClienteMercadoPago(string? email, string? userId)
        {
            Console.WriteLine("email = " + email);
            this.email = email;
            this.userId = userId;
            MercadoPagoConfig.AccessToken = Utils.MercadoPagoToken;
        }

        public async Task Crear()
        {
            try
            {
                if (email == null || email == "")
                {
                    throw new Exception("el email es nulo");
                }
                CustomerClient cliente = new();
                var newClient = await cliente.CreateAsync(new CustomerRequest
                {
                    Email = email
                }) ?? throw new ArgumentNullException("no se pudo crear el cliente correctamente");
                Console.WriteLine("exito!, cliente = " + cliente.ToString());
                SaveInFirebase(newClient.Id);
                CreateClientResult = true;
            }
            catch (Exception ex)
            {
                CreateClientResult = false;
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task Obtener(string id)
        {
            try
            {
                CustomerClient cliente = new();
                var tempClient = await cliente.GetAsync(id);
                Console.WriteLine($"email = {tempClient.Email}, id = {tempClient.Id}");
                Customer = tempClient;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public ResultsResourcesPage<Customer> Search()
        {
            if (email == null)
            {
                throw new ArgumentNullException(email, "el email es nulo");
            }
            CustomerClient client = new();
            var options = new SearchRequest
            {
                Offset = 0,
                Limit = 10,
                Filters = new Dictionary<string, object>
            {
                { "email", email }
            }
            };
            return client.Search(options);
        }

        private async void SaveInFirebase(string mpId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(userId, "userId es nulo");
            }
            FirestoreExpertRepository db = new(FirestoreDb.Create(Utils.FirestoreId));
            await db.SaveMpIdAsync(mpId, userId);
        }
    }
}
