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
        private readonly IRequestProcessor<IClientCredentialTokenRequest> clientCredentialRequestProcessor;
        private readonly IRequestProcessor<IRefreshingAccessTokenRequest> refreshingAccessRequest;

        public ResponseFactory(IRequestProcessor<ICodeGrantTokenRequest> codeRequestProcessor,
            IRequestProcessor<IPasswordGrantTokenRequest> passwordRequestProcessor,
            IRequestProcessor<IClientCredentialTokenRequest> clientCredentialRequestProcessor,
            IRequestProcessor<IRefreshingAccessTokenRequest> refreshingAccessRequest)
        {
            this.codeRequestProcessor = codeRequestProcessor;
            this.passwordRequestProcessor = passwordRequestProcessor;
            this.clientCredentialRequestProcessor = clientCredentialRequestProcessor;
            this.refreshingAccessRequest = refreshingAccessRequest;
        }
        public async Task<IResponse> CreateResponseAsync(AccessTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return request.GrantType switch
            {
                GrantType.AuthorizationCode => await codeRequestProcessor.ProcessToResponseAsync(request, cancellationToken),
                GrantType.ResourceOwnerPasswordCredentials => await passwordRequestProcessor.ProcessToResponseAsync(request, cancellationToken),
                GrantType.ClientCredentials => await clientCredentialRequestProcessor.ProcessToResponseAsync(request, cancellationToken),
                GrantType.RefreshToken => await refreshingAccessRequest.ProcessToResponseAsync(request, cancellationToken),
                _ => throw new ServerErrorException("Smth wrong")
            };
        }
    }
}
