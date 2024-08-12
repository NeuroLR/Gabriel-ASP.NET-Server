using Google.Cloud.Firestore;
using TesteandoSRWebServer.Interfaces;
using TesteandoSRWebServer.Models;

namespace TesteandoSRWebServer.Repositories
{
    public class FirebaseUserRepository(FirestoreDb db) : IUserRepository
    {
        private readonly FirestoreDb _db = db;

        public async Task<List<Usuario>> GetAllUsersAsync()
        {
            var users = new List<Usuario>();
            CollectionReference docRef = _db.Collection("Usuarios");
            QuerySnapshot snapshot = await docRef.Limit(5).GetSnapshotAsync();

            foreach (DocumentSnapshot item in snapshot)
            {
                Dictionary<string, object> doc = item.ToDictionary();
                Usuario user = new Usuario
                {
                    Nombre = doc["nombre"].ToString(),
                    Apellido = doc["apellido"].ToString(),
                    Movil = doc["movil"].ToString()
                };
                users.Add(user);
            }
            return users;
        }
    }
}
