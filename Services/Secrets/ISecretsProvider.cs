namespace Services.Secrets
{
    public interface ISecretsProvider
    {
        string GetConnectionString();
        string GetClientId();
        string GetClientSecret();
    }
}
