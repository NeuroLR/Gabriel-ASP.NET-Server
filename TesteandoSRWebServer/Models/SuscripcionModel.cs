namespace TesteandoSRWebServer.Models
{
    public class SuscripcionModel
    {
        public string card_token_id { get; set; }
        public string payer_email { get; set; }
        public string back_url { get; set; }
        public string preapproval_plan_id { get; set; }
        public string reason { get; set; }
        public string status { get; set; }
    }
}
