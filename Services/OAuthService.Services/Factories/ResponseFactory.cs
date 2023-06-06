using OAuthService.Core.Base;
using OAuthService.Core.Types.Requests;
using OAuthService.Interfaces.Facroies;
using OAuthService.Interfaces.Processors;
using OAuthService.Exceptions;

using static OAuthConstans.AccessTokenRequestGrantType;
using OAuthService.Core.Types;
using OAuthService.Core.Types.Responses;

namespace OAuthService.Services.Factories
{
    public class ResponseFactory : IRequestResponseFactory
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
        public async Task<AccessTokenResponse> CreateResponseAsync(Client responseAud, AccessTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return request.GrantType switch
            {
                AuthorizationCode => await codeRequestProcessor.ProcessToResponseAsync(responseAud, request, cancellationToken),
                Password => await passwordRequestProcessor.ProcessToResponseAsync(responseAud, request, cancellationToken),
                ClientCredentials => await clientCredentialRequestProcessor.ProcessToResponseAsync(responseAud, request, cancellationToken),
                RefreshToken => await refreshingAccessRequest.ProcessToResponseAsync(responseAud, request, cancellationToken),
                _ => throw new ServerErrorException(nameof(request.GrantType))
            };
        }
    }
}
