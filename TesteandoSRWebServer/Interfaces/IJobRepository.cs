namespace TesteandoSRWebServer.Interfaces
{
    public interface IJobRepository
    {
        Task<List<string>> GetAllJobsAsync();
    }
}
