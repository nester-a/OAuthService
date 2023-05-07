using OAuthService.Core.Types.Responses;

namespace OAuthService.Interfaces.Builders
{
    public interface IAccessTokenResponseBuilder
    {
        IAccessTokenResponseBuilder AddAccessToken(string accessToken);

        IAccessTokenResponseBuilder AddTokenType(string tokenType);

        IAccessTokenResponseBuilder AddExpiresIn(uint expiresIn);

        IAccessTokenResponseBuilder AddRefreshToken(string refreshToken);

        AccessTokenResponse Build();
    }
}
