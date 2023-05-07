using Microsoft.AspNetCore.Mvc;
using OAuthService.Core.Types.Requests;
using OAuthService.Interfaces;

namespace OAuthService.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OAuthController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly IRequestFactory requestFactory;
        private readonly ILogger<OAuthController> _logger;

        public OAuthController(IRequestFactory requestFactory, ILogger<OAuthController> logger)
        {
            this.requestFactory = requestFactory;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Token([FromForm]AccessTokenRequest accessTokenRequest)
        {
            var cancellationToken = new CancellationTokenSource().Token;
            var request = await requestFactory.CreateRequestAsync(accessTokenRequest, cancellationToken);

            var response = await request.Processor.ProcessToResponseAsync(cancellationToken);

            return Ok();
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}