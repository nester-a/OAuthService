using Microsoft.IdentityModel.Tokens;
using OAuthService.Core.Extensions;
using OAuthService.Interfaces.Builders;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OAuthService.Services.Builders
{
    public class TokenBuilder : ITokenBuilder
    {
        string issuer = string.Empty;
        string audience = string.Empty;
        List<Claim> claims = new();
        DateTime expiresIn;
        DateTime notBefore;
        SymmetricSecurityKey ssk = null!;

        public ITokenBuilder SignedWithKey(string key)
        {
            ssk = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            return this;
        }

        public ITokenBuilder AddIss(string iss)
        {
            issuer = iss;
            return this;
        }

        public ITokenBuilder AddSub(string sub)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, sub));
            return this;
        }

        public ITokenBuilder AddAud(string aud)
        {
            audience = aud;
            return this;
        }

        public ITokenBuilder AddExp(DateTime exp)
        {
            expiresIn = exp;
            return this;
        }

        public ITokenBuilder AddNbf(DateTime nbf)
        {
            notBefore = nbf;
            return this;
        }

        public ITokenBuilder AddIat(DateTime iat)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, iat.ToUnixTimestamp().ToString(), ClaimValueTypes.UInteger32));
            return this;
        }

        public ITokenBuilder AddJti(string jti)
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
