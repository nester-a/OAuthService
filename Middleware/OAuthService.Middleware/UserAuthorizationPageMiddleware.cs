using Microsoft.AspNetCore.Http;
using OAuthService.Core.Exceptions;
using OAuthService.Middleware.Options;
using System.Net;

namespace OAuthService.Middleware
{
    public class UserAuthorizationPageMiddleware
    {
        private readonly RequestDelegate next;
        private readonly UserAuthorizationPageOptions opt;

        public UserAuthorizationPageMiddleware(RequestDelegate next, UserAuthorizationPageOptions opt)
        {
            this.next = next;
            this.opt = opt;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == opt.uriPath)
            {
                switch (context.Request.Method)
                {
                    case WebRequestMethods.Http.Get:
                        await context.Response.SendFileAsync(opt.pageFilePath, context.RequestAborted);
                        break;
                   case WebRequestMethods.Http.Post:
                        //<- здесь будет авторизация пользователя и выдача кода
                        break;
                    default:
                        throw new ServerErrorException("Unsupported method for this endpoint");

                }
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
