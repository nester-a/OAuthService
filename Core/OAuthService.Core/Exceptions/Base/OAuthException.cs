namespace OAuthService.Core.Exceptions.Base
{
    public abstract class OAuthException : Exception
    {
        public string Error { get; private set; }

        public string? ErrorDescription { get; private set; }

        public OAuthException(string error, string? errorDescription)
            : base($"{error}-{errorDescription}")
        {
            Error = error;
            ErrorDescription = errorDescription;
        }
    }
}
