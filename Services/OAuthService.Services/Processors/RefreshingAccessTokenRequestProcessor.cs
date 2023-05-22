using OAuthService.Core.Base;
using OAuthService.Core.Enums;
using OAuthService.Core.Exceptions;
using OAuthService.Interfaces.Accessors;
using OAuthService.Interfaces.Builders;
using OAuthService.Interfaces.Processors;
using OAuthService.Interfaces.Storages;
using OAuthService.Services.Processors.Base;

namespace OAuthService.Services.Processors
{
    public class RefreshingAccessTokenRequestProcessor : BaseRequestProcessor, IRequestProcessor<IRefreshingAccessTokenRequest>
    {
        private readonly ITokenStorage tokenStorage;

        public RefreshingAccessTokenRequestProcessor(IClientAccessor clientAccessor, 
            ITokenBuilder tokenBuilder, 
            ITokenStorage tokenStorage, 
            IAccessTokenResponseBuilder accessTokenResponseBuilder) : base(clientAccessor, tokenBuilder, tokenStorage, accessTokenResponseBuilder)
        {
            this.tokenStorage = tokenStorage;
        }

        public async Task<IResponse> ProcessToResponseAsync(IRefreshingAccessTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var subj = await tokenStorage.GetUserIdByValidTokenAsync(request.RefreshToken!, cancellationToken);

            if (string.IsNullOrWhiteSpace(subj))
            {
                throw new InvalidGrantException(nameof(request.RefreshToken));
            }

            return await BuildResponseAsync(subj, TokenSubject.User, true, cancellationToken);
        }
    }
}
