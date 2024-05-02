namespace TesteandoSRWebServer.Models
{
    public class NotificationBody
    {
        public string? FcmToken { get; set; }
        public Dictionary<String, String>? Data { get; set; }
        public string? UserId { get; set; }
        public string? Activity { get; set; }
        public bool? Profesional { get; set; }
        public string? ReceiverId { get; set; }
        public string? DocId { get; set; }
    }
}
