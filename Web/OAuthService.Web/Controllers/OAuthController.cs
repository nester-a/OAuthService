using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAuthService.Core.Types.Requests;
using OAuthService.Core.Types;
using OAuthService.Interfaces.Authorization;
using OAuthService.Interfaces.Facroies;
using OAuthService.Interfaces.Validation;
using OAuthService.Web.Attributes;
using OAuthService.Web.Common;

namespace OAuthService.Web.Controllers
{
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private readonly IClientAuthorizationService clientAuthorizationService;
        private readonly IValidationService requestValidationService;
        private readonly IRequestResponseFactory responseFactory;

        public OAuthController(IClientAuthorizationService clientAuthorizationService,
                               IValidationService requestValidationService,
                               IRequestResponseFactory responseFactory)
        {
            this.clientAuthorizationService = clientAuthorizationService;
            this.requestValidationService = requestValidationService;
            this.responseFactory = responseFactory;
        }

        [ClientAuthenticated]
        [HttpPost("/token")]
        public async Task<IActionResult> Token([FromForm] AccessTokenRequest request, CancellationToken cancellationToken = default)
        {
            var client = HttpContext.Items[ItemKey.Client] as Client;

            await clientAuthorizationService.CheckClientIsAuthorizedAsync(client!, request.GrantType, cancellationToken);

            await requestValidationService.ValidateAsync(request, cancellationToken);

            var response = await responseFactory.CreateResponseAsync(client!, request, cancellationToken);

            return Ok(response);
        }

        [ClientAuthenticated]
        [HttpPost("/revoke")]
        public async Task<IActionResult> Revoke()
        {
            return Ok("Revoke");
        }
    }
}
