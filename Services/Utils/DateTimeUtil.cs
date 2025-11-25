namespace CinemaSystem.Services.Utils
{
    public static class DateTimeUtil
    {
        public static DateTime ToUtc(this DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Utc)
                return dateTime;

            if (dateTime.Kind == DateTimeKind.Local)
                return dateTime.ToUniversalTime();

            return DateTime.SpecifyKind(dateTime, DateTimeKind.Local).ToUniversalTime();
        }

        public static DateTime ToLocal(this DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Local)
                return dateTime;

            if (dateTime.Kind == DateTimeKind.Utc)
                return dateTime.ToLocalTime();

            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ToLocalTime();
        }
    }
}
