using Microsoft.AspNetCore.Mvc;

namespace OAuthService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RevokeController : ControllerBase
    {
        
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            return Ok("Revoke");
        }
    }
}