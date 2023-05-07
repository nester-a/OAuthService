using OAuthService.Core.Enums;

namespace OAuthService.Interfaces.Storages
{
    public interface ITokenStorage
    {
        Task SaveTokenAsync(string tokenId, string tokeValue, TokenType tokenType, DateTime valiTillUtc, CancellationToken cancellation = default);
    }
}
