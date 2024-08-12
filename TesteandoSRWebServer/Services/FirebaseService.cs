using TesteandoSRWebServer.Interfaces;

namespace TesteandoSRWebServer.Services
{
    public class FirebaseService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IExpertRepository _expertRepository;

        public FirebaseService(IUserRepository userRepository, IJobRepository jobRepository, IExpertRepository expertRepository)
        {
            _userRepository = userRepository;
            _jobRepository = jobRepository;
            _expertRepository = expertRepository;
        }

        public async Task DisplayAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            foreach (var user in users)
            {
                Console.WriteLine(user.ToString()); 
            }
        }

        public async Task DisplayAllJobsAsync()
        {
            var jobs = await _jobRepository.GetAllJobsAsync();
            Console.WriteLine(string.Join(", ", jobs));
        }

        public async Task SaveMpIdAsync(string mpId, string userId)
        {
            await _expertRepository.SaveMpIdAsync(mpId, userId);
        }

        public async Task<bool> SaveSubIdAsync(string? userId, string? subId)
        {
            return await _expertRepository.SaveSubIdAsync(userId, subId);
        }
    }
}
