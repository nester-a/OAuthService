using Microsoft.AspNetCore.Mvc;
using OAuthService.Web.Attributes;

namespace OAuthService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RevokeController : ControllerBase
    {
        [ClientAuthenticated]
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            return Ok("Revoke");
        }
    }
}