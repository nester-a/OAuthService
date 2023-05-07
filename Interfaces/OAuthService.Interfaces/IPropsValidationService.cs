namespace OAuthService.Interfaces
{
    public interface IPropsValidationService
    {
        bool AllAreNullOrEmpty(params string?[] stringProps);
        bool AllAreNotNullOrEmpty(params string?[] stringProps);
    }
}
