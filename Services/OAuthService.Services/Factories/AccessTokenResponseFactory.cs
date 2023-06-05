using OAuthService.Core.Enums;
using OAuthService.Core.Extensions;
using OAuthService.Core.Types;
using OAuthService.Core.Types.Responses;
using OAuthService.Interfaces.Builders;
using OAuthService.Interfaces.Facroies;
using OAuthService.Interfaces.Storages;

namespace OAuthService.Services.Factories
{
    public class AccessTokenResponseFactory : IAccessTokenResponseFactory
    {
        private readonly ITokenBuilder tokenBuilder;
        private readonly IAccessTokenResponseBuilder accessTokenResponseBuilder;
        private readonly ITokenStorage tokenStorage;

        public AccessTokenResponseFactory(ITokenBuilder tokenBuilder, IAccessTokenResponseBuilder accessTokenResponseBuilder, ITokenStorage tokenStorage)
        {
            this.tokenBuilder = tokenBuilder;
            this.accessTokenResponseBuilder = accessTokenResponseBuilder;
            this.tokenStorage = tokenStorage;
        }
        public async Task<AccessTokenResponse> CreateResponseAsync(Client client, string responseTokenSub, TokenSubject tokenSubjectType, bool refreshTokenRequired, CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            var exp = now.AddDays(1);
            var jti = Guid.NewGuid().ToString();
            var key = client!.TokenKey;

            var token = await tokenBuilder.SignedWithKey(key)
                                          .AddIat(now)
                                          .AddNbf(now)
                                          .AddExp(exp)
                                          .AddJti(jti)
                                          .AddAud(client.Id)
                                          .AddSub(responseTokenSub)
                                          .BuildAsync(cancellationToken);

            if (tokenSubjectType is TokenSubject.User)
            {
                await tokenStorage.SaveTokenAsync(jti, token, TokenType.AccessToken, exp, responseTokenSub, cancellationToken);
            }
            else
            {
                await tokenStorage.SaveTokenAsync(jti, token, TokenType.AccessToken, exp, null, cancellationToken);
            }

            accessTokenResponseBuilder.AddAccessToken(token)
                                      .AddTokenType("Bearer")
                                      .AddExpiresIn(exp.ToUnixTimestamp());

            if (refreshTokenRequired)
            {
                jti = Guid.NewGuid().ToString();
                var refreshToken = Guid.NewGuid().ToString();
                exp = now.AddDays(7);

                if (tokenSubjectType is TokenSubject.User)
                {

                    await tokenStorage.SaveTokenAsync(jti, refreshToken, TokenType.RefreshToken, exp, responseTokenSub, cancellationToken);
                }
                else
                {

                    await tokenStorage.SaveTokenAsync(jti, refreshToken, TokenType.RefreshToken, exp, null, cancellationToken);
                }

                accessTokenResponseBuilder.AddRefreshToken(refreshToken);
            }

            return accessTokenResponseBuilder.Build();
        }
    }
}
