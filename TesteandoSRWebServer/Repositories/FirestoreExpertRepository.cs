using Google.Cloud.Firestore;
using TesteandoSRWebServer.Interfaces;

namespace TesteandoSRWebServer.Repositories
{
    public class FirestoreExpertRepository(FirestoreDb db) : IExpertRepository
    {
        private readonly FirestoreDb _db = db;

        public async Task SaveMpIdAsync(string mpId, string userId)
        {
            try
            {
                DocumentReference docRef = _db.Collection("Expertos").Document(userId);
                Dictionary<string, object> update = new()
            {
                { "mpId", mpId }
            };
                await docRef.UpdateAsync(update);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<bool> SaveSubIdAsync(string? userId, string? subId)
        {
            try
            {
                if (userId == null || subId == null) return false;

                DocumentReference docRef = _db.Collection("Expertos").Document(userId);
                Dictionary<string, object> update = new()
            {
                { "subId", subId }
            };
                await docRef.UpdateAsync(update);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
