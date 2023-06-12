﻿using Microsoft.Net.Http.Headers;
using OAuthConstans;
using OAuthService.Data.Abstraction;
using OAuthService.Exceptions;
using OAuthService.Web.Common;

namespace OAuthService.Web.Middlewares
{
    public class ClientAuthenticationMiddleware
    {
        private readonly RequestDelegate next;

        public ClientAuthenticationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IClientStorage clientStorage)
        {
            var request = context.Request;
            var headers = request.Headers;

            var basics = headers[HeaderNames.Authorization].ToString();

            (string? clientId, string? clientSecret) clientData;

            if (!string.IsNullOrWhiteSpace(basics))
            {
                clientData = await GetClientDataFromBasics(basics, context.RequestAborted);
            }
            else
            {
                var form = request.Form;
                clientData = (form[AccessTokenRequestParameter.ClientId], form[AccessTokenRequestParameter.ClientSecret]);
            }

            if(!string.IsNullOrEmpty(clientData.clientId))
            {
                var client = await clientStorage.GetClientByIdAndNullableSecretAsync(clientData.clientId, clientData.clientSecret, context.RequestAborted);

                if (client is not null)
                {
                    context.Items.Add(ItemKey.Client, client);
                }
            }

            await next.Invoke(context);
        }

        private static async Task<(string?, string?)> GetClientDataFromBasics(string basicsString, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Task.Factory.StartNew(() =>
            {
                var encodedBytes = Convert.FromBase64String(basicsString.Split(' ')[1]);

                //clientData: clientId:clientSecret
                var data = System.Text.Encoding.UTF8.GetString(encodedBytes);

                if (string.IsNullOrWhiteSpace(data))
                {
                    throw new InvalidClientException("Client authentication parameters not found");
                }

                var splitedData = data.Split(':');

                return (splitedData[0], splitedData[1]);
            }, cancellationToken);
        }
    }
}
