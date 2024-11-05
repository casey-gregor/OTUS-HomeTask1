using System;
using System.Globalization;

namespace RealTime
{
    public static class TextFormatter
    {
        public static DateTime StringToDateTimeUtcStrict(string text)
        {
            return DateTime.ParseExact(
                text, 
                "yyyy-MM-ddTHH:mm:ss.fffffffK", 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal
            );
        }
        public static DateTime StringToDateTimeUtcNonStrict(string text)
        {
            return DateTime.Parse(
                text, 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal
            );
        }

        public static string DateTimeToString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
        }
    }
}