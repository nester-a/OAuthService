using OAuthService.Core.Base;
using OAuthService.Core.Enums;

namespace OAuthService.Core.Types.Requests.RequestObjs
{
    public class RefreshingRequestObj : IRequest
    {
        public Grant GrantType => Grant.RefreshToken;
        public string RefreshToken { get; }


        public RefreshingRequestObj(AccessTokenRequest accessTokenRequest)
        {
            RefreshToken = accessTokenRequest.RefreshToken!;
        }
    }
}
