using Microsoft.AspNetCore.Http;
using System.Net;

namespace OAuthService.Interfaces
{
    public interface IResponsePreparationService
    {
        Task PrepareAndSendResponse(HttpResponse response, object responseObj, HttpStatusCode requiredStatusCode, CancellationToken cancellationToken = default);
    }
}
