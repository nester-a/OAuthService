using OAuthService.Infrastructure.Builders;

namespace OAuthService.Infrastructure.Factories
{
    public class AuthorizationResponseBuilderFactory
    {
        public AuthorizationResponseBuilder Create() => new();
    }
}
