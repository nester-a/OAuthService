namespace OAuthService.Core.Base
{
    public interface IRefreshingAccessTokenRequest : IRequest
    {
        string? RefreshToken { get; }
        string? Scope { get; }
    }
}
