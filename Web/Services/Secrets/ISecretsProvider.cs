namespace Web.Services.Secrets
{
    public interface ISecretsProvider
    {
        string GetConnectionString();
        string GetClientId();
        string GetClientSecret();
    }
}
