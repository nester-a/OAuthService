using OAuth.Types.Abstraction;
using OAuthConstans;
using OAuthService.Core.Entities;
using OAuthService.Core.Enums;
using OAuthService.Data.Abstraction;
using OAuthService.Exceptions;
using OAuthService.Infrastructure.Abstraction;
using OAuthService.Infrastructure.Extensions;

namespace OAuthService.Infrastructure.Factories
{
    public class AccessTokenResponseFactory : IAccessTokenResponseFactory
    {
        private readonly ITokenStorage tokenStorage;
        private readonly ICodeStorage codeStorage;
        private readonly IUserStorage userStorage;
        private readonly JwtBuilderFactory jwtBuilderFactory;
        private readonly AccessTokenResponseBuilderFactory accessTokenResponseBuilderFactory;

        public AccessTokenResponseFactory(ITokenStorage tokenStorage, 
            ICodeStorage codeStorage, 
            IUserStorage userStorage,
            JwtBuilderFactory jwtBuilderFactory,
            AccessTokenResponseBuilderFactory accessTokenResponseBuilderFactory)
        {
            this.tokenStorage = tokenStorage;
            this.codeStorage = codeStorage;
            this.userStorage = userStorage;
            this.jwtBuilderFactory = jwtBuilderFactory;
            this.accessTokenResponseBuilderFactory = accessTokenResponseBuilderFactory;
        }

        public async Task<IAccessTokenResponse> CreateAsync(Client client, IAccessTokenRequest request, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            string? tokenSub;

            switch (request.GrantType)
            {
                case AccessTokenRequestGrantType.ClientCredentials:
                    tokenSub = client.Id;
                    break;
                case AccessTokenRequestGrantType.RefreshToken:
                    tokenSub = await tokenStorage.GetUserIdByValidTokenAsync(request.RefreshToken!, cancellation);
                    break;
                case AccessTokenRequestGrantType.AuthorizationCode:
                    tokenSub = await codeStorage.GetUserIdByCodeAndClientIdAsync(request.Code!, request.ClientId!, cancellation);
                    break;
                case AccessTokenRequestGrantType.Password:
                    var hash = request.Password!;
                    tokenSub = await userStorage.GetUserIdByUsernameAndPasswordHashAsync(request.Username!, hash, cancellation);
                    break;
                default:
                    throw new UnsupportedGrantTypeException(request.GrantType);
            }

            if (string.IsNullOrEmpty(tokenSub))
            {
                throw new InvalidGrantException(request.GrantType);
            }

            var now = DateTime.UtcNow;
            var exp = now.AddDays(1);
            var jti = Guid.NewGuid().ToString();
            var key = client.TokenKey;

            var jwtBuilder = jwtBuilderFactory.Create().SignedWithKey(key)
                                                       .AddIat(now)
                                                       .AddNbf(now)
                                                       .AddExp(exp)
                                                       .AddJti(jti)
                                                       .AddSub(tokenSub);

            if(request.GrantType != AccessTokenRequestGrantType.ClientCredentials)
            {
                jwtBuilder = jwtBuilder.AddAud(client.Id);
            }

            var token = await jwtBuilder.BuildAsync();

            if (request.GrantType == AccessTokenRequestGrantType.ClientCredentials)
            {
                await tokenStorage.SaveTokenAsync(jti, token, TokenType.AccessToken, exp, null, cancellation);
            }
            else
            {
                await tokenStorage.SaveTokenAsync(jti, token, TokenType.AccessToken, exp, tokenSub, cancellation);
            }


            var responseBuilder = accessTokenResponseBuilderFactory.Create().AddAccessToken(token)
                                                                            .AddTokenType(AccessTokenType.Bearer)
                                                                            .AddExpiresIn(exp.ToUnixTimestamp());

            if (request.GrantType != AccessTokenRequestGrantType.ClientCredentials)
            {
                jti = Guid.NewGuid().ToString();
                var refreshToken = Guid.NewGuid().ToString();
                exp = now.AddDays(7);

                await tokenStorage.SaveTokenAsync(jti, refreshToken, TokenType.RefreshToken, exp, tokenSub, cancellation);

                responseBuilder = responseBuilder.AddRefreshToken(refreshToken);
            }


            return responseBuilder.Build();
        }
    }
}
