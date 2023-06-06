using OAuthService.Core.Base;
using OAuthService.Interfaces.Builders;
using OAuthService.Interfaces.Processors;
using OAuthService.Interfaces.Storages;
using OAuthService.Services.Processors.Base;
using OAuthService.Core.Enums;
using OAuthService.Exceptions;
using OAuthService.Core.Types;
using OAuthService.Core.Types.Responses;

namespace OAuthService.Services.Processors
{
    public class PasswordRequestProcessor : BaseRequestProcessor, IRequestProcessor<IPasswordGrantTokenRequest>
    {
        private readonly IUserStorage userStorage;

        public PasswordRequestProcessor(IUserStorage userStorage,
                                        ITokenBuilder tokenBuilder, 
                                        ITokenStorage tokenStorage, 
                                        IAccessTokenResponseBuilder accessTokenResponseBuilder) 
                                        : base(tokenBuilder, tokenStorage, accessTokenResponseBuilder)
        {
            this.userStorage = userStorage;
        }

        public async Task<AccessTokenResponse> ProcessToResponseAsync(Client responseAud, IPasswordGrantTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var hash = request.Password!;

            var id = await userStorage.GetUserIdByUsernameAndPasswordHashAsync(request.Username!, hash, cancellationToken);

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new InvalidGrantException(nameof(id));
            }

            return await BuildResponseAsync(responseAud, id, TokenSubject.User, true, cancellationToken);
        }
    }
}
