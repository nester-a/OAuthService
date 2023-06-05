using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using OAuthService.Exceptions;
using OAuthService.Interfaces;
using OAuthService.Interfaces.Accessors;

namespace OAuthService.Services
{
    public class ClientAuthenticationService : IClientAuthenticationService
    {
        private readonly IClientAccessor clientAccessor;

        public ClientAuthenticationService(IClientAccessor clientAccessor)
        {
            this.clientAccessor = clientAccessor;
        }
        public async Task AuthenticateClientByAuthorizationHeaderAsync(IHeaderDictionary headers, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            //header: Basic *BASE64-encoded data*
            var basics = headers[HeaderNames.Authorization].ToString();

            if(string.IsNullOrWhiteSpace(basics))
            {
                throw new InvalidRequestException("Client authentication parameters not found");
            }

            var encodedBytes = Convert.FromBase64String(basics.Split(' ')[1]);

            //clientData: clientId:clientSecret
            var clientData = System.Text.Encoding.UTF8.GetString(encodedBytes);

            if (string.IsNullOrWhiteSpace(clientData))
            {
                throw new InvalidRequestException("Client authentication parameters not found");
            }

            var splitedData = clientData.Split(':');

            var authCompleted = await clientAccessor.TrySetClientByIdAndSecretAsync(splitedData[0], splitedData[1], cancellation);

            if (!authCompleted)
            {
                throw new InvalidClientException("Client authentication failed");
            }

        }

        public async Task AuthenticateClientByIdAsync(string clientID, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            var authCompleted = await clientAccessor.TrySetClientByIdAsync(clientID, cancellation);

            if (!authCompleted)
            {
                throw new InvalidClientException("Client authentication failed");
            }
        }
    }
}
