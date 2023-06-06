using OAuthService.Core.Base;
using OAuthService.Core.Types;

namespace OAuthService.Interfaces.Processors
{
    public interface IRequestProcessor<in T> where T : IRequest
    {
        Task<IResponse> ProcessToResponseAsync(Client responseAud, T request, CancellationToken cancellationToken = default);
    }
}
