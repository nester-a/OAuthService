using OAuthService.Core.Types;
using OAuthService.Interfaces.Storages;

namespace OAuthService.Data
{
    public class CodeStorage : ICodeStorage
    {
        public async Task<string> GetUserIdByCodeAndClientIdAsync(string code, string clientId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return "123";
        }
    }
}
