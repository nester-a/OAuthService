using System.Net;
using System.Net.Http.Json;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OAuthConstans;
using OAuthService.Core.Exceptions.Base;
using OAuthService.Core.Types.Responses;
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

        public async Task InvokeAsync(HttpContext context, IErrorResponseBuilder errorResponseBuilder)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (OAuthException ex)
            {
                var error = errorResponseBuilder.FromException(ex)
                                                .Build();

                await PrepareAndSendErrorResponse(context.Response,
                                                  error,
                                                  HttpStatusCode.BadRequest,
                                                  context.RequestAborted);
            }
            catch (Exception ex)
            {
                var error = errorResponseBuilder.AddErrorCode(ErrorResponseErrorCode.ServerError)
                                                .AddErrorDescription(ex.Message)
                                                .Build();

                await PrepareAndSendErrorResponse(context.Response,
                                                  error,
                                                  HttpStatusCode.InternalServerError,
                                                  context.RequestAborted);
            }
        }

        private async Task PrepareAndSendErrorResponse(HttpResponse response, ErrorResponse error, HttpStatusCode requiredStatusCode, CancellationToken cancellationToken = default)
        {
            var json = JsonConvert.SerializeObject(error);
            response.StatusCode = (int)requiredStatusCode;
            response.ContentType = MediaTypeNames.Application.Json;
            await response.WriteAsync(json, cancellationToken);
        }
    }
}
