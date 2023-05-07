using Microsoft.AspNetCore.Mvc;
using OAuthService.Core.Base;
using OAuthService.Core.Types.Requests;
using OAuthService.Interfaces;

namespace OAuthService.MVC.Controllers
{
    public class OAuthController : Controller
    {
        private readonly IRequestFactory requestFactory;

        public OAuthController(IRequestFactory requestFactory)
        {
            this.requestFactory = requestFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Token(AccessTokenRequest accessTokenRequest)
        {
            IRequest request = await requestFactory.CreateRequestAsync(accessTokenRequest);

            return View();
        }
    }
}
