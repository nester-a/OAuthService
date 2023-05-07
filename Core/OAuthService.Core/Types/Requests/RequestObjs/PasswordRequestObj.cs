using OAuthService.Core.Base;
using OAuthService.Core.Enums;

namespace OAuthService.Core.Types.Requests.RequestObjs
{
    public class PasswordRequestObj : IRequest
    {
        public Grant GrantType => Grant.ResourceOwnerPasswordCredentials; 
        public string Username { get; }
        public string Password { get; }
        public string? Scope { get; }

        public PasswordRequestObj(AccessTokenRequest accessTokenRequest)
        {
            Username = accessTokenRequest.Username!;
            Password = accessTokenRequest.Password!;
            Scope = accessTokenRequest.Scope;
        }
    }
}
