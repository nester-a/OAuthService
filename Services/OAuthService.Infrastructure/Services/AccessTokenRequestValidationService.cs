using OAuthService.Exceptions;
using OAuth.Types.Abstraction;
using OAuthService.Interfaces.Validation;

using static OAuthConstans.AccessTokenRequestGrantType;

namespace OAuthService.Infrastructure.Services
{
    public class AccessTokenRequestValidationService : IAccessTokenRequestValidationService
    {
        public async Task ValidateAsync(IAccessTokenRequest request, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            string?[] shoulBeNotNullOrEmpty;
            string?[] shoulBeNullOrEmpty;

            switch (request.GrantType)
            {
                case AuthorizationCode:
                    shoulBeNotNullOrEmpty = new string?[] { request.Code, request.RedirectUri, request.ClientId };
                    shoulBeNullOrEmpty = new string?[] { request.Username, request.Password, request.Scope, request.RefreshToken };
                    break;
                case Password:
                    shoulBeNotNullOrEmpty = new string?[] { request.Username, request.Password };
                    shoulBeNullOrEmpty = new string?[] { request.Code, request.RedirectUri, request.RefreshToken };
                    break;
                case ClientCredentials:
                    shoulBeNotNullOrEmpty = Array.Empty<string?>();
                    shoulBeNullOrEmpty = new string?[] { request.Username, request.Password, request.Code, request.RedirectUri, request.RefreshToken };
                    break;
                case RefreshToken:
                    shoulBeNotNullOrEmpty = new string?[] { request.RefreshToken };
                    shoulBeNullOrEmpty = new string?[] { request.Username, request.Password, request.Code, request.RedirectUri };
                    break;
                default:
                    throw new UnsupportedGrantTypeException(nameof(request.GrantType));
            };

            var areNotNullTask = Task.Factory.StartNew(() => shoulBeNotNullOrEmpty.All(p => !string.IsNullOrEmpty(p)));
            var areNullTask = Task.Factory.StartNew(() => shoulBeNullOrEmpty.All(string.IsNullOrWhiteSpace));

            await Task.WhenAll(areNotNullTask, areNullTask);

            if (!areNotNullTask.Result || !areNullTask.Result)
            {
                throw new InvalidRequestException(nameof(request.GrantType));
            }
        }
    }
}
