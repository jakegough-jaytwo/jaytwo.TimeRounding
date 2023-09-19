namespace jaytwo.TimeRounding;

public static class TimeSpanRoundingExtensions
{
    public static TimeSpan FloorDays(this TimeSpan input)
        => TimeSpan.FromDays(Math.Floor(input.TotalDays));

    public static TimeSpan CeilingDays(this TimeSpan input)
        => TimeSpan.FromDays(Math.Ceiling(input.TotalDays));

    public static TimeSpan TruncateDays(this TimeSpan input)
        => TimeSpan.FromDays(Math.Truncate(input.TotalDays));

    public static TimeSpan FloorHours(this TimeSpan input)
        => TimeSpan.FromHours(Math.Floor(input.TotalHours));

    public static TimeSpan CeilingHours(this TimeSpan input)
        => TimeSpan.FromHours(Math.Ceiling(input.TotalHours));

    public static TimeSpan TruncateHours(this TimeSpan input)
        => TimeSpan.FromHours(Math.Truncate(input.TotalHours));

    public static TimeSpan FloorMinutes(this TimeSpan input)
        => TimeSpan.FromMinutes(Math.Floor(input.TotalMinutes));

    public static TimeSpan CeilingMinutes(this TimeSpan input)
        => TimeSpan.FromMinutes(Math.Ceiling(input.TotalMinutes));

    public static TimeSpan TruncateMinutes(this TimeSpan input)
        => TimeSpan.FromMinutes(Math.Truncate(input.TotalMinutes));

    public static TimeSpan FloorSeconds(this TimeSpan input)
        => TimeSpan.FromSeconds(Math.Floor(input.TotalSeconds));

    public static TimeSpan CeilingSeconds(this TimeSpan input)
        => TimeSpan.FromSeconds(Math.Ceiling(input.TotalSeconds));

    public static TimeSpan TruncateSeconds(this TimeSpan input)
        => TimeSpan.FromSeconds(Math.Truncate(input.TotalSeconds));

    public static TimeSpan FloorMilliseconds(this TimeSpan input)
        => TimeSpan.FromMilliseconds(Math.Floor(input.TotalMilliseconds));

    public static TimeSpan CeilingMilliseconds(this TimeSpan input)
        => TimeSpan.FromMilliseconds(Math.Ceiling(input.TotalMilliseconds));

    public static TimeSpan TruncateMilliseconds(this TimeSpan input)
        => TimeSpan.FromMilliseconds(Math.Truncate(input.TotalMilliseconds));

    public static TimeSpan NearestDay(this TimeSpan input, MidpointRounding midpointRounding = default)
        => TimeSpan.FromDays(Math.Round(input.TotalDays, 0, midpointRounding));

    public static TimeSpan NearestHour(this TimeSpan input, MidpointRounding midpointRounding = default)
        => TimeSpan.FromHours(Math.Round(input.TotalHours, 0, midpointRounding));

    public static TimeSpan NearestMinute(this TimeSpan input, MidpointRounding midpointRounding = default)
        => TimeSpan.FromMinutes(Math.Round(input.TotalMinutes, 0, midpointRounding));

    public static TimeSpan NearestSecond(this TimeSpan input, MidpointRounding midpointRounding = default)
        => TimeSpan.FromSeconds(Math.Round(input.TotalSeconds, 0, midpointRounding));

    public static TimeSpan NearestMillisecond(this TimeSpan input, MidpointRounding midpointRounding = default)
        => TimeSpan.FromMilliseconds(Math.Round(input.TotalMilliseconds, 0, midpointRounding));

    public static TimeSpan NearestMicrosecond(this TimeSpan input, MidpointRounding midpointRounding = default)
        => TimeSpan.FromMilliseconds(Math.Round(input.TotalMilliseconds, 3, midpointRounding));
}
