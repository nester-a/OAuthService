using OAuthService.Infrastructure.Builders;

namespace OAuthService.Infrastructure.Factories
{
    public class AccessTokenResponseBuilderFactory
    {
        public AccessTokenResponseBuilder Create()
            => new();
    }
}
