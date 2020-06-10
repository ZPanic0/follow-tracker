namespace Web.Services.AsyncQueue.Models
{
    public struct WebhookWorkItem
    {
        public WebhookWorkItem(string guid, WebhookType webhookType, string json)
        {
            Guid = guid;
            WebhookType = webhookType;
            Json = json;
        }

        public string Guid { get; }
        public WebhookType WebhookType { get; }
        public string Json { get; }
    }
}
