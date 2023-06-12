using Microsoft.IdentityModel.Tokens;
using OAuthService.Infrastructure.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OAuthService.Infrastructure.Builders
{
    public class JwtBuilder
    {
        string issuer = string.Empty;
        string audience = string.Empty;
        List<Claim> claims = new();
        DateTime expiresIn;
        DateTime notBefore;
        SymmetricSecurityKey ssk = null!;

        public JwtBuilder SignedWithKey(string key)
        {
            ssk = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            return this;
        }

        public JwtBuilder AddIss(string iss)
        {
            issuer = iss;
            return this;
        }

        public JwtBuilder AddSub(string sub)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, sub));
            return this;
        }

        public JwtBuilder AddAud(string aud)
        {
            audience = aud;
            return this;
        }

        public JwtBuilder AddExp(DateTime exp)
        {
            expiresIn = exp;
            return this;
        }

        public JwtBuilder AddNbf(DateTime nbf)
        {
            notBefore = nbf;
            return this;
        }

        public JwtBuilder AddIat(DateTime iat)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, iat.ToUnixTimestamp().ToString(), ClaimValueTypes.UInteger32));
            return this;
        }

        public JwtBuilder AddJti(string jti)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, jti));
            return this;
        }

        public string Build()
        {
            var jwt = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: notBefore,
                expires: expiresIn,
                signingCredentials: new SigningCredentials(ssk, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public async Task<string> BuildAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Factory.StartNew(Build, cancellationToken);
        }
    }
}
