namespace jaytwo.TimeRounding;

public class RoundedMillisecond
{
    private TimeSpan _value;

    public RoundedMillisecond(TimeSpan value, AbsoluteRounding mode)
        : this(RoundMilliseconds(value, mode))
    {
    }

    public RoundedMillisecond(TimeSpan value, MidpointRounding mode)
        : this(RoundMilliseconds(value, mode))
    {
    }

    internal RoundedMillisecond(TimeSpan value)
    {
        var nearestMicrosecond = value.NearestMicrosecond();
        if (nearestMicrosecond.TotalMilliseconds % 1 != 0)
        {
            throw new ArgumentException("TimeSpan must be within a microsecond of a full millisecond increment.");
        }

        _value = nearestMicrosecond;
    }

    public int Hours => (int)Math.Floor(_value.TotalHours);

    public int Minutes => (int)_value.Minutes;

    public int Seconds => (int)_value.Seconds;

    public int Milliseconds => (int)_value.Milliseconds;

    public double TotalHours => _value.TotalHours;

    public double TotalMinutes => _value.TotalMinutes;

    public double TotalSeconds => _value.TotalSeconds;

    public long TotalMilliseconds => (long)_value.TotalMilliseconds;

    public TimeSpan TimeSpan => _value;

    public static RoundedMillisecond FromHoursExact(double hours)
        => new RoundedMillisecond(TimeSpan.FromHours(hours));

    public static RoundedMillisecond FromHours(double hours, AbsoluteRounding mode)
        => new RoundedMillisecond(TimeSpan.FromHours(hours), mode);

    public static RoundedMillisecond FromHours(double hours, MidpointRounding mode)
        => new RoundedMillisecond(TimeSpan.FromHours(hours), mode);

    public static RoundedMillisecond FromMinutesExact(double minutes)
        => new RoundedMillisecond(TimeSpan.FromMinutes(minutes));

    public static RoundedMillisecond FromMinutes(double minutes, AbsoluteRounding mode)
        => new RoundedMillisecond(TimeSpan.FromMinutes(minutes), mode);

    public static RoundedMillisecond FromMinutes(double minutes, MidpointRounding mode)
        => new RoundedMillisecond(TimeSpan.FromMinutes(minutes), mode);

    public static RoundedMillisecond FromSecondsExact(double seconds)
        => new RoundedMillisecond(TimeSpan.FromSeconds(seconds));

    public static RoundedMillisecond FromSeconds(double seconds, AbsoluteRounding mode)
        => new RoundedMillisecond(TimeSpan.FromSeconds(seconds), mode);

    public static RoundedMillisecond FromSeconds(double seconds, MidpointRounding mode)
        => new RoundedMillisecond(TimeSpan.FromSeconds(seconds), mode);

    public static RoundedMillisecond FromMillisecondsExact(long milliseconds)
        => new RoundedMillisecond(TimeSpan.FromMilliseconds(milliseconds));

    public static RoundedMillisecond FromMilliseconds(double milliseconds, AbsoluteRounding mode)
        => new RoundedMillisecond(TimeSpan.FromMilliseconds(milliseconds), mode);

    public static RoundedMillisecond FromMilliseconds(double milliseconds, MidpointRounding mode)
        => new RoundedMillisecond(TimeSpan.FromMilliseconds(milliseconds), mode);

    public RoundedMillisecond Negative()
        => new RoundedMillisecond(_value.Negate());

    public RoundedMillisecond AddHours(double hours, AbsoluteRounding mode)
        => Add(TimeSpan.FromHours(hours), mode);

    public RoundedMillisecond AddHours(double hours, MidpointRounding mode)
        => Add(TimeSpan.FromHours(hours), mode);

    public RoundedMillisecond AddMinutes(double minutes, AbsoluteRounding mode)
        => Add(TimeSpan.FromMinutes(minutes), mode);

    public RoundedMillisecond AddMinutes(double minutes, MidpointRounding mode)
        => Add(TimeSpan.FromMinutes(minutes), mode);

    public RoundedMillisecond AddSeconds(double seconds, AbsoluteRounding mode)
        => Add(TimeSpan.FromSeconds(seconds), mode);

    public RoundedMillisecond AddSeconds(double seconds, MidpointRounding mode)
        => Add(TimeSpan.FromSeconds(seconds), mode);

    public RoundedMillisecond Add(TimeSpan value, AbsoluteRounding mode)
        => new RoundedMillisecond(_value.Add(RoundMilliseconds(value, mode)));

    public RoundedMillisecond Add(TimeSpan value, MidpointRounding mode)
        => new RoundedMillisecond(_value.Add(RoundMilliseconds(value, mode)));

    public RoundedMillisecond Add(RoundedMinute value)
        => new RoundedMillisecond(_value.Add(value.TimeSpan));

    public RoundedMillisecond Add(RoundedSecond value)
        => new RoundedMillisecond(_value.Add(value.TimeSpan));

    public RoundedMillisecond Add(RoundedMillisecond value)
        => new RoundedMillisecond(_value.Add(value.TimeSpan));

    public RoundedMillisecond Subtract(RoundedMinute value)
        => Add(value.Negative());

    public RoundedMillisecond Subtract(RoundedSecond value)
        => Add(value.Negative());

    public RoundedMillisecond Subtract(RoundedMillisecond value)
        => Add(value.Negative());

    public double GetRoundedTotalHours(int digits, MidpointRounding mode)
        => Math.Round(TotalHours, digits, mode);

    public double GetRoundedTotalHours(int digits, AbsoluteRounding mode)
        => AbsoluteRounder.Round(TotalHours, digits, mode);

    public override string ToString()
        => $"{Hours}:{Minutes:00}:{Seconds:00}.{Milliseconds:000}";

    public RoundedMinute ToRoundedMinute(MidpointRounding mode)
        => new RoundedMinute(_value, mode);

    public RoundedMinute ToRoundedMinute(AbsoluteRounding mode)
        => new RoundedMinute(_value, mode);

    public RoundedSecond ToRoundedSecond(MidpointRounding mode)
        => new RoundedSecond(_value, mode);

    public RoundedSecond ToRoundedSecond(AbsoluteRounding mode)
        => new RoundedSecond(_value, mode);

    internal static TimeSpan RoundMilliseconds(TimeSpan timeSpan, AbsoluteRounding mode)
        => mode switch
        {
            AbsoluteRounding.Ceiling => timeSpan.CeilingMilliseconds(),
            AbsoluteRounding.Floor => timeSpan.FloorMilliseconds(),
            AbsoluteRounding.Truncate => timeSpan.TruncateMilliseconds(),
            _ => throw new ArgumentOutOfRangeException(nameof(mode)),
        };

    internal static TimeSpan RoundMilliseconds(TimeSpan timeSpan, MidpointRounding mode)
        => timeSpan.NearestMillisecond(mode);
}
