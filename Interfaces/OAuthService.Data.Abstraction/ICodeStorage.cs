namespace OAuthService.Data.Abstraction
{
    public interface ICodeStorage
    {
        Task<string> GetUserIdByCodeAndClientIdAsync(string code, string clientId, CancellationToken cancellationToken = default);
        Task SaveCodeAsync(string code, DateTime valiTillUtc, string userId, string clientId, CancellationToken cancellation = default);
    }
}
