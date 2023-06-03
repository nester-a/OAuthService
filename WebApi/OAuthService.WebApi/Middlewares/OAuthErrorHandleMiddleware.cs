using Newtonsoft.Json;
using OAuthService.Core.Exceptions.Base;
using OAuthService.Core.Types.Responses;
using OAuthService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            catch (OAuthException ex)
            {
                var error = new ErrorResponse(ex);
                var json = JsonConvert.SerializeObject(error);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(json);
            }
        }
    }
}
