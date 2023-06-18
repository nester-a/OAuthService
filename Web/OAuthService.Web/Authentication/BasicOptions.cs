using Microsoft.AspNetCore.Authentication;

namespace OAuthService.Web.Authentication
{
    public class BasicOptions : AuthenticationSchemeOptions
    {
        public char Separator { get; set; } = ':';
    }
}