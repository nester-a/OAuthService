using OAuthService.Core.Base;
using OAuthService.Core.Types.Requests;

namespace OAuthService.Interfaces.Processors
{
    public interface IRequestProcessor<in T> where T : IRequest
    {
        Task<IResponse> ProcessToResponseAsync(T request, CancellationToken cancellationToken = default);
    }
}
