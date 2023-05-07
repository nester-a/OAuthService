namespace OAuthService.Interfaces.Validation
{
    public interface IPropsValidationService
    {
        bool AllAreNullOrEmpty(params string?[] stringProps);
        bool AllAreNotNullOrEmpty(params string?[] stringProps);
    }
}
