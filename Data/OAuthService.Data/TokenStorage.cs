using OAuthService.Core.Enums;
using OAuthService.Data.Abstraction;

namespace OAuthService.Data
{
    public class TokenStorage : ITokenStorage
    {
        public async Task SaveTokenAsync(string tokenId, string tokeValue, TokenType tokenType, DateTime valiTillUtc, string? userId = null, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            return;
        }

        public async Task<string?> GetUserIdByValidTokenAsync(string tokenValue, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            return "123";
        }

        public async Task RevokeTokenAsync(string tokenValue, TokenType tokenType, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();
        }
    }
}
