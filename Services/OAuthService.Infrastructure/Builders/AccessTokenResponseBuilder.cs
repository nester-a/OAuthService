using OAuth.Types.Abstraction;
using OAuthService.Types;

namespace OAuthService.Infrastructure.Builders
{
    public class AccessTokenResponseBuilder
    {
        readonly AccessTokenResponse response = new ();

        public AccessTokenResponseBuilder AddAccessToken(string accessToken)
        {
            response.AccessToken = accessToken;
            return this;
        }

        public AccessTokenResponseBuilder AddTokenType(string tokenType)
        {
            response.TokenType = tokenType;
            return this;
        }

        public AccessTokenResponseBuilder AddExpiresIn(uint expiresIn)
        {
            response.ExpiresIn = expiresIn;
            return this;
        }

        public AccessTokenResponseBuilder AddRefreshToken(string refreshToken)
        {
            response.RefreshToken = refreshToken;
            return this;
        }

        public IAccessTokenResponse Build() => response;
    }
}
