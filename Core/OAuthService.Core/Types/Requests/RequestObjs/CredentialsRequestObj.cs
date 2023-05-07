using OAuthService.Core.Base;
using OAuthService.Core.Enums;

namespace OAuthService.Core.Types.Requests.RequestObjs
{
    public class CredentialsRequestObj : IRequest
    {
        public Grant GrantType => Grant.ClientCredentials;
        public string ClientId { get; }
        public string? Scope { get; }

        public CredentialsRequestObj(AccessTokenRequest accessTokenRequest)
        {
            ClientId = accessTokenRequest.ClientId!;
            Scope = accessTokenRequest.Scope;
        }
    }
}
