using OAuthService.Core.Enums;
using OAuthService.Interfaces.Storages;

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
    }
}
