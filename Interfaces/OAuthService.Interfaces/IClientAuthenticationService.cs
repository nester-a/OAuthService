using Microsoft.AspNetCore.Http;

namespace OAuthService.Interfaces
{
    public interface IClientAuthenticationService
    {
        Task AuthenticateClientByAuthorizationHeaderAsync(IHeaderDictionary headers, CancellationToken cancellation = default);
        Task AuthenticateClientByIdAsync(string clientID, CancellationToken cancellation = default);
    }
}
