using Microsoft.AspNetCore.Mvc;

namespace OAuthService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            return Ok("Token");
        }
    }
}