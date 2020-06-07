namespace Web.Services.Secrets
{
    public interface ISecretsProvider
    {
        string GetClientId();
        string GetClientSecret();
    }
}
