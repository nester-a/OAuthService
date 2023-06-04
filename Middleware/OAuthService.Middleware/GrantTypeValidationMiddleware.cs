using Microsoft.AspNetCore.Http;
using OAuthConstans;
using OAuthService.Core.Exceptions;
using OAuthService.Interfaces.Validation;

using static OAuthConstans.AccessTokenRequestParameter;

namespace OAuthService.Middleware
{
    public class GrantTypeValidationMiddleware
    {
        private readonly RequestDelegate next;

        public GrantTypeValidationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IValidationService validationService)
        {
            if(context.Request.Path == ProtocolEndpoint.Token)
            {
                var form = context.Request.Form;
                var grantType = form[GrantType];

                if (string.IsNullOrEmpty(grantType))
                {
                    throw new InvalidRequestException("The request is missing a required parameter or has unnecessary parameter");
                }

                string[] shoulBeNotNullOrEmpty;
                string[] shoulBeNullOrEmpty;

                switch (grantType)
                {
                    case AccessTokenRequestGrantType.AuthorizationCode:
                        shoulBeNotNullOrEmpty = new string[] { form[Code], form[RedirectUri], form[ClientId] };
                        shoulBeNullOrEmpty = new string[] { form[Username], form[Password], form[Scope], form[RefreshToken] };
                        break;
                    case AccessTokenRequestGrantType.Password:
                        shoulBeNotNullOrEmpty = new string[] { form[Username], form[Password] };
                        shoulBeNullOrEmpty = new string[] { form[Code], form[RedirectUri], form[RefreshToken] };
                        break;
                    case AccessTokenRequestGrantType.ClientCredentials:
                        shoulBeNotNullOrEmpty = Array.Empty<string>();
                        shoulBeNullOrEmpty = new string[] { form[Username], form[Password], form[Scope], form[RefreshToken] };
                        break;
                    case AccessTokenRequestGrantType.RefreshToken:
                        shoulBeNotNullOrEmpty = new string[] { form[RefreshToken] };
                        shoulBeNullOrEmpty = new string[] { form[Username], form[Password], form[Scope], form[Code], form[RedirectUri] };
                        break;
                    default:
                        throw new UnsupportedGrantTypeException("The authorization grant type is not supported by the authorization server.");
                }

                await ValidateGrantTypeAsync(shoulBeNotNullOrEmpty, shoulBeNullOrEmpty, context.RequestAborted);
            }

            await next.Invoke(context);

        }

        private async Task ValidateGrantTypeAsync(string?[] shoulBeNotNullOrEmpty, string?[] shouldBeNullOrEmpty, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var areNotNullTask = Task.Factory.StartNew(() => shoulBeNotNullOrEmpty.Length != 0 ? 
                                                    shoulBeNotNullOrEmpty.All(p => !string.IsNullOrWhiteSpace(p)) : true, cancellationToken);
            var areNullTask = Task.Factory.StartNew(() => shouldBeNullOrEmpty.Length != 0 ? 
                                                    shouldBeNullOrEmpty.All(string.IsNullOrWhiteSpace) : true, cancellationToken);

            await Task.WhenAll(areNotNullTask, areNullTask);

            if (!areNotNullTask.Result || !areNullTask.Result)
            {
                throw new InvalidRequestException("The request is missing a required parameter or has unnecessary parameter");
            }
        }

    }
}
