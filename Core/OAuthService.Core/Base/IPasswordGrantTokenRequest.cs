namespace OAuthService.Core.Base
{
    public interface IPasswordGrantTokenRequest : IRequest
    {
        string? Username { get; }
        string? Password { get; }
    }
}
