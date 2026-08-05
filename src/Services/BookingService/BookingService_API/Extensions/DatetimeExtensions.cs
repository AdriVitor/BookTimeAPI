namespace BookingService_API.Extensions
{
    public static class DatetimeExtensions
    {
        public static DateTime SetSpecifyKind(this DateTime dateTime, DateTimeKind kind)
        {
            dateTime = DateTime.SpecifyKind(dateTime, kind);

            return dateTime;
        }
    }
}
