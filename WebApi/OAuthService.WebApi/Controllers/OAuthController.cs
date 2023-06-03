using Microsoft.AspNetCore.Mvc;
using OAuthService.Core.Types.Requests;
using OAuthService.Interfaces.Facroies;
using OAuthService.Interfaces.Validation;

namespace OAuthService.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OAuthController : Controller
    {
        private readonly IValidationService validationService;
        private readonly IResponseFactory responseFactory;
        private readonly ILogger<OAuthController> _logger;

        public OAuthController(IValidationService validationService, IResponseFactory responseFactory, ILogger<OAuthController> logger)
        {
            this.validationService = validationService;
            this.responseFactory = responseFactory;
            _logger = logger;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Token([FromForm]AccessTokenRequest accessTokenRequest, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await validationService.ValidateAsync(accessTokenRequest, default);

            var response = await responseFactory.CreateResponseAsync(accessTokenRequest, default);

            return response.State switch
            {
                "success" => Ok(response),
                "error" => BadRequest(response),
                _ => NotFound()
            };
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Authorization([FromQuery] AuthorizationRequest authorizationRequest)
        {
            var model = new UserAuthorizationRequest(authorizationRequest);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Authorization([FromBody] UserAuthorizationRequest userAuthorizationRequest)
        {
            return Ok();
        }
    }
}