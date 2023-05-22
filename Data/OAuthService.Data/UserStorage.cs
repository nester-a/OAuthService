using OAuthService.Interfaces.Storages;

namespace OAuthService.Data
{
    public class UserStorage : IUserStorage
    {
        public async Task<string> GetUserIdByUsernameAndPasswordHashAsync(string username, string passwordHash, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return "123";
        }
    }
}
