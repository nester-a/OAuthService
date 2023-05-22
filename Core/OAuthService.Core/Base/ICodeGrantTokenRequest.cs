namespace OAuthService.Core.Base
{
    public interface ICodeGrantTokenRequest : IRequest
    {
        string? Code { get; }
        string? RedirectUri { get; }
        string? ClientId { get; }
    }
}
