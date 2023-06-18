using Microsoft.AspNetCore.Mvc;
using OAuth.Types.Abstraction;
using OAuthConstans;

namespace OAuthService.Web.Models
{
    public class RevocationRequest : IRevocationRequest
    {
        [FromForm(Name = RevocationRequestParameter.Token)]
        public string Token { get; set; } = string.Empty;

        [FromForm(Name = RevocationRequestParameter.TokenTypeHint)]
        public string? TokenTypeHint { get; set; }
    }
}
