using Microsoft.AspNetCore.Mvc;
using OAuthService.Interfaces.Validation;
using OAuthService.Web.Attributes;
using OAuthService.Web.Common;
using OAuthService.Core.Entities;
using OAuthService.Infrastructure.Abstraction;
using OAuthService.Types;

namespace OAuthService.Web.Controllers
{

    [ClientAuthenticated]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private readonly IClientAuthorizationService clientAuthorizationService;
        private readonly IAccessTokenRequestValidationService accessTokenRequestValidationService;
        private readonly IAccessTokenResponseFactory accessTokenResponseFactory;

        public OAuthController(IClientAuthorizationService clientAuthorizationService,
                               IAccessTokenRequestValidationService accessTokenRequestValidationService,
                               IAccessTokenResponseFactory accessTokenResponseFactory)
        {
            this.clientAuthorizationService = clientAuthorizationService;
            this.accessTokenRequestValidationService = accessTokenRequestValidationService;
            this.accessTokenResponseFactory = accessTokenResponseFactory;
        }

        [HttpPost("/token")]
        public async Task<IActionResult> Token([FromForm] AccessTokenRequest request, CancellationToken cancellationToken = default)
        {
            var client = HttpContext.Items[ItemKey.Client] as Client;

            await clientAuthorizationService.CheckClientIsAuthorizedAsync(client!, request.GrantType, cancellationToken);

            await accessTokenRequestValidationService.ValidateAsync(request, cancellationToken);

            var response = await accessTokenResponseFactory.CreateAsync(client!, request, cancellationToken);

            return Ok(response);
        }

        [HttpPost("/revoke")]
        public async Task<IActionResult> Revoke()
        {
            return Ok("Revoke");
        }
    }
}
