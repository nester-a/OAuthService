using OAuthService.Core.Enums;
using OAuthService.Core.Types;
using OAuthService.Core.Types.Responses;

namespace OAuthService.Interfaces.Facroies
{
    public interface IAccessTokenResponseFactory
    {
        Task<AccessTokenResponse> CreateResponseAsync(Client client, string responseTokenSub, TokenSubject tokenSubjectType, bool refreshTokenRequired, CancellationToken cancellationToken = default);
    }
}
