using OAuthService.Core.Base;
using OAuthService.Core.Enums;
using OAuthService.Core.Types;
using OAuthService.Interfaces.Builders;
using OAuthService.Interfaces.Processors;
using OAuthService.Interfaces.Storages;
using OAuthService.Services.Processors.Base;

namespace OAuthService.Services.Processors
{
    public class ClientCredentialRequestProcessor : BaseRequestProcessor, IRequestProcessor<IClientCredentialTokenRequest>
    {
        public ClientCredentialRequestProcessor(ITokenBuilder tokenBuilder,
                                                ITokenStorage tokenStorage,
                                                IAccessTokenResponseBuilder accessTokenResponseBuilder)
                                                : base(tokenBuilder, tokenStorage, accessTokenResponseBuilder) { }

        public async Task<IResponse> ProcessToResponseAsync(Client responseAud, IClientCredentialTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await BuildResponseAsync(responseAud, responseAud.Id, TokenSubject.Client, false, cancellationToken);
        }
    }
}
