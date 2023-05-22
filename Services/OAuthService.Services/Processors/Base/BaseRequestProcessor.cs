using OAuthService.Core.Base;
using OAuthService.Core.Enums;
using OAuthService.Core.Extensions;
using OAuthService.Interfaces.Accessors;
using OAuthService.Interfaces.Builders;
using OAuthService.Interfaces.Storages;

namespace OAuthService.Services.Processors.Base
{
    public abstract class BaseRequestProcessor
    {
        private readonly IClientAccessor clientAccessor;
        private readonly ITokenBuilder tokenBuilder;
        private readonly ITokenStorage tokenStorage;
        private readonly IAccessTokenResponseBuilder accessTokenResponseBuilder;

        public BaseRequestProcessor(IClientAccessor clientAccessor, 
            ITokenBuilder tokenBuilder, 
            ITokenStorage tokenStorage,
            IAccessTokenResponseBuilder accessTokenResponseBuilder)
        {
            this.clientAccessor = clientAccessor;
            this.tokenBuilder = tokenBuilder;
            this.tokenStorage = tokenStorage;
            this.accessTokenResponseBuilder = accessTokenResponseBuilder;
        }
        protected async Task<IResponse> BuildResponseAsync(string responseTokenSub, bool refreshTokenRequired, CancellationToken cancellationToken = default)
        {
            var client = clientAccessor.Client;

            var now = DateTime.UtcNow;
            var exp = now.AddDays(1);
            var jti = Guid.NewGuid().ToString();
            var key = client.TokenKey;

            var token = await tokenBuilder.SignedWithKey(key)
                                          .AddIat(now)
                                          .AddNbf(now)
                                          .AddExp(exp)
                                          .AddJti(jti)
                                          .AddAud(client.Id)
                                          .AddSub(responseTokenSub)
                                          .BuildAsync(cancellationToken);

            await tokenStorage.SaveTokenAsync(jti, token, TokenType.AccessToken, exp, cancellationToken);

            accessTokenResponseBuilder.AddAccessToken(token)
                                      .AddTokenType("Bearer")
                                      .AddExpiresIn(exp.ToUnixTimestamp());

            if (refreshTokenRequired)
            {
                jti = Guid.NewGuid().ToString();
                var refreshToken = Guid.NewGuid().ToString();
                exp = now.AddDays(7);
                await tokenStorage.SaveTokenAsync(jti, refreshToken, TokenType.RefreshToken, exp, cancellationToken);

                accessTokenResponseBuilder.AddRefreshToken(refreshToken);
            }

            return accessTokenResponseBuilder.Build();
        }
    }
}
