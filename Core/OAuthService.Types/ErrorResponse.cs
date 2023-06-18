using OAuth.Types.Abstraction;

namespace OAuthService.Infrastructure.Types
{
    public class ErrorResponse : IErrorResponse
    {
        public string Error { get; set; } = string.Empty;

        public string? ErrorDescription { get; set; }

        public string? ErrorUri { get; set; }

        public string? State { get; set; }
    }
}
