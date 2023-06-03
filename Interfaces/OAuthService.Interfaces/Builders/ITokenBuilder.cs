using OAuthService.Interfaces.Builders.Base;

namespace OAuthService.Interfaces.Builders
{
    public interface ITokenBuilder : IBaseBuilder<string>
    {
        ITokenBuilder SignedWithKey(string key);

        ITokenBuilder AddIss(string iss);

        ITokenBuilder AddSub(string sub);

        ITokenBuilder AddAud(string aud);

        ITokenBuilder AddExp(DateTime exp);

        ITokenBuilder AddNbf(DateTime nbf);

        ITokenBuilder AddIat(DateTime iat);

        ITokenBuilder AddJti(string jti);

        Task<string> BuildAsync(CancellationToken cancellationToken = default);
    }
}
