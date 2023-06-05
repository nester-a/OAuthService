using Newtonsoft.Json;
using OAuthService.Exceptions.Base;
using OAuthService.Core.Types.Responses;

namespace OAuthService.WebApi.Middlewares
{
    public class OAuthErrorHandleMiddleware
    {
        private readonly RequestDelegate next;

        public OAuthErrorHandleMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (OAuthErrorException ex)
            {
                var error = new ErrorResponse(ex);
                var json = JsonConvert.SerializeObject(error);
                context.Response.StatusCode = (int)ex.AssociatedStatusCode;
                await context.Response.WriteAsync(json);
            }
        }
    }
}
