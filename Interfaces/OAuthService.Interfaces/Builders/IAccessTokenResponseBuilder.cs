using OAuthService.Core.Types.Responses;
using OAuthService.Interfaces.Builders.Base;

namespace OAuthService.Interfaces.Builders
{
    public interface IAccessTokenResponseBuilder : IBaseBuilder<AccessTokenResponse>
    {
        IAccessTokenResponseBuilder AddAccessToken(string accessToken);

        IAccessTokenResponseBuilder AddTokenType(string tokenType);

        IAccessTokenResponseBuilder AddExpiresIn(uint expiresIn);

        IAccessTokenResponseBuilder AddRefreshToken(string refreshToken);
    }
}
