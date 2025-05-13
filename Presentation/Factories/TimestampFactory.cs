using Google.Protobuf.WellKnownTypes;

namespace Presentation.Factories;

public class TimestampFactory
{
    public static string ToViewModel(Timestamp timestamp)
    {
        DateTime dateTime = timestamp.ToDateTime();
        var returnDateTime = dateTime.ToString("yyyyMMddHHmm");
        return returnDateTime;
    }

    public static Timestamp FromViewModel(string date)
    {
        DateTime dateTime = DateTime.Parse(date);
        Timestamp timestamp = dateTime.ToTimestamp();
        return timestamp;
    }
}