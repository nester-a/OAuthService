namespace OAuthService.Interfaces.Builders
{
    public interface ITokenBuilder
    {
        ITokenBuilder SignedWithKey(string key);

        ITokenBuilder AddIss(string iss);

        ITokenBuilder AddSub(string sub);

        ITokenBuilder AddAud(string aud);

        ITokenBuilder AddExp(DateTime exp);

        ITokenBuilder AddNbf(DateTime nbf);

        ITokenBuilder AddIat(DateTime iat);

        ITokenBuilder AddJti(string jti);

        string Build();

        Task<string> BuildAsync(CancellationToken cancellationToken = default);
    }
}
