namespace TesteandoSRWebServer.Interfaces
{
    public interface IExpertRepository
    {
        Task SaveMpIdAsync(string mpId, string userId);
        Task<bool> SaveSubIdAsync(string? userId, string? subId);
    }
}
