using Microsoft.AspNetCore.Mvc;
using OAuthService.Core.Types.Requests;
using OAuthService.Interfaces.Facroies;
using OAuthService.Interfaces.Validation;

namespace OAuthService.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OAuthController : ControllerBase
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
        public async Task<IActionResult> Token([FromForm]AccessTokenRequest accessTokenRequest)
        {
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                var token = cancellationTokenSource.Token;
                await validationService.ValidateAsync(accessTokenRequest, token);

                var response = await responseFactory.CreateResponseAsync(accessTokenRequest, token);

                return response.State switch
                {
                    "success" => Ok(response),
                    "error" => BadRequest(response),
                    _ => NotFound()
                };
            }
        }
    }
}