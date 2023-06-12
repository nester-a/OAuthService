using Microsoft.AspNetCore.Mvc;
using OAuthService.Interfaces.Validation;
using OAuthService.Web.Attributes;
using OAuthService.Web.Common;
using OAuthService.Core.Entities;
using OAuthService.Infrastructure.Abstraction;
using OAuthService.Types;
using OAuthService.Data.Abstraction;
using OAuthService.Core.Enums;
using OAuthConstans;

namespace OAuthService.Web.Controllers
{

    [ClientAuthenticated]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private readonly IClientAuthorizationService clientAuthorizationService;
        private readonly IAccessTokenRequestValidationService accessTokenRequestValidationService;
        private readonly IAccessTokenResponseFactory accessTokenResponseFactory;
        private readonly ITokenStorage tokenStorage;

        public OAuthController(IClientAuthorizationService clientAuthorizationService,
                               IAccessTokenRequestValidationService accessTokenRequestValidationService,
                               IAccessTokenResponseFactory accessTokenResponseFactory,
                               ITokenStorage tokenStorage)
        {
            this.clientAuthorizationService = clientAuthorizationService;
            this.accessTokenRequestValidationService = accessTokenRequestValidationService;
            this.accessTokenResponseFactory = accessTokenResponseFactory;
            this.tokenStorage = tokenStorage;
        }

        [HttpPost("/token")]
        public async Task<IActionResult> Token([FromForm] AccessTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var client = HttpContext.Items[ItemKey.Client] as Client;

            await clientAuthorizationService.CheckClientIsAuthorizedAsync(client!, request.GrantType, cancellationToken);

            await accessTokenRequestValidationService.ValidateAsync(request, cancellationToken);

            var response = await accessTokenResponseFactory.CreateAsync(client!, request, cancellationToken);

            return Ok(response);
        }

        [HttpPost("/revoke")]
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
