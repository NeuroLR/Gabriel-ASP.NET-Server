using MercadoPago.Resource.Customer;
using MercadoPago.Resource;

namespace TesteandoSRWebServer.Interfaces
{
    public interface IClienteMP
    {
        Task Crear();
        Task Obtener(string id);
        ResultsResourcesPage<Customer> Search();
    }
}
