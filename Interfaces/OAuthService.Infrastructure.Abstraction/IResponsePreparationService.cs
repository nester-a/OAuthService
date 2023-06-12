using Microsoft.AspNetCore.Http;
using System.Net;

namespace OAuthService.Infrastructure.Abstraction
{
    public interface IResponsePreparationService
    {
        Task PrepareAndSendResponse(HttpResponse response, object responseObj, HttpStatusCode requiredStatusCode, CancellationToken cancellationToken = default);
    }
}
