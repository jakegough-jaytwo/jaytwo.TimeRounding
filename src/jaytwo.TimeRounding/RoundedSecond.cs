namespace jaytwo.TimeRounding;

public class RoundedSecond
{
    private TimeSpan _value;

    public RoundedSecond(TimeSpan value, AbsoluteRounding mode)
        : this(RoundSeconds(value, mode))
    {
    }

    public RoundedSecond(TimeSpan value, MidpointRounding mode)
        : this(RoundSeconds(value, mode))
    {
    }

    internal RoundedSecond(TimeSpan value)
    {
        var nearestMicrosecond = value.NearestMicrosecond();
        if (nearestMicrosecond.TotalSeconds % 1 != 0)
        {
            throw new ArgumentException("TimeSpan must be within a microsecond of a full second increment.");
        }

        _value = nearestMicrosecond;
    }

    public int Hours => (int)Math.Floor(_value.TotalHours);

    public int Minutes => (int)_value.Minutes;

    public int Seconds => (int)_value.Seconds;

    public double TotalHours => _value.TotalHours;

    public double TotalMinutes => _value.TotalMinutes;

    public long TotalSeconds => (long)_value.TotalSeconds;

    public long TotalMilliseconds => (long)_value.TotalMilliseconds;

    public TimeSpan TimeSpan => _value;

    public static RoundedSecond FromHoursExact(double hours)
        => new RoundedSecond(TimeSpan.FromHours(hours));

    public static RoundedSecond FromHours(double hours, AbsoluteRounding mode)
        => new RoundedSecond(TimeSpan.FromHours(hours), mode);

    public static RoundedSecond FromHours(double hours, MidpointRounding mode)
        => new RoundedSecond(TimeSpan.FromHours(hours), mode);

    public static RoundedSecond FromMinutesExact(double minutes)
        => new RoundedSecond(TimeSpan.FromMinutes(minutes));

    public static RoundedSecond FromMinutes(double minutes, AbsoluteRounding mode)
        => new RoundedSecond(TimeSpan.FromMinutes(minutes), mode);

    public static RoundedSecond FromMinutes(double minutes, MidpointRounding mode)
        => new RoundedSecond(TimeSpan.FromMinutes(minutes), mode);

    public static RoundedSecond FromSecondsExact(long seconds)
        => new RoundedSecond(TimeSpan.FromSeconds(seconds));

    public static RoundedSecond FromSeconds(double seconds, AbsoluteRounding mode)
        => new RoundedSecond(TimeSpan.FromSeconds(seconds), mode);

    public static RoundedSecond FromSeconds(double seconds, MidpointRounding mode)
        => new RoundedSecond(TimeSpan.FromSeconds(seconds), mode);

    public RoundedSecond Negative()
        => new RoundedSecond(_value.Negate());

    public RoundedSecond AddHours(double hours, AbsoluteRounding mode)
        => Add(TimeSpan.FromHours(hours), mode);

    public RoundedSecond AddHours(double hours, MidpointRounding mode)
        => Add(TimeSpan.FromHours(hours), mode);

    public RoundedSecond AddMinutes(double minutes, AbsoluteRounding mode)
        => Add(TimeSpan.FromMinutes(minutes), mode);

    public RoundedSecond AddMinutes(double minutes, MidpointRounding mode)
        => Add(TimeSpan.FromMinutes(minutes), mode);

    public RoundedSecond AddSeconds(double seconds, AbsoluteRounding mode)
        => Add(TimeSpan.FromSeconds(seconds), mode);

    public RoundedSecond AddSeconds(double seconds, MidpointRounding mode)
        => Add(TimeSpan.FromSeconds(seconds), mode);

    public RoundedSecond Add(TimeSpan value, AbsoluteRounding mode)
        => new RoundedSecond(_value.Add(RoundSeconds(value, mode)));

    public RoundedSecond Add(TimeSpan value, MidpointRounding mode)
        => new RoundedSecond(_value.Add(RoundSeconds(value, mode)));

    public RoundedSecond Add(RoundedMinute value)
        => new RoundedSecond(_value.Add(value.TimeSpan));

    public RoundedSecond Add(RoundedSecond value)
        => new RoundedSecond(_value.Add(value.TimeSpan));

    public RoundedSecond Subtract(RoundedMinute value)
        => Add(value.Negative());

    public RoundedSecond Subtract(RoundedSecond value)
        => Add(value.Negative());

    public double GetRoundedTotalHours(int digits, MidpointRounding mode)
        => Math.Round(TotalHours, digits, mode);

    public double GetRoundedTotalHours(int digits, AbsoluteRounding mode)
        => AbsoluteRounder.Round(TotalHours, digits, mode);

    public override string ToString()
        => $"{Hours}:{Minutes:00}:{Seconds:00}";

    public RoundedMinute ToRoundedMinute(MidpointRounding mode)
        => new RoundedMinute(_value, mode);

    public RoundedMinute ToRoundedMinute(AbsoluteRounding mode)
        => new RoundedMinute(_value, mode);

    public RoundedMillisecond ToRoundedMillisecond()
        => new RoundedMillisecond(_value);

    internal static TimeSpan RoundSeconds(TimeSpan timeSpan, AbsoluteRounding mode)
        => mode switch
        {
            AbsoluteRounding.Ceiling => timeSpan.CeilingSeconds(),
            AbsoluteRounding.Floor => timeSpan.FloorSeconds(),
            AbsoluteRounding.Truncate => timeSpan.TruncateSeconds(),
            _ => throw new ArgumentOutOfRangeException(nameof(mode)),
        };

    internal static TimeSpan RoundSeconds(TimeSpan timeSpan, MidpointRounding mode)
        => timeSpan.NearestSecond(mode);
}
