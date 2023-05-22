namespace OAuthService.Interfaces.Storages
{
    public interface IUserStorage
    {
        Task<string> GetUserIdByUsernameAndPasswordHashAsync(string username, string passwordHash, CancellationToken cancellationToken = default);
    }
}
