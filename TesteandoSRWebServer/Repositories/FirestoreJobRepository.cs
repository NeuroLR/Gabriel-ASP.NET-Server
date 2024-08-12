using Google.Cloud.Firestore;
using TesteandoSRWebServer.Interfaces;

namespace TesteandoSRWebServer.Repositories
{
    public class FirestoreJobRepository(FirestoreDb db) : IJobRepository
    {
        private readonly FirestoreDb _db = db;

        public async Task<List<string>> GetAllJobsAsync()
        {
            DocumentReference docRef = _db.Collection("Oficios").Document("allJobs");
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            return snapshot.GetValue<List<string>>("oficios");
        }
    }
}
