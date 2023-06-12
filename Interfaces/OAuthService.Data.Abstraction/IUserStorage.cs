namespace OAuthService.Data.Abstraction
{
    public interface IUserStorage
    {
        Task<string> GetUserIdByUsernameAndPasswordHashAsync(string username, string passwordHash, CancellationToken cancellationToken = default);
    }
}
