using OAuthService.Infrastructure.Builders;

namespace OAuthService.Infrastructure.Factories
{
    public class JwtBuilderFactory
    {
        public JwtBuilder Create() => 
            new();
    }
}
