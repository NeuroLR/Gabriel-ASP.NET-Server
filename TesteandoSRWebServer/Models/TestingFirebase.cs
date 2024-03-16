using Google.Cloud.Firestore;


namespace TesteandoSRWebServer.Models
{
    public class TestingFirebase
    {
        private readonly FirestoreDb db;

        public TestingFirebase()
        {
            db = FirestoreDb.Create("necessito-proyecto-app");
        }

        public async void GetAllUsers()
        {
            CollectionReference docRef = db.Collection("Usuarios");
            QuerySnapshot snapshot = await docRef.Limit(5).GetSnapshotAsync(); 
            foreach (DocumentSnapshot item in snapshot) 
            {
                Console.WriteLine("Document data for {0} document:", item.Id);
                Dictionary<String, Object> doc = item.ToDictionary();
                foreach(KeyValuePair<String, Object> pair in doc) 
                {
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }
            }
        }
    }
}