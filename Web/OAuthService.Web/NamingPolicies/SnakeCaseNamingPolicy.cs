using OAuthService.Web.Common;
using System.Text.Json;

namespace OAuthService.Web.NamingPolicies
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return name.ToSnakeCase();
        }
    }
}