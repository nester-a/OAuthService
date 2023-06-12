using OAuthConstans;
using OAuthService.Core.Entities;
using OAuthService.Data.Abstraction;


namespace OAuthService.Data
{
    public class ClientStorage : IClientStorage
    {
        public async Task<Client> GetClientByIdAndNullableSecretAsync(string id, string? secret = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return new Client() { Id = "123", Secret = "123", SupportedGrantTypes = new string[] { AccessTokenRequestGrantType.AuthorizationCode, 
                                                                                                   AccessTokenRequestGrantType.Password, 
                                                                                                   AccessTokenRequestGrantType.ClientCredentials, 
                                                                                                   AccessTokenRequestGrantType.RefreshToken 
                                                                                                 } 
            };
        }
    }
}
