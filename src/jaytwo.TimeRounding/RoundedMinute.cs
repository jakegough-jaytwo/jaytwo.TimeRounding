using System;

namespace jaytwo.TimeRounding;

public class RoundedMinute
{
    private TimeSpan _value;

    public RoundedMinute(TimeSpan value, AbsoluteRounding mode)
        : this(RoundMinutes(value, mode))
    {
    }

    public RoundedMinute(TimeSpan value, MidpointRounding mode)
        : this(RoundMinutes(value, mode))
    {
    }

    internal RoundedMinute(TimeSpan value)
    {
        var nearestMicrosecond = value.NearestMicrosecond();
        if (nearestMicrosecond.TotalMinutes % 1 != 0)
        {
            throw new ArgumentException("TimeSpan must be within a microsecond of a full minute increment.");
        }

        _value = nearestMicrosecond;
    }

    public int Hours => (int)Math.Floor(_value.TotalHours);

    public int Minutes => (int)_value.Minutes;

    public double TotalHours => _value.TotalHours;

    public long TotalMinutes => (long)_value.TotalMinutes;

    public long TotalSeconds => (long)_value.TotalSeconds;

    public long TotalMilliseconds => (long)_value.TotalMilliseconds;

    public TimeSpan TimeSpan => _value;

    public static RoundedMinute FromHoursExact(double hours)
        => new RoundedMinute(TimeSpan.FromHours(hours));

    public static RoundedMinute FromHours(double hours, AbsoluteRounding mode)
        => new RoundedMinute(TimeSpan.FromHours(hours), mode);

    public static RoundedMinute FromHours(double hours, MidpointRounding mode)
        => new RoundedMinute(TimeSpan.FromHours(hours), mode);

    public static RoundedMinute FromMinutesExact(long minutes)
        => new RoundedMinute(TimeSpan.FromMinutes(minutes));

    public static RoundedMinute FromMinutes(double minutes, AbsoluteRounding mode)
        => new RoundedMinute(TimeSpan.FromMinutes(minutes), mode);

    public static RoundedMinute FromMinutes(double minutes, MidpointRounding mode)
        => new RoundedMinute(TimeSpan.FromMinutes(minutes), mode);

    public RoundedMinute Negative()
        => new RoundedMinute(_value.Negate());

    public RoundedMinute AddHours(double hours, AbsoluteRounding mode)
        => Add(TimeSpan.FromHours(hours), mode);

    public RoundedMinute AddHours(double hours, MidpointRounding mode)
        => Add(TimeSpan.FromHours(hours), mode);

    public RoundedMinute AddMinutes(double minutes, AbsoluteRounding mode)
        => Add(TimeSpan.FromMinutes(minutes), mode);

    public RoundedMinute AddMinutes(double minutes, MidpointRounding mode)
        => Add(TimeSpan.FromMinutes(minutes), mode);

    public RoundedMinute Add(TimeSpan value, AbsoluteRounding mode)
        => new RoundedMinute(_value.Add(RoundMinutes(value, mode)));

    public RoundedMinute Add(TimeSpan value, MidpointRounding mode)
        => new RoundedMinute(_value.Add(RoundMinutes(value, mode)));

    public RoundedMinute Add(RoundedMinute value)
        => new RoundedMinute(_value.Add(value.TimeSpan));

    public RoundedMinute Subtract(RoundedMinute value)
        => Add(value.Negative());

    public double GetRoundedTotalHours(int digits, MidpointRounding mode)
        => Math.Round(TotalHours, digits, mode);

    public double GetRoundedTotalHours(int digits, AbsoluteRounding mode)
        => AbsoluteRounder.Round(TotalHours, digits, mode);

    public override string ToString()
        => $"{Hours}:{Minutes:00}";

    public RoundedSecond ToRoundedSecond()
        => new RoundedSecond(_value);

    public RoundedMillisecond ToRoundedMillisecond()
        => new RoundedMillisecond(_value);

    internal static TimeSpan RoundMinutes(TimeSpan timeSpan, AbsoluteRounding mode)
        => mode switch
        {
            AbsoluteRounding.Ceiling => timeSpan.CeilingMinutes(),
            AbsoluteRounding.Floor => timeSpan.FloorMinutes(),
            AbsoluteRounding.Truncate => timeSpan.TruncateMinutes(),
            _ => throw new ArgumentOutOfRangeException(nameof(mode)),
        };

    internal static TimeSpan RoundMinutes(TimeSpan timeSpan, MidpointRounding mode)
        => timeSpan.NearestMinute(mode);
}
