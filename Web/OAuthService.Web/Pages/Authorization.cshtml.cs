using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OAuthConstans;
using OAuthService.Web.Attributes;
using OAuthService.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace OAuthService.Web.Pages
{
    public class AuthorizationModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; } = new();

        public async Task<IActionResult> OnGet([FromQuery] AuthorizationRequest request)
        {
            Input.ClientId = request.ClientId;
            Input.ResponseType = request.ResponseType;
            Input.RedirectUri = request.RedirectUri;
            Input.Scope = request.Scope;
            Input.State = request.State;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            switch (Input.ResponseType)
            {
                case AuthorizationRequestResponseType.Token:
                    return Redirect(Input.RedirectUri);
                case AuthorizationRequestResponseType.Code:
                    return Redirect(Input.RedirectUri);
                default:
                    break;
            }

            return Page();
        }

        public class InputModel
        {
            [Required]
            public string Username { get; set; } = string.Empty;

            [Required]
            public string Password { get; set; } = string.Empty;

            [AllowedStringValues(AuthorizationRequestResponseType.Token, AuthorizationRequestResponseType.Code, ErrorMessage = "Not valid response_type value")]
            public string ResponseType { get; set; } = string.Empty;

            [Required(ErrorMessage = "Client_id parameter is required")]
            public string ClientId { get; set; } = string.Empty;

            public string? RedirectUri { get; set; }
            public string? Scope { get; set; }
            public string? State { get; set; }
        }
    }
}
