using System.Globalization;

namespace ArwynFr.Reforger.ServerMgr.Domain;

public sealed record LogDirectoryName(DateTime Value)
{
    private const string Prefix = "logs";
    private const string DateFormat = "yyyy-MM-dd";
    private const string TimeFormat = "HH-mm-ss";
    private const string Separator = "_";
    public static LogDirectoryName? Factory(string? value) => value?.Split(Separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) switch
    {
        [Prefix, string date, string time] when
            DateOnly.TryParseExact(date, DateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out var _date) &&
            TimeOnly.TryParseExact(time, TimeFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out var _time)
            => new(new DateTime(_date, _time, DateTimeKind.Local)),
        _ => null
    };

    internal string ToPath() => string.Join(Separator, Prefix, Value.ToString(DateFormat), Value.ToString(TimeFormat));

}