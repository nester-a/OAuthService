using OAuthService.Core.Base;
using OAuthService.Core.Constans;
using OAuthService.Core.Exceptions;
using OAuthService.Core.Types.Requests;
using OAuthService.Core.Types.Requests.Obj;
using OAuthService.Interfaces;

namespace OAuthService.Services
{
    public class RequestFactory : IRequestFactory
    {
        private readonly IPropsValidationService propsValidation;

        public RequestFactory(IPropsValidationService propsValidation)
        {
            this.propsValidation = propsValidation;
        }

        public async Task<IRequest> CreateRequestAsync(AccessTokenRequest request, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            try
            {
                return request.GrandType switch
                {
                    GrantType.AuthorizationCode => await ValidateCodeRequestAndBuildObjAsync(request),
                    GrantType.ResourceOwnerPasswordCredentials => await ValidatePasswordRequestAndBuildObjAsync(request),
                    GrantType.ClientCredentials => await ValidateCredentialsRequestAndBuildObjAsync(request),
                    GrantType.RefreshToken => await ValidateRefreshRequestAndBuildObjAsync(request),
                    _ => throw new UnsupportedGrantTypeException($"{request.GrandType} is not supported by this service")
                };
            }
            catch
            {
                throw;
            }
        }

        async Task<CodeRequestObj> ValidateCodeRequestAndBuildObjAsync(AccessTokenRequest request, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            string?[] shoulBeNotNullOrEmpty = new string?[] { request.Code, request.RedirectUri, request.ClientId };
            string?[] shoulBeNullOrEmpty = new string?[] { request.Username, request.Password, request.Scope, request.RefreshToken };

            try
            {
                await ValidateGrantTypeAsync(shoulBeNotNullOrEmpty, shoulBeNullOrEmpty, cancellation);
                return new CodeRequestObj(request);
            }
            catch
            {
                throw;
            }

        }

        async Task<PasswordRequestObj> ValidatePasswordRequestAndBuildObjAsync(AccessTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string?[] shoulBeNotNullOrEmpty = new string?[] { request.Username, request.Password };
            string?[] shoulBeNullOrEmpty = new string?[] { request.Code, request.RedirectUri, request.ClientId, request.RefreshToken };

            try
            {
                await ValidateGrantTypeAsync(shoulBeNotNullOrEmpty, shoulBeNullOrEmpty, cancellationToken);
                return new PasswordRequestObj(request);
            }
            catch
            {
                throw;
            }
        }

        async Task<CredentialsRequestObj> ValidateCredentialsRequestAndBuildObjAsync(AccessTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string?[] shoulBeNullOrEmpty = new string?[] { request.Username, request.Password, request.Code, request.RedirectUri, request.ClientId, request.RefreshToken };

            try
            {
                await ValidateGrantTypeAsync(Array.Empty<string?>(), shoulBeNullOrEmpty, cancellationToken);
                return new CredentialsRequestObj(request);
            }
            catch
            {
                throw;
            }
        }

        async Task<RefreshingRequestObj> ValidateRefreshRequestAndBuildObjAsync(AccessTokenRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string?[] shoulBeNotNullOrEmpty = new string?[] { request.RefreshToken };
            string?[] shoulBeNullOrEmpty = new string?[] { request.Username, request.Password, request.Code, request.RedirectUri, request.ClientId };

            try
            {
                await ValidateGrantTypeAsync(shoulBeNotNullOrEmpty, shoulBeNullOrEmpty, cancellationToken);
                return new RefreshingRequestObj(request);
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
