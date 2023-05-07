using OAuthService.Core.Enums;
using OAuthService.Interfaces.Storages;

namespace OAuthService.Data
{
    public class TokenStorage : ITokenStorage
    {
        public async Task SaveTokenAsync(string tokenId, string tokeValue, TokenType tokenType, DateTime valiTillUtc, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            return;
        }
    }
}
