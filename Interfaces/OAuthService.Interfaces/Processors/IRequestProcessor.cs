using OAuthService.Core.Base;
using OAuthService.Core.Types;
using OAuthService.Core.Types.Responses;

namespace OAuthService.Interfaces.Processors
{
    public interface IRequestProcessor<in T> where T : IRequest
    {
        Task<AccessTokenResponse> ProcessToResponseAsync(Client responseAud, T request, CancellationToken cancellationToken = default);
    }
}
