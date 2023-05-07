using OAuthService.Core.Types.Requests;

namespace OAuthService.Interfaces.Validation
{
    public interface IValidationService
    {
        Task ValidateAsync(AccessTokenRequest request, CancellationToken cancellation = default);
    }
}
