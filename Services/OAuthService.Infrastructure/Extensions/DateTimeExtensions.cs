namespace OAuthService.Infrastructure.Extensions
{
    internal static class DateTimeExtensions
    {
        internal static uint ToUnixTimestamp(this DateTime dateTime)
        {
            return (uint)dateTime.Subtract(DateTime.UnixEpoch).TotalSeconds;
        }
    }
}
