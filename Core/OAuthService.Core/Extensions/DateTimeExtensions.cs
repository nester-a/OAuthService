namespace OAuthService.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static uint ToUnixTimestamp(this DateTime dateTime)
        {
            return (uint)dateTime.Subtract(DateTime.UnixEpoch).TotalSeconds;
        }
    }
}
