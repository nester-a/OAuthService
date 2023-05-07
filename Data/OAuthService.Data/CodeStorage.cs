using OAuthService.Core.Types;
using OAuthService.Interfaces.Storages;

namespace OAuthService.Data
{
    public class CodeStorage : ICodeStorage
    {
        public async Task<Code> GetCodeByCodeAndClientIdAsync(string code, string clientId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return new Code() { CodeString = code, UserId = "321", ValidTillUtc = DateTime.UtcNow.AddDays(20) };
        }
    }
}
