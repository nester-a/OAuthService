namespace OAuthService.Interfaces.Storages
{
    public interface ICodeStorage
    {
        Task<string> GetUserIdByCodeAndClientIdAsync(string code, string clientId, CancellationToken cancellationToken = default);
    }
}
