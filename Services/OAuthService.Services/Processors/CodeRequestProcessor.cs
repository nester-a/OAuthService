using OAuthService.Core.Base;
using OAuthService.Interfaces.Storages;
using OAuthService.Interfaces.Builders;
using OAuthService.Interfaces.Accessors;
using OAuthService.Interfaces.Processors;
using OAuthService.Services.Processors.Base;
using OAuthService.Core.Enums;
using OAuthService.Exceptions;

namespace OAuthService.Services.Processors
{
    public class CodeRequestProcessor : BaseRequestProcessor, IRequestProcessor<ICodeGrantTokenRequest>
    {
        private readonly ICodeStorage codeStorage;

        public CodeRequestProcessor(
            IClientAccessor clientAccessor,
            ICodeStorage codeStorage, 
            ITokenBuilder tokenBuilder, 
            ITokenStorage tokenStorage,
            IAccessTokenResponseBuilder accessTokenResponseBuilder) : base(clientAccessor, tokenBuilder, tokenStorage, accessTokenResponseBuilder)
        {
            this.codeStorage = codeStorage;
        }

        public async Task<IResponse> ProcessToResponseAsync(ICodeGrantTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userId = await codeStorage.GetUserIdByCodeAndClientIdAsync(request.Code!, request.ClientId!, cancellationToken);

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new InvalidGrantException(nameof(request.Code));
            }

            return await BuildResponseAsync(userId, TokenSubject.User, true, cancellationToken);
        }
    }
}
