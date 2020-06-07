using Web.Services.Secrets.Models;

namespace Web.Services.Secrets
{
    public class SecretsModel
    {
        public string ConnectionString { get; set; }
        public Twitch Twitch { get; set; }
    }
}
