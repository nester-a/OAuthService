using OAuthService.Core.Base;
using OAuthService.Core.Exceptions;
using OAuthService.Interfaces.Storages;
using OAuthService.Core.Exceptions.Base;
using OAuthService.Core.Types.Responses;
using OAuthService.Interfaces.Builders;
using OAuthService.Interfaces.Accessors;
using OAuthService.Interfaces.Processors;
using OAuthService.Services.Processors.Base;
using OAuthService.Core.Enums;

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

            try
            {
                var code = await codeStorage.GetCodeByCodeAndClientIdAsync(request.Code!, request.ClientId!, cancellationToken);

                if (code is null || code?.ValidTillUtc < DateTime.UtcNow)
                {
                    throw new InvalidGrantException(nameof(code));
                }

                return await BuildResponseAsync(code!.UserId, TokenSubject.User, true, cancellationToken);
            }
            catch (OAuthException ex)
            {
                return new ErrorResponse(ex);
            }
        }
    }
}
