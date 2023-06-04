using System.Net;
using Microsoft.AspNetCore.Http;
using OAuthConstans;
using OAuthService.Core.Exceptions.Base;
using OAuthService.Interfaces;
using OAuthService.Interfaces.Builders;

namespace OAuthService.Middleware
{
    public class OAuthErrorHandleMiddleware
    {
        private readonly RequestDelegate next;

        public OAuthErrorHandleMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IErrorResponseBuilder errorResponseBuilder, IResponsePreparationService responsePreparationService)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (OAuthException ex)
            {
                var error = errorResponseBuilder.FromException(ex)
                                                .Build();

                await responsePreparationService.PrepareAndSendResponse(context.Response,
                                                                        error,
                                                                        HttpStatusCode.BadRequest,
                                                                        context.RequestAborted);
            }
            catch (Exception ex)
            {
                var error = errorResponseBuilder.AddErrorCode(ErrorResponseErrorCode.ServerError)
                                                .AddErrorDescription(ex.Message)
                                                .Build();

                await responsePreparationService.PrepareAndSendResponse(context.Response,
                                                                        error,
                                                                        HttpStatusCode.InternalServerError,
                                                                        context.RequestAborted);
            }
        }
    }
}
