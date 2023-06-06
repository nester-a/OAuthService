using Microsoft.AspNetCore.Mvc;
using OAuthService.Core.Types;
using OAuthService.Core.Types.Requests;
using OAuthService.Interfaces.Authorization;
using OAuthService.Interfaces.Facroies;
using OAuthService.Interfaces.Validation;
using OAuthService.Web.Attributes;
using OAuthService.Web.Common;

namespace OAuthService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IClientAuthorizationService clientAuthorizationService;
        private readonly IValidationService requestValidationService;
        private readonly IResponseFactory responseFactory;

        public TokenController(IClientAuthorizationService clientAuthorizationService, 
                               IValidationService requestValidationService,
                               IResponseFactory responseFactory)
        {
            this.clientAuthorizationService = clientAuthorizationService;
            this.requestValidationService = requestValidationService;
            this.responseFactory = responseFactory;
        }

        [ClientAuthenticated]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AccessTokenRequest request, CancellationToken cancellationToken = default)
        {
            var client = HttpContext.Items[ItemKey.Client] as Client;

            await clientAuthorizationService.CheckClientIsAuthorizedAsync(client!, request.GrantType, cancellationToken);

            await requestValidationService.ValidateAsync(request, cancellationToken);

            var response = await responseFactory.CreateResponseAsync(client!, request, cancellationToken);

            return Ok(response);
        }
    }
}