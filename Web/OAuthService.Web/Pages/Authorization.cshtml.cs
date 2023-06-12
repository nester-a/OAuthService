using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OAuthService.Types;
using System.ComponentModel.DataAnnotations;

namespace OAuthService.Web.Pages
{
    public class AuthorizationModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; } = new();

        public void OnGet([FromQuery] AuthorizationRequest request)
        {
            Input.ClientId = request.ClientId;
            Input.ResponseType = request.ResponseType;
            Input.RedirectUri = request.RedirectUri;
            Input.Scope = request.Scope;
            Input.State = request.State;
        }

        public void OnPost()
        {
            Input.RedirectUri = "123";
        }

        public class InputModel
        {
            [Required]
            public string Username { get; set; } = string.Empty;

            [Required]
            public string Password { get; set; } = string.Empty;

            [Required]
            public string ResponseType { get; set; } = string.Empty;

            [Required]
            public string ClientId { get; set; } = string.Empty;

            public string? RedirectUri { get; set; }
            public string? Scope { get; set; }
            public string? State { get; set; }
        }
    }
}
