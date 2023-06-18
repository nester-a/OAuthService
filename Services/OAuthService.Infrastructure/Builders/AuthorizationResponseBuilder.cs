using OAuth.Types.Abstraction;
using OAuthService.Infrastructure.Types;

namespace OAuthService.Infrastructure.Builders
{
    public class AuthorizationResponseBuilder
    {
        AuthorizationResponse response = new();

        public AuthorizationResponseBuilder AddCode(string code)
        {
            response.Code = code;
            return this;
        }

        public AuthorizationResponseBuilder AddState(string state)
        {
            response.State = state;
            return this;
        }

        public IAuthorizationResponse Build()
        {
            return response;
        }
    }
}
