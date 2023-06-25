using OAuth.Types.Abstraction;
using OAuthConstans;
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

        public async Task<IAccessTokenResponse> CreateForClientCredentialsAsync(string clientId, 
                                                                                string tokenKey,
                                                                                CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            return await CreateAsync(clientId, null, tokenKey, false, cancellation);
        }

        public async Task<IAccessTokenResponse> CreateForRefreshTokenAsync(string refreshToken, 
                                                                           string clientId,
                                                                           string tokenKey, 
                                                                           CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            var sub = await tokenStorage.GetUserIdByValidTokenAsync(refreshToken, cancellation);

            if (string.IsNullOrWhiteSpace(sub))
            {
                throw new InvalidGrantException("Invalid refresh token");
            }

            return await CreateAsync(sub, clientId, tokenKey, true, cancellation);
        }

        public async Task<IAccessTokenResponse> CreateForAuthorizationCodeAsync(string code,
                                                                                string clientId,
                                                                                string tokenKey,
                                                                                CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            var sub = await codeStorage.GetUserIdByCodeAndClientIdAsync(code, clientId, cancellation); 

            if (string.IsNullOrWhiteSpace(sub))
            {
                throw new InvalidGrantException("Invalid code");
            }

            return await CreateAsync(sub, clientId, tokenKey, false, cancellation);
        }

        public async Task<IAccessTokenResponse> CreateForPasswordAsync(string username,
                                                                       string password, 
                                                                       string clientId,
                                                                       string tokenKey,
                                                                       CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            var hash = password;

            var sub = await userStorage.GetUserIdByUsernameAndPasswordHashAsync(username, hash, cancellation); 
            
            if (string.IsNullOrWhiteSpace(sub))
            {
                throw new InvalidGrantException("Invalid username or password");
            }

            return await CreateAsync(sub, clientId, tokenKey, true, cancellation);
        }

        private async Task<IAccessTokenResponse> CreateAsync(string tokenSub, string? tokenAud, string tokenKey, bool refreshTokenRequired, CancellationToken cancellation = default)
        {
            var now = DateTime.UtcNow;
            var exp = now.AddDays(1);
            var jti = Guid.NewGuid().ToString();

            var jwtBuilder = jwtBuilderFactory.Create().SignedWithKey(tokenKey)
                                                       .AddIat(now)
                                                       .AddNbf(now)
                                                       .AddExp(exp)
                                                       .AddJti(jti)
                                                       .AddSub(tokenSub);

            string token;

            if (!string.IsNullOrWhiteSpace(tokenAud))
            {
                jwtBuilder = jwtBuilder.AddAud(tokenAud);

                token = await jwtBuilder.BuildAsync();

                await tokenStorage.SaveTokenAsync(jti, token, TokenType.AccessToken, exp, tokenSub, cancellation);
            }
            else
            {
                token = await jwtBuilder.BuildAsync();

                await tokenStorage.SaveTokenAsync(jti, token, TokenType.AccessToken, exp, null, cancellation);
            }


            var responseBuilder = accessTokenResponseBuilderFactory.Create().AddAccessToken(token)
                                                                            .AddTokenType(AccessTokenType.Bearer)
                                                                            .AddExpiresIn(exp.ToUnixTimestamp());

            if (refreshTokenRequired)
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
