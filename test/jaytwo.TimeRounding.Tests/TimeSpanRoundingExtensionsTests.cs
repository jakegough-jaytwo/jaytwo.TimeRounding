using Xunit;

namespace jaytwo.TimeRounding.Tests;

public class TimeSpanRoundingExtensionsTests
{
    [Theory]
    [InlineData(1.5, 1)]
    [InlineData(1, 1)]
    [InlineData(-1.5, -2)]
    public void FloorMillisecondsReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromMilliseconds(value);

        // act
        var actual = timespan.FloorMilliseconds().TotalMilliseconds;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1.5, 1)]
    [InlineData(1, 1)]
    [InlineData(-1.5, -2)]
    public void FloorSecondsReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromSeconds(value);

        // act
        var actual = timespan.FloorSeconds().TotalSeconds;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1.5, 1)]
    [InlineData(1, 1)]
    [InlineData(-1.5, -2)]
    public void FloorMinutesReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromMinutes(value);

        // act
        var actual = timespan.FloorMinutes().TotalMinutes;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1.5, 1)]
    [InlineData(1, 1)]
    [InlineData(-1.5, -2)]
    public void FloorHoursReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromHours(value);

        // act
        var actual = timespan.FloorHours().TotalHours;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1.5, 1)]
    [InlineData(1, 1)]
    [InlineData(-1.5, -2)]
    public void FloorDaysReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromDays(value);

        // act
        var actual = timespan.FloorDays().TotalDays;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1.5, 2)]
    [InlineData(1, 1)]
    [InlineData(-1.5, -1)]
    public void CeilingMillisecondsReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromMilliseconds(value);

        // act
        var actual = timespan.CeilingMilliseconds().TotalMilliseconds;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1.5, 2)]
    [InlineData(1, 1)]
    [InlineData(-1.5, -1)]
    public void CeilingSecondsReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromSeconds(value);

        // act
        var actual = timespan.CeilingSeconds().TotalSeconds;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1.5, 2)]
    [InlineData(1, 1)]
    [InlineData(-1.5, -1)]
    public void CeilingMinutesReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromMinutes(value);

        // act
        var actual = timespan.CeilingMinutes().TotalMinutes;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1.5, 2)]
    [InlineData(1, 1)]
    [InlineData(-1.5, -1)]
    public void CeilingHoursReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromHours(value);

        // act
        var actual = timespan.CeilingHours().TotalHours;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1.5, 2)]
    [InlineData(1, 1)]
    [InlineData(-1.5, -1)]
    public void CeilingDaysReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromDays(value);

        // act
        var actual = timespan.CeilingDays().TotalDays;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1.5, 1)]
    [InlineData(1, 1)]
    [InlineData(-1.5, -1)]
    public void TruncateMillisecondsReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromMilliseconds(value);

        // act
        var actual = timespan.TruncateMilliseconds().TotalMilliseconds;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1.5, 1)]
    [InlineData(1, 1)]
    [InlineData(-1.5, -1)]
    public void TruncateSecondsReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromSeconds(value);

        // act
        var actual = timespan.TruncateSeconds().TotalSeconds;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1.5, 1)]
    [InlineData(1, 1)]
    [InlineData(-1.5, -1)]
    public void TruncateMinutesReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromMinutes(value);

        // act
        var actual = timespan.TruncateMinutes().TotalMinutes;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1.5, 1)]
    [InlineData(1, 1)]
    [InlineData(-1.5, -1)]
    public void TruncateHoursReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromHours(value);

        // act
        var actual = timespan.TruncateHours().TotalHours;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1.5, 1)]
    [InlineData(1, 1)]
    [InlineData(-1.5, -1)]
    public void TruncateDaysReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromDays(value);

        // act
        var actual = timespan.TruncateDays().TotalDays;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-0.5, MidpointRounding.AwayFromZero, -1)]
    [InlineData(-1.5, MidpointRounding.AwayFromZero, -2)]
    [InlineData(0.5, MidpointRounding.AwayFromZero, 1)]
    [InlineData(1.5, MidpointRounding.AwayFromZero, 2)]
    [InlineData(-0.5, MidpointRounding.ToEven, 0)]
    [InlineData(-1.5, MidpointRounding.ToEven, -2)]
    [InlineData(0.5, MidpointRounding.ToEven, 0)]
    [InlineData(1.5, MidpointRounding.ToEven, 2)]
    public void NearestMicrosecondWithMidpointRoundingReturnsExpectedResults(double value, MidpointRounding midpointRounding, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromMilliseconds(value / 1000d);

        // act
        var actual = timespan.NearestMicrosecond(midpointRounding).TotalMilliseconds * 1000d;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-0.4, -0)]
    [InlineData(-0.6, -1)]
    [InlineData(0.4, 0)]
    [InlineData(0.6, 1)]
    public void NearestMillisecondReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromMilliseconds(value);

        // act
        var actual = timespan.NearestMillisecond().TotalMilliseconds;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-0.5, MidpointRounding.AwayFromZero, -1)]
    [InlineData(-1.5, MidpointRounding.AwayFromZero, -2)]
    [InlineData(0.5, MidpointRounding.AwayFromZero, 1)]
    [InlineData(1.5, MidpointRounding.AwayFromZero, 2)]
    [InlineData(-0.5, MidpointRounding.ToEven, 0)]
    [InlineData(-1.5, MidpointRounding.ToEven, -2)]
    [InlineData(0.5, MidpointRounding.ToEven, 0)]
    [InlineData(1.5, MidpointRounding.ToEven, 2)]
    public void NearestMillisecondWithMidpointRoundingReturnsExpectedResults(double value, MidpointRounding midpointRounding, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromMilliseconds(value);

        // act
        var actual = timespan.NearestMillisecond(midpointRounding).TotalMilliseconds;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-0.4, 0)]
    [InlineData(-0.6, -1)]
    [InlineData(0.4, 0)]
    [InlineData(0.6, 1)]
    public void NearestSecondReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromSeconds(value);

        // act
        var actual = timespan.NearestSecond().TotalSeconds;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-0.5, MidpointRounding.AwayFromZero, -1)]
    [InlineData(-1.5, MidpointRounding.AwayFromZero, -2)]
    [InlineData(0.5, MidpointRounding.AwayFromZero, 1)]
    [InlineData(1.5, MidpointRounding.AwayFromZero, 2)]
    [InlineData(-0.5, MidpointRounding.ToEven, 0)]
    [InlineData(-1.5, MidpointRounding.ToEven, -2)]
    [InlineData(0.5, MidpointRounding.ToEven, 0)]
    [InlineData(1.5, MidpointRounding.ToEven, 2)]
    public void NearestSecondWithMidpointRoundingReturnsExpectedResults(double value, MidpointRounding midpointRounding, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromSeconds(value);

        // act
        var actual = timespan.NearestSecond(midpointRounding).TotalSeconds;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-0.4, 0)]
    [InlineData(-0.6, -1)]
    [InlineData(0.4, 0)]
    [InlineData(0.6, 1)]
    public void NearestMinuteReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromMinutes(value);

        // act
        var actual = timespan.NearestMinute().TotalMinutes;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-0.5, MidpointRounding.AwayFromZero, -1)]
    [InlineData(-1.5, MidpointRounding.AwayFromZero, -2)]
    [InlineData(0.5, MidpointRounding.AwayFromZero, 1)]
    [InlineData(1.5, MidpointRounding.AwayFromZero, 2)]
    [InlineData(-0.5, MidpointRounding.ToEven, 0)]
    [InlineData(-1.5, MidpointRounding.ToEven, -2)]
    [InlineData(0.5, MidpointRounding.ToEven, 0)]
    [InlineData(1.5, MidpointRounding.ToEven, 2)]
    public void NearestMinuteWithMidpointRoundingReturnsExpectedResults(double value, MidpointRounding midpointRounding, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromMinutes(value);

        // act
        var actual = timespan.NearestMinute(midpointRounding).TotalMinutes;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-0.4, 0)]
    [InlineData(-0.6, -1)]
    [InlineData(0.4, 0)]
    [InlineData(0.6, 1)]
    public void NearestHourReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromHours(value);

        // act
        var actual = timespan.NearestHour().TotalHours;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-0.5, MidpointRounding.AwayFromZero, -1)]
    [InlineData(-1.5, MidpointRounding.AwayFromZero, -2)]
    [InlineData(0.5, MidpointRounding.AwayFromZero, 1)]
    [InlineData(1.5, MidpointRounding.AwayFromZero, 2)]
    [InlineData(-0.5, MidpointRounding.ToEven, 0)]
    [InlineData(-1.5, MidpointRounding.ToEven, -2)]
    [InlineData(0.5, MidpointRounding.ToEven, 0)]
    [InlineData(1.5, MidpointRounding.ToEven, 2)]
    public void NearestHourWithMidpointRoundingReturnsExpectedResults(double value, MidpointRounding midpointRounding, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromHours(value);

        // act
        var actual = timespan.NearestHour(midpointRounding).TotalHours;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-0.4, 0)]
    [InlineData(-0.6, -1)]
    [InlineData(0.4, 0)]
    [InlineData(0.6, 1)]
    public void NearestDayReturnsExpectedResults(double value, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromDays(value);

        // act
        var actual = timespan.NearestDay().TotalDays;

        // assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-0.5, MidpointRounding.AwayFromZero, -1)]
    [InlineData(-1.5, MidpointRounding.AwayFromZero, -2)]
    [InlineData(0.5, MidpointRounding.AwayFromZero, 1)]
    [InlineData(1.5, MidpointRounding.AwayFromZero, 2)]
    [InlineData(-0.5, MidpointRounding.ToEven, 0)]
    [InlineData(-1.5, MidpointRounding.ToEven, -2)]
    [InlineData(0.5, MidpointRounding.ToEven, 0)]
    [InlineData(1.5, MidpointRounding.ToEven, 2)]
    public void NearestDayWithMidpointRoundingReturnsExpectedResults(double value, MidpointRounding midpointRounding, double expected)
    {
        // arrange
        var timespan = TimeSpan.FromDays(value);

        // act
        var actual = timespan.NearestDay(midpointRounding).TotalDays;

        // assert
        Assert.Equal(expected, actual);
    }
}
