using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OAuthService.Interfaces;
using System.Net;
using System.Net.Mime;

namespace OAuthService.Services
{
    public class ResponsePreparationService : IResponsePreparationService
    {
        public async Task PrepareAndSendResponse(HttpResponse response, object responseObj, HttpStatusCode requiredStatusCode, CancellationToken cancellationToken = default)
        {
            var json = JsonConvert.SerializeObject(responseObj);
            response.StatusCode = (int)requiredStatusCode;
            response.ContentType = MediaTypeNames.Application.Json;
            await response.WriteAsync(json, cancellationToken);
        }
    }
}
