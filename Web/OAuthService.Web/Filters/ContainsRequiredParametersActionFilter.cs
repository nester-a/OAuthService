using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OAuthConstans;
using OAuthService.Infrastructure.Factories;
using OAuthService.Interfaces.Validation;
using OAuthService.Web.Models;

namespace OAuthService.Web.Filters
{
    public class ContainsRequiredParametersActionFilter : IAsyncActionFilter
    {
        private readonly IAccessTokenRequestValidationService accessTokenRequestValidationService;
        private readonly ErrorResponseBuilderFactory errorResponseBuilderFactory;

        public ContainsRequiredParametersActionFilter(IAccessTokenRequestValidationService accessTokenRequestValidationService, 
                                                      ErrorResponseBuilderFactory errorResponseBuilderFactory)
        {
            this.accessTokenRequestValidationService = accessTokenRequestValidationService;
            this.errorResponseBuilderFactory = errorResponseBuilderFactory;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cancelation = context.HttpContext.RequestAborted;

            cancelation.ThrowIfCancellationRequested();

            var request = context.ActionArguments["request"] as AccessTokenRequest;

            var valid = await accessTokenRequestValidationService.IsValidAsync(request!, cancelation);

            if (valid)
            {
                await next();
            }
            else
            {
                var builder = errorResponseBuilderFactory.Create();

                var error = builder.AddErrorCode(ErrorResponseErrorCode.InvalidRequest)
                                   .AddErrorDescription("Request contains or missing required parameters")
                                   .Build();

                context.Result = new BadRequestObjectResult(error);
            }
        }
    }
}
