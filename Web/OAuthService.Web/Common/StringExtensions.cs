using System.Text.RegularExpressions;

namespace OAuthService.Web.Common
{
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string value)
        {
            return Regex.Replace(value, "([a-z])([A-Z])", $"$1_$2")
                        .ToLower();
        }
    }
}