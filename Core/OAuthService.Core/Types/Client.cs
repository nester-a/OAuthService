namespace OAuthService.Core.Types
{
    public class Client
    {
        public string Id { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
        public string TokenKey { get; set; } = Guid.NewGuid().ToString();
        public string[] SupportedGrantTypes { get; set; } = Array.Empty<string>();
    }
}
