using OAuthService.Core.Types.Requests;
using OAuthService.Interfaces.Validation;
using OAuthService.Exceptions;

using static OAuthConstans.AccessTokenRequestGrantType;

namespace OAuthService.Services.Validation
{
    public class ValidationService : IValidationService
    {
        private readonly IPropsValidationService propsValidation;

        public ValidationService(IPropsValidationService propsValidation)
        {
            this.propsValidation = propsValidation;
        }

        public async Task ValidateAsync(AccessTokenRequest request, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            try
            {
                switch (request.GrantType)
                {
                    case AuthorizationCode:
                        await ValidateCodeRequestAndBuildObjAsync(request);
                        break;
                    case Password:
                        await ValidatePasswordRequestAndBuildObjAsync(request);
                        break;
                    case ClientCredentials:
                        await ValidateCredentialsRequestAndBuildObjAsync(request);
                        break;
                    case RefreshToken:
                        await ValidateRefreshRequestAndBuildObjAsync(request);
                        break;
                    default:
                        throw new UnsupportedGrantTypeException($"{request.GrantType} is not supported by this service");
                };
            }
            catch
            {
                throw;
            }
        }

        async Task ValidateCodeRequestAndBuildObjAsync(AccessTokenRequest request, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            string?[] shoulBeNotNullOrEmpty = new string?[] { request.Code, request.RedirectUri, request.ClientId };
            string?[] shoulBeNullOrEmpty = new string?[] { request.Username, request.Password, request.Scope, request.RefreshToken };

            try
            {
                await ValidateGrantTypeAsync(shoulBeNotNullOrEmpty, shoulBeNullOrEmpty, cancellation);
            }
            catch
            {
                throw;
            }

        }

        async Task ValidatePasswordRequestAndBuildObjAsync(AccessTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string?[] shoulBeNotNullOrEmpty = new string?[] { request.Username, request.Password };
            string?[] shoulBeNullOrEmpty = new string?[] { request.Code, request.RedirectUri, request.ClientId, request.RefreshToken };

            try
            {
                await ValidateGrantTypeAsync(shoulBeNotNullOrEmpty, shoulBeNullOrEmpty, cancellationToken);
            }
            catch
            {
                throw;
            }
        }

        async Task ValidateCredentialsRequestAndBuildObjAsync(AccessTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string?[] shoulBeNullOrEmpty = new string?[] { request.Username, request.Password, request.Code, request.RedirectUri, request.ClientId, request.RefreshToken };

            try
            {
                await ValidateGrantTypeAsync(Array.Empty<string?>(), shoulBeNullOrEmpty, cancellationToken);
            }
            catch
            {
                throw;
            }
        }

        async Task ValidateRefreshRequestAndBuildObjAsync(AccessTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string?[] shoulBeNotNullOrEmpty = new string?[] { request.RefreshToken };
            string?[] shoulBeNullOrEmpty = new string?[] { request.Username, request.Password, request.Code, request.RedirectUri, request.ClientId };

            try
            {
                await ValidateGrantTypeAsync(shoulBeNotNullOrEmpty, shoulBeNullOrEmpty, cancellationToken);
            }
            catch
            {
                throw;
            }
        }

        async Task ValidateGrantTypeAsync(string?[] shoulBeNotNullOrEmpty, string?[] shouldBeNullOrEmpty, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var areNotNullTask = Task.Factory.StartNew(() => propsValidation.AllAreNotNullOrEmpty(shoulBeNotNullOrEmpty));
            var areNullTask = Task.Factory.StartNew(() => propsValidation.AllAreNullOrEmpty(shouldBeNullOrEmpty));

            await Task.WhenAll(areNotNullTask, areNullTask);

            if (!areNotNullTask.Result || !areNullTask.Result)
            {
                throw new InvalidRequestException("The request is missing a required parameter or has unnecessary parameter");
            }
        }

    }
}
