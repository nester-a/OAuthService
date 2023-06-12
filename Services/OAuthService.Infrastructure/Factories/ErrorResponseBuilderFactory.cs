using OAuthService.Infrastructure.Builders;

namespace OAuthService.Infrastructure.Factories
{
    public class ErrorResponseBuilderFactory
    {
        public ErrorResponseBuilder Create() 
            => new();
    }
}
