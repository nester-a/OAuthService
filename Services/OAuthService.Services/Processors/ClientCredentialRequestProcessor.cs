using OAuthService.Core.Base;
using OAuthService.Core.Enums;
using OAuthService.Interfaces.Accessors;
using OAuthService.Interfaces.Builders;
using OAuthService.Interfaces.Processors;
using OAuthService.Interfaces.Storages;
using OAuthService.Services.Processors.Base;

namespace OAuthService.Services.Processors
{
    public class ClientCredentialRequestProcessor : BaseRequestProcessor, IRequestProcessor<IClientCredentialTokenRequest>
    {
        private readonly IClientAccessor clientAccessor;

        public ClientCredentialRequestProcessor(IClientAccessor clientAccessor,
            ITokenBuilder tokenBuilder,
            ITokenStorage tokenStorage,
            IAccessTokenResponseBuilder accessTokenResponseBuilder)
            : base(clientAccessor, tokenBuilder, tokenStorage, accessTokenResponseBuilder)
        {
            this.clientAccessor = clientAccessor;
        }

        public async Task<IResponse> ProcessToResponseAsync(IClientCredentialTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var sub = clientAccessor.Client!.Id;

            return await BuildResponseAsync(sub, TokenSubject.Client, false, cancellationToken);
        }
    }
}
