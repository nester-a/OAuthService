using OAuthService.Core.Base;
using OAuthService.Interfaces.Storages;
using OAuthService.Interfaces.Builders;
using OAuthService.Interfaces.Processors;
using OAuthService.Services.Processors.Base;
using OAuthService.Core.Enums;
using OAuthService.Exceptions;
using OAuthService.Core.Types;

namespace OAuthService.Services.Processors
{
    public class CodeRequestProcessor : BaseRequestProcessor, IRequestProcessor<ICodeGrantTokenRequest>
    {
        private readonly ICodeStorage codeStorage;

        public CodeRequestProcessor(ICodeStorage codeStorage, 
                                    ITokenBuilder tokenBuilder, 
                                    ITokenStorage tokenStorage,
                                    IAccessTokenResponseBuilder accessTokenResponseBuilder) 
                                    : base(tokenBuilder, tokenStorage, accessTokenResponseBuilder)
        {
            this.codeStorage = codeStorage;
        }

        public async Task<IResponse> ProcessToResponseAsync(Client responseAud, ICodeGrantTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userId = await codeStorage.GetUserIdByCodeAndClientIdAsync(request.Code!, request.ClientId!, cancellationToken);

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new InvalidGrantException(nameof(request.Code));
            }

            return await BuildResponseAsync(responseAud, userId, TokenSubject.User, true, cancellationToken);
        }
    }
}
