using OAuthService.Core.Types.Responses;
using OAuthService.Interfaces.Builders;

namespace OAuthService.Services.Builders
{
    public class AccessTokenResponseBuilder : IAccessTokenResponseBuilder
    {
        AccessTokenResponse response = new();

        public IAccessTokenResponseBuilder AddAccessToken(string accessToken)
        {
            response.AccessToken = accessToken;
            return this;
        }

        public IAccessTokenResponseBuilder AddTokenType(string tokenType)
        {
            response.TokenType = tokenType;
            return this;
        }

        public IAccessTokenResponseBuilder AddExpiresIn(uint expiresIn)
        {
            response.ExpiresIn = expiresIn;
            return this;
        }

        public IAccessTokenResponseBuilder AddRefreshToken(string refreshToken)
        {
            response.RefreshToken = refreshToken;
            return this;
        }

        public AccessTokenResponse Build() => response;
    }
}
