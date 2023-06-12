using OAuthService.Data.Abstraction;
using System.Threading;

namespace OAuthService.Data
{
    public class CodeStorage : ICodeStorage
    {
        public async Task<string> GetUserIdByCodeAndClientIdAsync(string code, string clientId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return "123";
        }

        public async Task SaveCodeAsync(string code, DateTime valiTillUtc, string userId, string clientId, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();
        }
    }
}
