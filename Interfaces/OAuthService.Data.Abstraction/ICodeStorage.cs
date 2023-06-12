namespace OAuthService.Data.Abstraction
{
    public interface ICodeStorage
    {
        Task<string> GetUserIdByCodeAndClientIdAsync(string code, string clientId, CancellationToken cancellationToken = default);
    }
}
