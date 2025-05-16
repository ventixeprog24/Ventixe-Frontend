using System.Globalization;
using Google.Protobuf.WellKnownTypes;

namespace Presentation.Factories;

public class TimestampFactory
{
    public static string ToViewModel(Timestamp timestamp)
    {
        //Got help from GPT to make sure it's always UTC.
        DateTime dateTimeUtc = timestamp.ToDateTime().ToUniversalTime();
        DateTime dateOnly = dateTimeUtc.Date;
        var returnDateTime = dateTimeUtc.ToString("yyyy-MM-dd");
        return returnDateTime;
    }

    public static Timestamp FromViewModel(string date)
    {
        //This DataTimeParse is GPT generated because i couldn't solve it to get guaranteed to UTC.
        var dateTime = DateTime.Parse(
            date,
            CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
        
        Timestamp timestamp = dateTime.ToTimestamp();
        return timestamp;
    }
}