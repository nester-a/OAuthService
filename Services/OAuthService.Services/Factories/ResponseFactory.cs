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
        private readonly IRequestProcessor<ICodeGrantTokenRequest> codeRequestProcessor;
        private readonly IRequestProcessor<IPasswordGrantTokenRequest> passwordRequestProcessor;

        public ResponseFactory(IRequestProcessor<ICodeGrantTokenRequest> codeRequestProcessor,
            IRequestProcessor<IPasswordGrantTokenRequest> passwordRequestProcessor)
        {
            this.codeRequestProcessor = codeRequestProcessor;
            this.passwordRequestProcessor = passwordRequestProcessor;
        }
        public async Task<IResponse> CreateResponseAsync(AccessTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return request.GrantType switch
            {
                GrantType.AuthorizationCode => await codeRequestProcessor.ProcessToResponseAsync(request, cancellationToken),
                GrantType.ResourceOwnerPasswordCredentials => await passwordRequestProcessor.ProcessToResponseAsync(request, cancellationToken),
                _ => throw new ServerErrorException("Smth wrong")
            };
        }
    }
}
