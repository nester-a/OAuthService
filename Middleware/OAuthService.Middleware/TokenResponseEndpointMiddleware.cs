using Microsoft.AspNetCore.Http;
using OAuthConstans;
using OAuthService.Core.Base;
using OAuthService.Core.Enums;
using OAuthService.Core.Exceptions;
using OAuthService.Core.Types;
using OAuthService.Core.Types.Responses;
using OAuthService.Interfaces;
using OAuthService.Interfaces.Facroies;
using OAuthService.Interfaces.Storages;
using System.Net;
using System.Threading;

namespace OAuthService.Middleware
{
    public class TokenResponseEndpointMiddleware
    {
        private readonly RequestDelegate next;
        readonly string invalidGrantMessage = "The provided authorization grant (e.g., authorization code, " +
                                "resource owner credentials) or refresh token is invalid, expired, revoked, does not match " +
                                "the redirection URI used in the authorization request, or was issued to another client.";

        public TokenResponseEndpointMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context,
                                      IUserStorage userStorage,
                                      ICodeStorage codeStorage,
                                      ITokenStorage tokenStorage,
                                      IAccessTokenResponseFactory accessTokenResponseFactory,
                                      IResponsePreparationService responsePreparationService)
        {
            if(context.Request.Path == ProtocolEndpoint.Token)
            {
                var form = context.Request.Form;
                var client = context.Items[ItemKey.Client] as Client;
                if (client is null)
                {
                    throw new ServerErrorException("Client not setted");
                }

                AccessTokenResponse response = null!;

                switch (form[AccessTokenRequestParameter.GrantType]) 
                {
                    case AccessTokenRequestGrantType.ClientCredentials:
                        response = await accessTokenResponseFactory.CreateResponseAsync(client, client.Id, TokenSubject.Client, false, context.RequestAborted);
                        break;

                    case AccessTokenRequestGrantType.AuthorizationCode:
                        var userId = await codeStorage.GetUserIdByCodeAndClientIdAsync(form[AccessTokenRequestParameter.Code], client.Id, context.RequestAborted);
                        if(string.IsNullOrWhiteSpace(userId))
                        {
                            throw new InvalidGrantException(invalidGrantMessage);
                        }
                        response = await accessTokenResponseFactory.CreateResponseAsync(client, userId, TokenSubject.User, true, context.RequestAborted);
                        break;

                    case AccessTokenRequestGrantType.Password:
                        var id = await userStorage.GetUserIdByUsernameAndPasswordHashAsync(form[AccessTokenRequestParameter.Username],
                                                                                           form[AccessTokenRequestParameter.Password], 
                                                                                           context.RequestAborted);

                        if (string.IsNullOrWhiteSpace(id))
                        {
                            throw new InvalidGrantException(invalidGrantMessage);
                        }

                        response = await accessTokenResponseFactory.CreateResponseAsync(client, id, TokenSubject.User, true, context.RequestAborted);
                        break;

                    case AccessTokenRequestGrantType.RefreshToken:
                        var subj = await tokenStorage.GetUserIdByValidTokenAsync(form[AccessTokenRequestParameter.RefreshToken], context.RequestAborted);

                        if (string.IsNullOrWhiteSpace(subj))
                        {
                            throw new InvalidGrantException(invalidGrantMessage);
                        }

                        response = await accessTokenResponseFactory.CreateResponseAsync(client, subj, TokenSubject.User, true, context.RequestAborted);
                        break;
                }

                await responsePreparationService.PrepareAndSendResponse(context.Response, response, HttpStatusCode.OK, context.RequestAborted);
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
