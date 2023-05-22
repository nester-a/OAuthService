using OAuthService.Core.Base;
using OAuthService.Core.Exceptions.Base;
using OAuthService.Core.Exceptions;
using OAuthService.Core.Types.Responses;
using OAuthService.Interfaces.Accessors;
using OAuthService.Interfaces.Builders;
using OAuthService.Interfaces.Processors;
using OAuthService.Interfaces.Storages;
using OAuthService.Services.Processors.Base;
using OAuthService.Core.Enums;

namespace OAuthService.Services.Processors
{
    public class PasswordRequestProcessor : BaseRequestProcessor, IRequestProcessor<IPasswordGrantTokenRequest>
    {
        private readonly IUserStorage userStorage;

        public PasswordRequestProcessor(IUserStorage userStorage,
            IClientAccessor clientAccessor, 
            ITokenBuilder tokenBuilder, 
            ITokenStorage tokenStorage, 
            IAccessTokenResponseBuilder accessTokenResponseBuilder) : base(clientAccessor, tokenBuilder, tokenStorage, accessTokenResponseBuilder)
        {
            this.userStorage = userStorage;
        }

        public async Task<IResponse> ProcessToResponseAsync(IPasswordGrantTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var hash = request.Password!;

                var id = await userStorage.GetUserIdByUsernameAndPasswordHashAsync(request.Username!, hash, cancellationToken);

                if (string.IsNullOrWhiteSpace(id))
                {
                    throw new InvalidGrantException(nameof(id));
                }

                return await BuildResponseAsync(id, TokenSubject.User, true, cancellationToken);
            }
            catch (OAuthException ex)
            {
                return new ErrorResponse(ex);
            }
        }
    }
}
