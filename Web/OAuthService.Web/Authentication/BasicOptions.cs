using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace OAuthService.Web.Authentication
{
    public class BasicOptions : AuthenticationSchemeOptions
    {
        public char Separator { get; set; } = ':';

        public Func<string, string, Claim[]>? GetPrincipalClaimsFunc { get; set; }
    }
}