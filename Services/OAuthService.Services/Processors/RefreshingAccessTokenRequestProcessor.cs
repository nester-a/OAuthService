using OAuthService.Core.Base;
using OAuthService.Core.Enums;
using OAuthService.Core.Types;
using OAuthService.Core.Types.Responses;
using OAuthService.Exceptions;
using OAuthService.Interfaces.Builders;
using OAuthService.Interfaces.Processors;
using OAuthService.Interfaces.Storages;
using OAuthService.Services.Processors.Base;

namespace OAuthService.Services.Processors
{
    public class RefreshingAccessTokenRequestProcessor : BaseRequestProcessor, IRequestProcessor<IRefreshingAccessTokenRequest>
    {
        private readonly ITokenStorage tokenStorage;

        public RefreshingAccessTokenRequestProcessor(ITokenBuilder tokenBuilder, 
                                                     ITokenStorage tokenStorage, 
                                                     IAccessTokenResponseBuilder accessTokenResponseBuilder) 
                                                     : base(tokenBuilder, tokenStorage, accessTokenResponseBuilder)
        {
            this.tokenStorage = tokenStorage;
        }

        public async Task<AccessTokenResponse> ProcessToResponseAsync(Client responseAud, IRefreshingAccessTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var subj = await tokenStorage.GetUserIdByValidTokenAsync(request.RefreshToken!, cancellationToken);

            if (string.IsNullOrWhiteSpace(subj))
            {
                throw new InvalidGrantException(nameof(request.RefreshToken));
            }

            return await BuildResponseAsync(responseAud, subj, TokenSubject.User, true, cancellationToken);
        }
    }
}
