using OAuthService.Core.Base;
using OAuthService.Core.Exceptions;
using OAuthService.Interfaces.Storages;
using OAuthService.Core.Exceptions.Base;
using OAuthService.Core.Types.Responses;
using OAuthService.Interfaces.Builders;
using OAuthService.Core.Enums;
using OAuthService.Core.Extensions;
using OAuthService.Interfaces.Accessors;
using OAuthService.Core.Types.Requests;
using OAuthService.Interfaces.Processors;

namespace OAuthService.Services.Processors
{
    public class CodeRequestProcessor : ICodeRequestProcessor
    {
        private readonly IClientAccessor clientAccessor;
        private readonly ICodeStorage codeStorage;
        private readonly ITokenBuilder tokenBuilder;
        private readonly ITokenStorage tokenStorage;
        private readonly IAccessTokenResponseBuilder accessTokenResponseBuilder;

        public CodeRequestProcessor(
            IClientAccessor clientAccessor,
            ICodeStorage codeStorage, 
            ITokenBuilder tokenBuilder, 
            ITokenStorage tokenStorage,
            IAccessTokenResponseBuilder accessTokenResponseBuilder)
        {
            this.clientAccessor = clientAccessor;
            this.codeStorage = codeStorage;
            this.tokenBuilder = tokenBuilder;
            this.tokenStorage = tokenStorage;
            this.accessTokenResponseBuilder = accessTokenResponseBuilder;
        }

        public async Task<IResponse> ProcessToResponseAsync(AccessTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var code = await codeStorage.GetCodeByCodeAndClientIdAsync(request.Code!, request.ClientId!, cancellationToken);

                if (code is null || code?.ValidTillUtc < DateTime.UtcNow)
                {
                    throw new InvalidGrantException(nameof(code));
                }

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
                                              .AddSub(code!.UserId)
                                              .BuildAsync(cancellationToken);

                await tokenStorage.SaveTokenAsync(jti, token, TokenType.AccessToken, exp, cancellationToken);

                accessTokenResponseBuilder.AddAccessToken(token)
                                          .AddTokenType("Bearer")
                                          .AddExpiresIn(exp.ToUnixTimestamp());

                if (client.RequiresRefreshToken)
                {
                    jti = Guid.NewGuid().ToString();
                    var refreshToken = Guid.NewGuid().ToString();
                    exp = now.AddDays(7);
                    await tokenStorage.SaveTokenAsync(jti, refreshToken, TokenType.RefreshToken, exp, cancellationToken);

                    accessTokenResponseBuilder.AddRefreshToken(refreshToken);
                }

                return accessTokenResponseBuilder.Build();
            }
            catch (OAuthException ex)
            {
                return new ErrorResponse(ex);
            }
        }
    }
}
