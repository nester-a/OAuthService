using OAuthService.Core.Base;
using OAuthService.Core.Enums;

namespace OAuthService.Core.Types.Requests.RequestObjs
{
    public class CodeRequestObj : IRequest
    {
        public Grant GrantType => Grant.AuthorizationCode;
        public string Code { get; }
        public string RedirectUri { get; }
        public string ClientId { get; }

        public CodeRequestObj(AccessTokenRequest accessTokenRequest)
        {
            Code = accessTokenRequest.Code!;
            RedirectUri = accessTokenRequest.RedirectUri!;
            ClientId = accessTokenRequest.ClientId!;
        }
    }
}
