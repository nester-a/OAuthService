using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace OAuthService.Web.Authentication
{
    public class BasicHandler : AuthenticationHandler<BasicOptions>
    {
        public BasicHandler(IOptionsMonitor<BasicOptions> options,
                            ILoggerFactory logger,
                            UrlEncoder encoder,
                            ISystemClock clock) : base(options, logger, encoder, clock) { }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string? basic = string.Empty;
            var authorization = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authorization))
            {
                return await Task.FromResult(AuthenticateResult.NoResult());
            }

            if (authorization.StartsWith($"{BasicDefaults.AuthenticationScheme} ", StringComparison.OrdinalIgnoreCase))
            {
                basic = authorization.Substring($"{BasicDefaults.AuthenticationScheme} ".Length).Trim();
            }

            if (string.IsNullOrWhiteSpace(basic))
            {
                return await Task.FromResult(AuthenticateResult.NoResult());
            }

            byte[]? base64EncodedBytes;
            try
            {
                base64EncodedBytes = Convert.FromBase64String(basic);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(AuthenticateResult.Fail(ex));
            }

            var decodedBase64 = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            var basicAsArray = basic.Split(Options.Separator);

            if (basicAsArray.Length != 2)
            {
                return await Task.FromResult(AuthenticateResult.Fail("Incorrect basic authorization header"));
            }

            var identity = new ClaimsIdentity(nameof(BasicHandler));

            Claim[] claims;
            if (Options.GetPrincipalClaimsFunc is not null)
            {
                claims = Options.GetPrincipalClaimsFunc(basicAsArray[0], basicAsArray[1]);
            }
            else
            {
                claims = new Claim[] { new Claim(ClaimTypes.Name, basic) };
            }

            identity.AddClaims(claims);

            var principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
