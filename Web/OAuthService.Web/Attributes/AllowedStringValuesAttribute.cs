using System.ComponentModel.DataAnnotations;

namespace OAuthService.Web.Attributes
{
    public class AllowedStringValuesAttribute : ValidationAttribute
    {
        private readonly string[] values;

        public AllowedStringValuesAttribute(params string[] values)
        {
            this.values = values;
        }
        public override bool IsValid(object? value)
        {
            if(value is string str)
            {
                return values.Contains(str);
            }
            return false;
        }
    }
}