using Microsoft.AspNetCore.Mvc;
using OAuthService.Web.Attributes;

namespace OAuthService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        [ClientAuthenticated]
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            return Ok("Token");
        }
    }
}