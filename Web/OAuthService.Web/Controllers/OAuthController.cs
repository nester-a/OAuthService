using Microsoft.AspNetCore.Mvc;
using OAuthService.Web.Attributes;
using OAuthService.Web.Common;
using OAuthService.Core.Entities;
using OAuthService.Infrastructure.Abstraction;
using OAuthService.Data.Abstraction;
using OAuthService.Core.Enums;
using OAuthConstans;
using OAuthService.Web.Models;
using Microsoft.AspNetCore.Authorization;
using OAuthService.Web.Authentication;
using OAuthService.Exceptions;

namespace OAuthService.Web.Controllers
{
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private readonly IClientAuthorizationService clientAuthorizationService;
        private readonly IAccessTokenResponseFactory accessTokenResponseFactory;
        private readonly ITokenStorage tokenStorage;

        public OAuthController(IClientAuthorizationService clientAuthorizationService,
                               IAccessTokenResponseFactory accessTokenResponseFactory,
                               ITokenStorage tokenStorage)
        {
            this.clientAuthorizationService = clientAuthorizationService;
            this.accessTokenResponseFactory = accessTokenResponseFactory;
            this.tokenStorage = tokenStorage;
        }

        [HttpPost("/token")]
        [Authorize(AuthenticationSchemes = BasicDefaults.AuthenticationScheme, Policy = "ClientAuthorized")]
        [ContainsRequiredParameters]
        public async Task<IActionResult> Token([FromForm] AccessTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var client = HttpContext.Items[ItemKey.Client] as Client;

            await clientAuthorizationService.CheckClientIsAuthorizedAsync(client!, request.GrantType, cancellationToken);

            var response = request.GrantType switch
            {
                AccessTokenRequestGrantType.RefreshToken => await accessTokenResponseFactory.CreateForRefreshTokenAsync(request.RefreshToken!, 
                                                                                                                        client!.Id,
                                                                                                                        client!.TokenKey, 
                                                                                                                        cancellationToken),
                AccessTokenRequestGrantType.AuthorizationCode => await accessTokenResponseFactory.CreateForAuthorizationCodeAsync(request.Code!, 
                                                                                                                                  client!.Id,
                                                                                                                                  client!.TokenKey,
                                                                                                                                  cancellationToken),
                AccessTokenRequestGrantType.Password => await accessTokenResponseFactory.CreateForPasswordAsync(request.Username!,
                                                                                                                request.Password!, 
                                                                                                                client!.Id,
                                                                                                                client!.TokenKey,
                                                                                                                cancellationToken),
                AccessTokenRequestGrantType.ClientCredentials => await accessTokenResponseFactory.CreateForClientCredentialsAsync(client!.Id,
                                                                                                                                  client!.TokenKey,
                                                                                                                                  cancellationToken),
                _ => throw new UnsupportedGrantTypeException()
            };

            return Ok(response);
        }

        [HttpPost("/revoke")]
        [Authorize(AuthenticationSchemes = BasicDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Revoke([FromForm] RevocationRequest request, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            switch (request.TokenTypeHint)
            {
                case AccessTokenResponseParameter.AccessToken:
                    await tokenStorage.RevokeTokenAsync(request.Token, TokenType.AccessToken, cancellation);
                    break;
                case AccessTokenResponseParameter.RefreshToken:
                    await tokenStorage.RevokeTokenAsync(request.Token, TokenType.RefreshToken, cancellation);
                    break;
                default:
                    await tokenStorage.RevokeTokenAsync(request.Token, TokenType.None, cancellation);
                    break;
            }

            return Ok();
        }
    }
}
