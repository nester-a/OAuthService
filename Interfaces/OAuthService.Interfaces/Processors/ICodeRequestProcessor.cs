using OAuthService.Core.Base;
using OAuthService.Core.Types.Requests;

namespace OAuthService.Interfaces.Processors
{
    public interface ICodeRequestProcessor
    {
        Task<IResponse> ProcessToResponseAsync(AccessTokenRequest request, CancellationToken cancellationToken = default);
    }
}
