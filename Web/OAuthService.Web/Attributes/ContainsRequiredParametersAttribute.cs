using Microsoft.AspNetCore.Mvc;
using OAuthService.Web.Filters;

namespace OAuthService.Web.Attributes
{
    public class ContainsRequiredParametersAttribute : TypeFilterAttribute
    {
        public ContainsRequiredParametersAttribute() 
            : base(typeof(ContainsRequiredParametersActionFilter)) { }
    }
}
