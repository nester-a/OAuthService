using OAuth.Types.Abstraction;

namespace OAuthService.Infrastructure.Abstraction
{
    public interface IAccessTokenResponseFactory
    {
        Task<IAccessTokenResponse> CreateForClientCredentialsAsync(string clientId, string tokenKey, CancellationToken cancellation = default);

        Task<IAccessTokenResponse> CreateForRefreshTokenAsync(string refreshToken, string clientId, string tokenKey, CancellationToken cancellation = default);

        Task<IAccessTokenResponse> CreateForAuthorizationCodeAsync(string code, string clientId, string tokenKey, CancellationToken cancellation = default);

        Task<IAccessTokenResponse> CreateForPasswordAsync(string username, string password, string clientId, string tokenKey, CancellationToken cancellation = default);
    }
}
