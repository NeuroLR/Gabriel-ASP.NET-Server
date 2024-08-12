using TesteandoSRWebServer.Models;

namespace TesteandoSRWebServer.Interfaces
{
    public interface IUserRepository
    {
        Task<List<Usuario>> GetAllUsersAsync();
    }
}
