namespace OAuthService.Core.Types.Requests
{
    public class UserAuthorizationRequest
    {
        public UserAuthorizationRequest(AuthorizationRequest request)
        {
            Request = request;
        }

        public AuthorizationRequest Request { get; }

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
