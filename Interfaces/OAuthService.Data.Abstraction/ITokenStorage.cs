using OAuthService.Core.Enums;

namespace OAuthService.Data.Abstraction
{
    public interface ITokenStorage
    {
        Task SaveTokenAsync(string tokenId, string tokeValue, TokenType tokenType, DateTime valiTillUtc, string? userId = null, CancellationToken cancellation = default);

        Task<string?> GetUserIdByValidTokenAsync(string tokenValue, CancellationToken cancellation = default);

        Task RevokeTokenAsync(string tokenValue, TokenType tokenType, CancellationToken cancellation = default);
    }
}
