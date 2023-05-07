using OAuthService.Core.Base;
using OAuthService.Core.Types.Requests.Obj;
using OAuthService.Core.Exceptions;
using OAuthService.Interfaces;
using OAuthService.Interfaces.Storages;
using OAuthService.Core.Exceptions.Base;
using OAuthService.Core.Types.Responses;

namespace OAuthService.Services.Processors
{
    public class CodeRequestProcessor : IRequestProcessor
    {
        private readonly ICodeStorage codeStorage;

        public IRequest Request { get; set; } = null!;


        public CodeRequestProcessor(ICodeStorage codeStorage)
        {
            this.codeStorage = codeStorage;
        }

        public async Task<IResponse> ProcessToResponseAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                if (Request is CodeRequestObj codeObj)
                {
                    var code = await codeStorage.GetCodeByCodeAndClientIdAsync(codeObj.Code, codeObj.ClientId, cancellationToken);

                    if (code is null || code?.ValidTillUtc < DateTime.UtcNow)
                    {
                        throw new InvalidGrantException(nameof(code));
                    }


                }
                else
                {
                    throw new ServerErrorException($"Incorrect request for {nameof(CodeRequestProcessor)}");
                }
            }
            catch (OAuthException ex)
            {
                return new ErrorResponse(ex);
            }
        }
    }
}
