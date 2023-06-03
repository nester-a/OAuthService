using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OAuthConstans;
using OAuthService.Core.Exceptions.Base;
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
                var response = errorResponseBuilder.FromException(ex)
                                                   .Build();

                var json = JsonConvert.SerializeObject(response);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                var response = errorResponseBuilder.AddErrorCode(ErrorResponseErrorCode.ServerError)
                                                   .AddErrorDescription(ex.Message)
                                                   .Build();

                var json = JsonConvert.SerializeObject(response);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(json);
            }
        }
    }
}
