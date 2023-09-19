using Xunit;

namespace jaytwo.TimeRounding.Tests;

public class RoundedSecondTests
{
    [Theory]
    [InlineData(1, "0:00:01")]
    [InlineData(100, "0:01:40")]
    [InlineData(86401, "24:00:01")]
    public void ToString_returns_expected_value(int seconds, string expectedString)
    {
        // arrange
        var roundedMinute = new RoundedSecond(TimeSpan.FromSeconds(seconds));

        // act
        var actualString = roundedMinute.ToString();

        // assert
        Assert.Equal(expectedString, actualString);
    }

    [Theory]
    [InlineData(1, 1000)]
    [InlineData(120, 120000)]
    public void ToRoundedMillisecond_returns_expected_value(int seconds, int expectedMilliseconds)
    {
        // arrange
        var roundedSecond = RoundedSecond.FromSecondsExact(seconds);

        // act
        var roundedMillisecond = roundedSecond.ToRoundedMillisecond();

        // assert
        Assert.Equal(expectedMilliseconds, roundedMillisecond.TotalMilliseconds);
    }

    [Theory]
    [InlineData(90, MidpointRounding.ToZero, 1)]
    [InlineData(90, MidpointRounding.AwayFromZero, 2)]
    public void ToRoundedMinute_MidpointRounding_returns_expected_value(int seconds, MidpointRounding mode, int expectedMinutes)
    {
        // arrange
        var roundedMillisecond = RoundedSecond.FromSecondsExact(seconds);

        // act
        var roundedMinute = roundedMillisecond.ToRoundedMinute(mode);

        // assert
        Assert.Equal(expectedMinutes, roundedMinute.TotalMinutes);
    }

    [Theory]
    [InlineData(1, AbsoluteRounding.Ceiling, 1)]
    [InlineData(119, AbsoluteRounding.Floor, 1)]
    public void ToRoundedMinute_AbsoluteRounding_returns_expected_value(int seconds, AbsoluteRounding mode, int expectedMinutes)
    {
        // arrange
        var roundedMillisecond = RoundedSecond.FromSecondsExact(seconds);

        // act
        var roundedMinute = roundedMillisecond.ToRoundedMinute(mode);

        // assert
        Assert.Equal(expectedMinutes, roundedMinute.TotalMinutes);
    }

    [Theory]
    [InlineData(1, -1)]
    [InlineData(-1, 1)]
    public void Negative_returns_expected_value(int value, double expectedNegative)
    {
        // arrange
        var roundedMinute = new RoundedSecond(TimeSpan.FromSeconds(value));

        // act
        var negative = roundedMinute.Negative();

        // assert
        Assert.Equal(expectedNegative, negative.TotalSeconds);
    }

    [Theory]
    [InlineData(1, 0.00027777777777777778)]
    [InlineData(86400, 24)]
    public void TotalHours_returns_expected_value(int seconds, double expectedTotalHours)
    {
        // arrange
        var roundedMinute = new RoundedSecond(TimeSpan.FromSeconds(seconds));

        // act
        var actualTotalHours = roundedMinute.TotalHours;

        // assert
        Assert.Equal(expectedTotalHours, actualTotalHours);
    }

    [Theory]
    [InlineData(1800, 0, MidpointRounding.AwayFromZero, 1)]
    [InlineData(1800, 0, MidpointRounding.ToZero, 0)]
    [InlineData(1800, 1, MidpointRounding.AwayFromZero, 0.5)]
    [InlineData(1800, 1, MidpointRounding.ToZero, 0.5)]
    public void GetRoundedTotalHours_MidpointRounding_returns_expected_value(int seconds, int digits, MidpointRounding mode, double expectedTotalHours)
    {
        // arrange
        var roundedMinute = new RoundedSecond(TimeSpan.FromSeconds(seconds));

        // act
        var actualTotalHours = roundedMinute.GetRoundedTotalHours(digits, mode);

        // assert
        Assert.Equal(expectedTotalHours, actualTotalHours);
    }

    [Theory]
    [InlineData(1740, 0, AbsoluteRounding.Ceiling, 1)]
    [InlineData(1860, 0, AbsoluteRounding.Floor, 0)]
    [InlineData(1740, 2, AbsoluteRounding.Ceiling, 0.49)]
    [InlineData(1860, 2, AbsoluteRounding.Floor, 0.51)]
    public void GetRoundedTotalHours_AbsoluteRounding_returns_expected_value(int seconds, int digits, AbsoluteRounding mode, double expectedTotalHours)
    {
        // arrange
        var roundedMinute = new RoundedSecond(TimeSpan.FromSeconds(seconds));

        // act
        var actualTotalHours = roundedMinute.GetRoundedTotalHours(digits, mode);

        // assert
        Assert.Equal(expectedTotalHours, actualTotalHours);
    }

    [Theory]
    [InlineData(0.5, MidpointRounding.AwayFromZero, 1)]
    [InlineData(1.5, MidpointRounding.AwayFromZero, 2)]
    [InlineData(-0.5, MidpointRounding.AwayFromZero, -1)]
    [InlineData(-1.5, MidpointRounding.AwayFromZero, -2)]
    [InlineData(0.5, MidpointRounding.ToZero, 0)]
    [InlineData(1.5, MidpointRounding.ToZero, 1)]
    [InlineData(-0.5, MidpointRounding.ToZero, 0)]
    [InlineData(-1.5, MidpointRounding.ToZero, -1)]
    public void RoundSeconds_MidpointRounding_returns_expected_value(double seconds, MidpointRounding mode, double expectedSeconds)
    {
        // arrange
        var inputTime = TimeSpan.FromSeconds(seconds);

        // act
        var actualTime = RoundedSecond.RoundSeconds(inputTime, mode);

        // assert
        Assert.Equal(expectedSeconds, actualTime.TotalSeconds);
    }

    [Theory]
    [InlineData(0.1, AbsoluteRounding.Ceiling, 1)]
    [InlineData(1.1, AbsoluteRounding.Ceiling, 2)]
    [InlineData(-0.9, AbsoluteRounding.Ceiling, 0)]
    [InlineData(-1.9, AbsoluteRounding.Ceiling, -1)]
    [InlineData(0.9, AbsoluteRounding.Floor, 0)]
    [InlineData(1.9, AbsoluteRounding.Floor, 1)]
    [InlineData(-0.1, AbsoluteRounding.Floor, -1)]
    [InlineData(-1.1, AbsoluteRounding.Floor, -2)]
    [InlineData(0.9, AbsoluteRounding.Truncate, 0)]
    [InlineData(1.9, AbsoluteRounding.Truncate, 1)]
    [InlineData(-0.1, AbsoluteRounding.Truncate, 0)]
    [InlineData(-1.1, AbsoluteRounding.Truncate, -1)]
    public void RoundSeconds_AbsoluteRounding_returns_expected_value(double seconds, AbsoluteRounding mode, double expectedSeconds)
    {
        // arrange
        var inputTime = TimeSpan.FromSeconds(seconds);

        // act
        var actualTime = RoundedSecond.RoundSeconds(inputTime, mode);

        // assert
        Assert.Equal(expectedSeconds, actualTime.TotalSeconds);
    }

    [Theory]
    [InlineData(0.5, MidpointRounding.AwayFromZero, 1)]
    [InlineData(0.5, MidpointRounding.ToZero, 0)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void Constructor_MidpointRounding_returns_expected_value(double seconds, MidpointRounding mode, double expectedSeconds)
    {
        // arrange
        var time = TimeSpan.FromSeconds(seconds);

        // act
        var actual = new RoundedSecond(time, mode);

        // assert
        Assert.Equal(expectedSeconds, actual.TotalSeconds);
    }

    [Theory]
    [InlineData(0.4, AbsoluteRounding.Ceiling, 1)]
    [InlineData(0.6, AbsoluteRounding.Floor, 0)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void Constructor_AbsoluteRounding_returns_expected_value(double seconds, AbsoluteRounding mode, double expectedSeconds)
    {
        // arrange
        var time = TimeSpan.FromSeconds(seconds);

        // act
        var actual = new RoundedSecond(time, mode);

        // assert
        Assert.Equal(expectedSeconds, actual.TotalSeconds);
    }

    [Theory]
    [InlineData(0.5, MidpointRounding.AwayFromZero, 1)]
    [InlineData(0.5, MidpointRounding.ToZero, 0)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void FromSeconds_MidpointRounding_returns_expected_value(double seconds, MidpointRounding mode, double expectedSeconds)
    {
        // arrange

        // act
        var actualTime = RoundedSecond.FromSeconds(seconds, mode);

        // assert
        Assert.Equal(expectedSeconds, actualTime.TotalSeconds);
    }

    [Theory]
    [InlineData(0.4, AbsoluteRounding.Ceiling, 1)]
    [InlineData(0.6, AbsoluteRounding.Floor, 0)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void FromSeconds_AbsoluteRounding_returns_expected_value(double seconds, AbsoluteRounding mode, double expectedSeconds)
    {
        // arrange

        // act
        var actualTime = RoundedSecond.FromSeconds(seconds, mode);

        // assert
        Assert.Equal(expectedSeconds, actualTime.TotalSeconds);
    }

    [Theory]
    [InlineData(0.025, MidpointRounding.AwayFromZero, 2)] // 1.5s
    [InlineData(0.025, MidpointRounding.ToZero, 1)] // 1.5s
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void FromMinutes_MidpointRounding_returns_expected_value(double minutes, MidpointRounding mode, double expectedMinutes)
    {
        // arrange

        // act
        var actualTime = RoundedSecond.FromMinutes(minutes, mode);

        // assert
        Assert.Equal(expectedMinutes, actualTime.TotalSeconds);
    }

    [Theory]
    [InlineData(0.0375, AbsoluteRounding.Ceiling, 3)] // 2.25s
    [InlineData(0.0625, AbsoluteRounding.Floor, 3)] // 3.75s
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void FromMinutes_AbsoluteRounding_returns_expected_value(double minutes, AbsoluteRounding mode, double expectedSeconds)
    {
        // arrange

        // act
        var actualTime = RoundedSecond.FromMinutes(minutes, mode);

        // assert
        Assert.Equal(expectedSeconds, actualTime.TotalSeconds);
    }

    [Theory]
    [InlineData(0.08375, MidpointRounding.AwayFromZero, 302)] //301.5s
    [InlineData(0.08375, MidpointRounding.ToZero, 301)] //301.5s
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void FromHours_MidpointRounding_returns_expected_value(double hours, MidpointRounding mode, double expectedSeconds)
    {
        // arrange

        // act
        var actualTime = RoundedSecond.FromHours(hours, mode);

        // assert
        Assert.Equal(expectedSeconds, actualTime.TotalSeconds);
    }

    [Theory]
    [InlineData(0.08375, AbsoluteRounding.Ceiling, 302)] //301.5s
    [InlineData(0.08375, AbsoluteRounding.Floor, 301)] //301.5s
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void FromHours_AbsoluteRounding_returns_expected_value(double hours, AbsoluteRounding mode, double expectedSeconds)
    {
        // arrange

        // act
        var actualTime = RoundedSecond.FromHours(hours, mode);

        // assert
        Assert.Equal(expectedSeconds, actualTime.TotalSeconds);
    }

    [Theory]
    [InlineData(1, 1.5, MidpointRounding.AwayFromZero, 3)]
    [InlineData(1, 1.5, MidpointRounding.ToZero, 2)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void Add_MidpointRounding_returns_expected_value(int baseSeconds, double secondsToAdd, MidpointRounding mode, double expectedSeconds)
    {
        // arrange
        var baseTime = TimeSpan.FromSeconds(baseSeconds);
        var baseRoundedSecond = new RoundedSecond(baseTime);
        var timeToAdd = TimeSpan.FromSeconds(secondsToAdd);

        // act
        var actual = baseRoundedSecond.Add(timeToAdd, mode);

        // assert
        Assert.Equal(expectedSeconds, actual.TotalSeconds);
    }

    [Theory]
    [InlineData(1, 1.5, AbsoluteRounding.Ceiling, 3)]
    [InlineData(1, 1.5, AbsoluteRounding.Floor, 2)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void Add_AbsoluteRounding_returns_expected_value(int baseSeconds, double secondsToAdd, AbsoluteRounding mode, double expectedSeconds)
    {
        // arrange
        var baseTime = TimeSpan.FromSeconds(baseSeconds);
        var baseRoundedSecond = new RoundedSecond(baseTime);
        var timeToAdd = TimeSpan.FromSeconds(secondsToAdd);

        // act
        var actual = baseRoundedSecond.Add(timeToAdd, mode);

        // assert
        Assert.Equal(expectedSeconds, actual.TotalSeconds);
    }

    [Theory]
    [InlineData(1, 2, 3)]
    public void Add_RoundedSecond_returns_expected_value(int baseMinutes, int minutesToAdd, double expectedTotalMinutes)
    {
        // arrange
        var baseRounded = new RoundedSecond(TimeSpan.FromSeconds(baseMinutes));
        var toAdd = new RoundedSecond(TimeSpan.FromSeconds(minutesToAdd));

        // act
        var actual = baseRounded.Add(toAdd);

        // assert
        Assert.Equal(expectedTotalMinutes, actual.TotalSeconds);
    }

    [Theory]
    [InlineData(1, 1, 61)]
    public void Add_RoundedMinute_returns_expected_value(int baseSeconds, int minutesToAdd, double expectedTotalSeconds)
    {
        // arrange
        var baseRounded = new RoundedSecond(TimeSpan.FromSeconds(baseSeconds));
        var toAdd = new RoundedMinute(TimeSpan.FromMinutes(minutesToAdd));

        // act
        var actual = baseRounded.Add(toAdd);

        // assert
        Assert.Equal(expectedTotalSeconds, actual.TotalSeconds);
    }

    [Theory]
    [InlineData(3, 2, 1)]
    public void Subtract_RoundedSecond_returns_expected_value(int baseSeconds, int secondsToSubtract, double expectedSeconds)
    {
        // arrange
        var baseRounded = new RoundedSecond(TimeSpan.FromSeconds(baseSeconds));
        var toAdd = new RoundedSecond(TimeSpan.FromSeconds(secondsToSubtract));

        // act
        var actual = baseRounded.Subtract(toAdd);

        // assert
        Assert.Equal(expectedSeconds, actual.TotalSeconds);
    }

    [Theory]
    [InlineData(61, 1, 1)]
    public void Subtract_RoundedMinute_returns_expected_value(int baseSeconds, int minutesToSubtract, double expectedSeconds)
    {
        // arrange
        var baseRounded = new RoundedSecond(TimeSpan.FromSeconds(baseSeconds));
        var toAdd = new RoundedMinute(TimeSpan.FromMinutes(minutesToSubtract));

        // act
        var actual = baseRounded.Subtract(toAdd);

        // assert
        Assert.Equal(expectedSeconds, actual.TotalSeconds);
    }

    [Theory]
    [InlineData(1, 1.5, MidpointRounding.AwayFromZero, 3)]
    [InlineData(1, 1.5, MidpointRounding.ToZero, 2)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void AddSeconds_MidpointRounding_returns_expected_value(int baseSeconds, double secondsToAdd, MidpointRounding mode, double expectedSeconds)
    {
        // arrange
        var baseTime = TimeSpan.FromSeconds(baseSeconds);
        var baseRoundedSecond = new RoundedSecond(baseTime);

        // act
        var actual = baseRoundedSecond.AddSeconds(secondsToAdd, mode);

        // assert
        Assert.Equal(expectedSeconds, actual.TotalSeconds);
    }

    [Theory]
    [InlineData(1, 1.5, AbsoluteRounding.Ceiling, 3)]
    [InlineData(1, 1.5, AbsoluteRounding.Floor, 2)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void AddSeconds_AbsoluteRounding_returns_expected_value(int baseSeconds, double secondsToAdd, AbsoluteRounding mode, double expectedSeconds)
    {
        // arrange
        var baseTime = TimeSpan.FromSeconds(baseSeconds);
        var baseRoundedSecond = new RoundedSecond(baseTime);

        // act
        var actual = baseRoundedSecond.AddSeconds(secondsToAdd, mode);

        // assert
        Assert.Equal(expectedSeconds, actual.TotalSeconds);
    }

    [Theory]
    [InlineData(1, 0.025, MidpointRounding.AwayFromZero, 3)] // add 1.5s
    [InlineData(1, 0.025, MidpointRounding.ToZero, 2)] // add 1.5s
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void AddMinutes_MidpointRounding_returns_expected_value(int baseSeconds, double minutesToAdd, MidpointRounding mode, double expectedSeconds)
    {
        // arrange
        var baseTime = TimeSpan.FromSeconds(baseSeconds);
        var baseRoundedSecond = new RoundedSecond(baseTime);

        // act
        var actual = baseRoundedSecond.AddMinutes(minutesToAdd, mode);

        // assert
        Assert.Equal(expectedSeconds, actual.TotalSeconds);
    }

    [Theory]
    [InlineData(1, 0.0375, AbsoluteRounding.Ceiling, 4)] // add 2.25s
    [InlineData(1, 0.0625, AbsoluteRounding.Floor, 4)] // add 3.75s
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void AddMinutes_AbsoluteRounding_returns_expected_value(int baseSeconds, double minutesToAdd, AbsoluteRounding mode, double expectedSeconds)
    {
        // arrange
        var baseTime = TimeSpan.FromSeconds(baseSeconds);
        var baseRoundedSecond = new RoundedSecond(baseTime);

        // act
        var actual = baseRoundedSecond.AddMinutes(minutesToAdd, mode);

        // assert
        Assert.Equal(expectedSeconds, actual.TotalSeconds);
    }

    [Theory]
    [InlineData(1, 0.08375, MidpointRounding.AwayFromZero, 303)] // adding 301.5s
    [InlineData(1, 0.08375, MidpointRounding.ToZero, 302)] // adding 301.5s
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void AddHours_MidpointRounding_returns_expected_value(int baseSeconds, double hoursToAdd, MidpointRounding mode, double expectedSeconds)
    {
        // arrange
        var baseTime = TimeSpan.FromSeconds(baseSeconds);
        var baseRoundedSecond = new RoundedSecond(baseTime);

        // act
        var actual = baseRoundedSecond.AddHours(hoursToAdd, mode);

        // assert
        Assert.Equal(expectedSeconds, actual.TotalSeconds);
    }

    [Theory]
    [InlineData(1, 0.08375, AbsoluteRounding.Ceiling, 303)] // adding 301.5s
    [InlineData(1, 0.08375, AbsoluteRounding.Floor, 302)] // adding 301.5s
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void AddHours_AbsoluteRounding_returns_expected_value(int baseSeconds, double hoursToAdd, AbsoluteRounding mode, double expectedSeconds)
    {
        // arrange
        var baseTime = TimeSpan.FromSeconds(baseSeconds);
        var baseRoundedSecond = new RoundedSecond(baseTime);

        // act
        var actual = baseRoundedSecond.AddHours(hoursToAdd, mode);

        // assert
        Assert.Equal(expectedSeconds, actual.TotalSeconds);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(60, 0)]
    [InlineData(3600, 1)]
    public void Hours_returns_expected_value(int seconds, double expectedTotalHours)
    {
        // arrange
        var roundedSecond = new RoundedSecond(TimeSpan.FromSeconds(seconds));

        // act
        var actualHours = roundedSecond.Hours;

        // assert
        Assert.Equal(expectedTotalHours, actualHours);
    }

    [Theory]
    [InlineData(1, 0.016666666666666666)]
    [InlineData(90, 1.5)]
    [InlineData(120, 2)]
    public void TotalMinutes_returns_expected_value(int seconds, double expectedMinutes)
    {
        // arrange
        var roundedMinute = new RoundedSecond(TimeSpan.FromSeconds(seconds));

        // act
        var actualTotalMinutes = roundedMinute.TotalMinutes;

        // assert
        Assert.Equal(expectedMinutes, actualTotalMinutes);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(60, 1)]
    [InlineData(3600, 0)]
    public void Minutes_returns_expected_value(int seconds, double expectedMinutes)
    {
        // arrange
        var roundedSecond = new RoundedSecond(TimeSpan.FromSeconds(seconds));

        // act
        var actualMinutes = roundedSecond.Minutes;

        // assert
        Assert.Equal(expectedMinutes, actualMinutes);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(60)]
    [InlineData(120)]
    public void TotalSeconds_returns_expected_value(int seconds)
    {
        // arrange
        var roundedSecond = new RoundedSecond(TimeSpan.FromSeconds(seconds));

        // act
        var actualTotalSeconds = roundedSecond.TotalSeconds;

        // assert
        Assert.Equal(seconds, actualTotalSeconds);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(61, 1)]
    public void Seconds_returns_expected_value(int seconds, int expectedSeconds)
    {
        // arrange
        var roundedSecond = new RoundedSecond(TimeSpan.FromSeconds(seconds));

        // act
        var actualSeconds = roundedSecond.Seconds;

        // assert
        Assert.Equal(expectedSeconds, actualSeconds);
    }

    [Theory]
    [InlineData(1, 1000)]
    [InlineData(61, 61000)]
    public void TotalMilliseconds_returns_expected_value(int seconds, double expectedTotalMilliseconds)
    {
        // arrange
        var roundedSecond = new RoundedSecond(TimeSpan.FromSeconds(seconds));

        // act
        var actualTTotalMilliseconds = roundedSecond.TotalMilliseconds;

        // assert
        Assert.Equal(expectedTotalMilliseconds, actualTTotalMilliseconds);
    }

    [Theory]
    [InlineData(1, 10000000)]
    [InlineData(60, 600000000)]
    public void TimeSpan_returns_expected_value(int seconds, double expectedTimeSpanTicks)
    {
        // arrange
        var roundedSecond = new RoundedSecond(TimeSpan.FromSeconds(seconds));

        // act
        var actualTimeSpanTicks = roundedSecond.TimeSpan.Ticks;

        // assert
        Assert.Equal(expectedTimeSpanTicks, actualTimeSpanTicks);
    }
}
