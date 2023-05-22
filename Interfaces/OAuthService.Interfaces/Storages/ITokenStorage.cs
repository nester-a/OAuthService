using OAuthService.Core.Enums;

namespace OAuthService.Interfaces.Storages
{
    public interface ITokenStorage
    {
        Task SaveTokenAsync(string tokenId, string tokeValue, TokenType tokenType, DateTime valiTillUtc, string? userId = null, CancellationToken cancellation = default);

        Task<string> GetUserIdByValidTokenAsync(string tokenValue, CancellationToken cancellation = default);
    }
}
