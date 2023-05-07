namespace OAuthService.Core.Types
{
    public class Code
    {
        public string CodeString { get; set; } = string.Empty;

        public DateTime ValidTillUtc { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}
