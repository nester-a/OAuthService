using OAuthService.Interfaces.Validation;

namespace OAuthService.Services
{
    public class PropsValidationService : IPropsValidationService
    {
        public bool AllAreNotNullOrEmpty(params string?[] stringProps)
        {
            return stringProps.All(p => !string.IsNullOrWhiteSpace(p));
        }

        public bool AllAreNullOrEmpty(params string?[] stringProps)
        {
            return stringProps.All(string.IsNullOrWhiteSpace);
        }
    }
}
