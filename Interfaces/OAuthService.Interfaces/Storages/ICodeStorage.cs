using OAuthService.Core.Types;

namespace OAuthService.Interfaces.Storages
{
    public interface ICodeStorage
    {
        Task<Code> GetCodeByCodeAndClientIdAsync(string code, string clientId, CancellationToken cancellationToken = default);
    }
}
