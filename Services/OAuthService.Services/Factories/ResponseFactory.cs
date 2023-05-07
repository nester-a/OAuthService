using OAuthService.Core.Base;
using OAuthService.Core.Constans;
using OAuthService.Core.Exceptions;
using OAuthService.Core.Types.Requests;
using OAuthService.Interfaces.Facroies;
using OAuthService.Interfaces.Processors;

namespace OAuthService.Services.Factories
{
    public class ResponseFactory : IResponseFactory
    {
        private readonly ICodeRequestProcessor codeRequestProcessor;

        public ResponseFactory(ICodeRequestProcessor codeRequestProcessor)
        {
            this.codeRequestProcessor = codeRequestProcessor;
        }
        public async Task<IResponse> CreateResponseAsync(AccessTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return request.GrandType switch
            {
                GrantType.AuthorizationCode => await codeRequestProcessor.ProcessToResponseAsync(request, cancellationToken),
                _ => throw new ServerErrorException("Smth wrong")
            }; ;
        }
    }
}
