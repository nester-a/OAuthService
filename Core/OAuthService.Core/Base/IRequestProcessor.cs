namespace OAuthService.Core.Base
{
    public interface IRequestProcessor
    {
        IRequest Request { get; set; }
        Task<IResponse> ProcessToResponseAsync(CancellationToken cancellationToken = default);
    }
}
