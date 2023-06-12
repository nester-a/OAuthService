using OAuth.Types.Abstraction;
using OAuthService.Data.Abstraction;
using OAuthService.Exceptions;
using OAuthService.Infrastructure.Abstraction;

namespace OAuthService.Infrastructure.Factories
{
    public class AuthorizationResponseFactory : IAuthorizationResponseFactory
    {
        private readonly IUserStorage userStorage;
        private readonly ICodeStorage codeStorage;
        private readonly AuthorizationResponseBuilderFactory responseBuilderFactory;

        public AuthorizationResponseFactory(IUserStorage userStorage, 
            ICodeStorage codeStorage,
            AuthorizationResponseBuilderFactory responseBuilderFactory)
        {
            this.userStorage = userStorage;
            this.codeStorage = codeStorage;
            this.responseBuilderFactory = responseBuilderFactory;
        }
        public async Task<IAuthorizationResponse> CreateAsync(IAuthorizationRequest request, string username, string password, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            var hash = password;

            var userId = await userStorage.GetUserIdByUsernameAndPasswordHashAsync(username, hash, cancellation);

            if(string.IsNullOrWhiteSpace(userId))
            {
                throw new AccessDeniedException(username);
            }

            var exp = DateTime.UtcNow.AddMinutes(10);

            var code = Guid.NewGuid()
                           .ToString()
                           .Split('-')[0]
                           .ToUpper();

            await codeStorage.SaveCodeAsync(code, exp, userId, request.ClientId, cancellation);

            var builder = responseBuilderFactory.Create()
                                                .AddCode(code);

            if (!string.IsNullOrWhiteSpace(request.State))
            {
                builder = builder.AddState(request.State);
            }

            return builder.Build();
        }
    }
}
