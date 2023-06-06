using System.Net;
using OAuthConstans;
using OAuthService.Exceptions.Base;
using OAuthService.Interfaces;
using OAuthService.Interfaces.Builders;

namespace OAuthService.Web.Middlewares
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
            catch (OAuthErrorException ex)
            {
                var error = errorResponseBuilder.FromException(ex)
                                                .Build();

                await responsePreparationService.PrepareAndSendResponse(context.Response,
                                                                        error,
                                                                        ex.AssociatedStatusCode,
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
