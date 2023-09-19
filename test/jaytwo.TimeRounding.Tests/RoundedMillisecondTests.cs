using Xunit;

namespace jaytwo.TimeRounding.Tests;

public class RoundedMillisecondTests
{
    [Theory]
    [InlineData(1, "0:00:00.001")]
    [InlineData(1123, "0:00:01.123")]
    [InlineData(100000, "0:01:40.000")]
    [InlineData(86400001, "24:00:00.001")]
    public void ToString_returns_expected_value(int milliseconds, string expectedString)
    {
        // arrange
        var roundedMinute = new RoundedMillisecond(TimeSpan.FromMilliseconds(milliseconds));

        // act
        var actualString = roundedMinute.ToString();

        // assert
        Assert.Equal(expectedString, actualString);
    }

    [Theory]
    [InlineData(1500, MidpointRounding.ToZero, 1)]
    [InlineData(1500, MidpointRounding.AwayFromZero, 2)]
    public void ToRoundedSecond_MidpointRounding_returns_expected_value(int milliseconds, MidpointRounding mode, int expectedSeconds)
    {
        // arrange
        var roundedMillisecond = RoundedMillisecond.FromMillisecondsExact(milliseconds);

        // act
        var roundedSecond = roundedMillisecond.ToRoundedSecond(mode);

        // assert
        Assert.Equal(expectedSeconds, roundedSecond.TotalSeconds);
    }

    [Theory]
    [InlineData(1001, AbsoluteRounding.Ceiling, 2)]
    [InlineData(1999, AbsoluteRounding.Floor, 1)]
    public void ToRoundedSecond_AbsoluteRounding_returns_expected_value(int milliseconds, AbsoluteRounding mode, int expectedSeconds)
    {
        // arrange
        var roundedMillisecond = RoundedMillisecond.FromMillisecondsExact(milliseconds);

        // act
        var roundedSecond = roundedMillisecond.ToRoundedSecond(mode);

        // assert
        Assert.Equal(expectedSeconds, roundedSecond.TotalSeconds);
    }

    [Theory]
    [InlineData(90000, MidpointRounding.ToZero, 1)]
    [InlineData(90000, MidpointRounding.AwayFromZero, 2)]
    public void ToRoundedMinute_MidpointRounding_returns_expected_value(int milliseconds, MidpointRounding mode, int expectedMinutes)
    {
        // arrange
        var roundedMillisecond = RoundedMillisecond.FromMillisecondsExact(milliseconds);

        // act
        var roundedMinute = roundedMillisecond.ToRoundedMinute(mode);

        // assert
        Assert.Equal(expectedMinutes, roundedMinute.TotalMinutes);
    }

    [Theory]
    [InlineData(1, AbsoluteRounding.Ceiling, 1)]
    [InlineData(119999, AbsoluteRounding.Floor, 1)]
    public void ToRoundedMinute_AbsoluteRounding_returns_expected_value(int milliseconds, AbsoluteRounding mode, int expectedMinutes)
    {
        // arrange
        var roundedMillisecond = RoundedMillisecond.FromMillisecondsExact(milliseconds);

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
        var roundedMinute = new RoundedMillisecond(TimeSpan.FromMilliseconds(value));

        // act
        var negative = roundedMinute.Negative();

        // assert
        Assert.Equal(expectedNegative, negative.TotalMilliseconds);
    }

    [Theory]
    [InlineData(1, 0.00000027777777777777776)]
    [InlineData(86400001, 24.00000027777777777777776)]
    public void TotalHours_returns_expected_value(int milliseconds, double expectedTotalHours)
    {
        // arrange
        var roundedMinute = new RoundedMillisecond(TimeSpan.FromMilliseconds(milliseconds));

        // act
        var actualTotalHours = roundedMinute.TotalHours;

        // assert
        Assert.Equal(expectedTotalHours, actualTotalHours);
    }

    [Theory]
    [InlineData(1800000, 0, MidpointRounding.AwayFromZero, 1)]
    [InlineData(1800000, 0, MidpointRounding.ToZero, 0)]
    [InlineData(1800000, 1, MidpointRounding.AwayFromZero, 0.5)]
    [InlineData(1800000, 1, MidpointRounding.ToZero, 0.5)]
    public void GetRoundedTotalHours_MidpointRounding_returns_expected_value(int milliseconds, int digits, MidpointRounding mode, double expectedTotalHours)
    {
        // arrange
        var roundedMinute = new RoundedMillisecond(TimeSpan.FromMilliseconds(milliseconds));

        // act
        var actualTotalHours = roundedMinute.GetRoundedTotalHours(digits, mode);

        // assert
        Assert.Equal(expectedTotalHours, actualTotalHours);
    }

    [Theory]
    [InlineData(1740000, 0, AbsoluteRounding.Ceiling, 1)]
    [InlineData(1860000, 0, AbsoluteRounding.Floor, 0)]
    [InlineData(1740000, 2, AbsoluteRounding.Ceiling, 0.49)]
    [InlineData(1860000, 2, AbsoluteRounding.Floor, 0.51)]
    public void GetRoundedTotalHours_AbsoluteRounding_returns_expected_value(int milliseconds, int digits, AbsoluteRounding mode, double expectedTotalHours)
    {
        // arrange
        var roundedMinute = new RoundedMillisecond(TimeSpan.FromMilliseconds(milliseconds));

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
    public void RoundMilliseconds_MidpointRounding_returns_expected_value(double milliseconds, MidpointRounding mode, double expectedMilliseconds)
    {
        // arrange
        var inputTime = TimeSpan.FromMilliseconds(milliseconds);

        // act
        var actualTime = RoundedMillisecond.RoundMilliseconds(inputTime, mode);

        // assert
        Assert.Equal(expectedMilliseconds, actualTime.TotalMilliseconds);
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
    public void RoundMilliseconds_AbsoluteRounding_returns_expected_value(double milliseconds, AbsoluteRounding mode, double expectedMilliseconds)
    {
        // arrange
        var inputTime = TimeSpan.FromMilliseconds(milliseconds);

        // act
        var actualTime = RoundedMillisecond.RoundMilliseconds(inputTime, mode);

        // assert
        Assert.Equal(expectedMilliseconds, actualTime.TotalMilliseconds);
    }

    [Theory]
    [InlineData(0.5, MidpointRounding.AwayFromZero, 1)]
    [InlineData(0.5, MidpointRounding.ToZero, 0)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void Constructor_MidpointRounding_returns_expected_value(double milliseconds, MidpointRounding mode, double expectedMilliseconds)
    {
        // arrange
        var time = TimeSpan.FromMilliseconds(milliseconds);

        // act
        var actual = new RoundedMillisecond(time, mode);

        // assert
        Assert.Equal(expectedMilliseconds, actual.TotalMilliseconds);
    }

    [Theory]
    [InlineData(0.4, AbsoluteRounding.Ceiling, 1)]
    [InlineData(0.6, AbsoluteRounding.Floor, 0)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void Constructor_AbsoluteRounding_returns_expected_value(double milliseconds, AbsoluteRounding mode, double expectedMilliseconds)
    {
        // arrange
        var time = TimeSpan.FromMilliseconds(milliseconds);

        // act
        var actual = new RoundedMillisecond(time, mode);

        // assert
        Assert.Equal(expectedMilliseconds, actual.TotalMilliseconds);
    }

    [Theory]
    [InlineData(0.5, MidpointRounding.AwayFromZero, 1)]
    [InlineData(0.5, MidpointRounding.ToZero, 0)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void FromMilliseconds_MidpointRounding_returns_expected_value(double seconds, MidpointRounding mode, double expectedMilliseconds)
    {
        // arrange

        // act
        var actualTime = RoundedMillisecond.FromMilliseconds(seconds, mode);

        // assert
        Assert.Equal(expectedMilliseconds, actualTime.TotalMilliseconds);
    }

    [Theory]
    [InlineData(0.4, AbsoluteRounding.Ceiling, 1)]
    [InlineData(0.6, AbsoluteRounding.Floor, 0)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void FromMilliseconds_AbsoluteRounding_returns_expected_value(double seconds, AbsoluteRounding mode, double expectedMilliseconds)
    {
        // arrange

        // act
        var actualTime = RoundedMillisecond.FromMilliseconds(seconds, mode);

        // assert
        Assert.Equal(expectedMilliseconds, actualTime.TotalMilliseconds);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(-1)]
    public void FromMillisecondsExact_returns_expected_value(long totalMilliseconds)
    {
        // arrange

        // act
        var actualTime = RoundedMillisecond.FromMillisecondsExact(totalMilliseconds);

        // assert
        Assert.Equal(totalMilliseconds, actualTime.TotalMilliseconds);
    }

    [Theory]
    [InlineData(0.0005, MidpointRounding.AwayFromZero, 1)]
    [InlineData(0.0005, MidpointRounding.ToZero, 0)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void FromSeconds_MidpointRounding_returns_expected_value(double seconds, MidpointRounding mode, double expectedMilliseconds)
    {
        // arrange

        // act
        var actualTime = RoundedMillisecond.FromSeconds(seconds, mode);

        // assert
        Assert.Equal(expectedMilliseconds, actualTime.TotalMilliseconds);
    }

    [Theory]
    [InlineData(0.0004, AbsoluteRounding.Ceiling, 1)]
    [InlineData(0.0006, AbsoluteRounding.Floor, 0)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void FromSeconds_AbsoluteRounding_returns_expected_value(double seconds, AbsoluteRounding mode, double expectedMilliseconds)
    {
        // arrange

        // act
        var actualTime = RoundedMillisecond.FromSeconds(seconds, mode);

        // assert
        Assert.Equal(expectedMilliseconds, actualTime.TotalMilliseconds);
    }

    [Theory]
    [InlineData(0.001, 1)]
    [InlineData(-0.001, -1)]
    [InlineData(1.001, 1001)] // apparently a double has a hard time with 1s + 1ms
    [InlineData(-1.001, -1001)] // apparently a double has a hard time with 1s + 1ms
    [InlineData(1.002, 1002)]
    [InlineData(-1.002, -1002)]
    public void FromSecondsExact_returns_expected_value(double totalSeconds, double expectedMilliseconds)
    {
        // arrange

        // act
        var actualTime = RoundedMillisecond.FromSecondsExact(totalSeconds);

        // assert
        Assert.Equal(expectedMilliseconds, actualTime.TotalMilliseconds);
    }

    [Theory]
    [InlineData(0.000025, MidpointRounding.AwayFromZero, 2)] // 1.5ms
    [InlineData(0.000025, MidpointRounding.ToZero, 1)] // 1.5ms
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void FromMinutes_MidpointRounding_returns_expected_value(double minutes, MidpointRounding mode, double expectedMilliseconds)
    {
        // arrange

        // act
        var actualTime = RoundedMillisecond.FromMinutes(minutes, mode);

        // assert
        Assert.Equal(expectedMilliseconds, actualTime.TotalMilliseconds);
    }

    [Theory]
    [InlineData(0.0000375, AbsoluteRounding.Ceiling, 3)] // 2.25ms
    [InlineData(0.0000625, AbsoluteRounding.Floor, 3)] // 3.75ms
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void FromMinutes_AbsoluteRounding_returns_expected_value(double minutes, AbsoluteRounding mode, double expectedMilliseconds)
    {
        // arrange

        // act
        var actualTime = RoundedMillisecond.FromMinutes(minutes, mode);

        // assert
        Assert.Equal(expectedMilliseconds, actualTime.TotalMilliseconds);
    }

    [Theory]
    [InlineData(1d / 60d, 1000)]
    public void FromMinutesExact_returns_expected_value(double totalMinutes, double expectedMilliseconds)
    {
        // arrange

        // act
        var actualTime = RoundedMillisecond.FromMinutesExact(totalMinutes);

        // assert
        Assert.Equal(expectedMilliseconds, actualTime.TotalMilliseconds);
    }

    [Theory]
    [InlineData(0.00008375, MidpointRounding.AwayFromZero, 302)] //301.5s
    [InlineData(0.00008375, MidpointRounding.ToZero, 301)] //301.5s
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void FromHours_MidpointRounding_returns_expected_value(double hours, MidpointRounding mode, double expectedMilliseconds)
    {
        // arrange

        // act
        var actualTime = RoundedMillisecond.FromHours(hours, mode);

        // assert
        Assert.Equal(expectedMilliseconds, actualTime.TotalMilliseconds);
    }

    [Theory]
    [InlineData(0.00008375, AbsoluteRounding.Ceiling, 302)] //301.5s
    [InlineData(0.00008375, AbsoluteRounding.Floor, 301)] //301.5s
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void FromHours_AbsoluteRounding_returns_expected_value(double hours, AbsoluteRounding mode, double expectedMilliseconds)
    {
        // arrange

        // act
        var actualTime = RoundedMillisecond.FromHours(hours, mode);

        // assert
        Assert.Equal(expectedMilliseconds, actualTime.TotalMilliseconds);
    }

    [Theory]
    [InlineData(1d / 60d / 60d, 1000)]
    public void FromHoursExact_returns_expected_value(double totalHours, double expectedMilliseconds)
    {
        // arrange

        // act
        var actualTime = RoundedMillisecond.FromHoursExact(totalHours);

        // assert
        Assert.Equal(expectedMilliseconds, actualTime.TotalMilliseconds);
    }

    [Theory]
    [InlineData(1, 1.5, MidpointRounding.AwayFromZero, 3)]
    [InlineData(1, 1.5, MidpointRounding.ToZero, 2)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void Add_MidpointRounding_returns_expected_value(int baseMilliseconds, double millisecondsToAdd, MidpointRounding mode, double expectedTotalMilliseconds)
    {
        // arrange
        var baseTime = TimeSpan.FromMilliseconds(baseMilliseconds);
        var baseRoundedMillisecond = new RoundedMillisecond(baseTime);
        var timeToAdd = TimeSpan.FromMilliseconds(millisecondsToAdd);

        // act
        var actual = baseRoundedMillisecond.Add(timeToAdd, mode);

        // assert
        Assert.Equal(expectedTotalMilliseconds, actual.TotalMilliseconds);
    }

    [Theory]
    [InlineData(1, 1.5, AbsoluteRounding.Ceiling, 3)]
    [InlineData(1, 1.5, AbsoluteRounding.Floor, 2)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void Add_AbsoluteRounding_returns_expected_value(int baseMilliseconds, double millisecondsToAdd, AbsoluteRounding mode, double expectedTotalMilliseconds)
    {
        // arrange
        var baseTime = TimeSpan.FromMilliseconds(baseMilliseconds);
        var baseRoundedMillisecond = new RoundedMillisecond(baseTime);
        var timeToAdd = TimeSpan.FromMilliseconds(millisecondsToAdd);

        // act
        var actual = baseRoundedMillisecond.Add(timeToAdd, mode);

        // assert
        Assert.Equal(expectedTotalMilliseconds, actual.TotalMilliseconds);
    }

    [Theory]
    [InlineData(1, 2, 3)]
    public void Add_RoundedMillisecond_returns_expected_value(int baseMilliseconds, int minutesToAdd, double expectedTotalMilliseconds)
    {
        // arrange
        var baseRounded = new RoundedMillisecond(TimeSpan.FromMilliseconds(baseMilliseconds));
        var toAdd = new RoundedMillisecond(TimeSpan.FromMilliseconds(minutesToAdd));

        // act
        var actual = baseRounded.Add(toAdd);

        // assert
        Assert.Equal(expectedTotalMilliseconds, actual.TotalMilliseconds);
    }

    [Theory]
    [InlineData(1, 2, 2001)]
    public void Add_RoundedSecond_returns_expected_value(int baseMilliseconds, int secondsToAdd, double expectedTotalMilliseconds)
    {
        // arrange
        var baseRounded = new RoundedMillisecond(TimeSpan.FromMilliseconds(baseMilliseconds));
        var toAdd = new RoundedSecond(TimeSpan.FromSeconds(secondsToAdd));

        // act
        var actual = baseRounded.Add(toAdd);

        // assert
        Assert.Equal(expectedTotalMilliseconds, actual.TotalMilliseconds);
    }

    [Theory]
    [InlineData(1, 1, 60001)]
    public void Add_RoundedMinute_returns_expected_value(int baseMilliseconds, int minutesToAdd, double expectedTotalMilliseconds)
    {
        // arrange
        var baseRounded = new RoundedMillisecond(TimeSpan.FromMilliseconds(baseMilliseconds));
        var toAdd = new RoundedMinute(TimeSpan.FromMinutes(minutesToAdd));

        // act
        var actual = baseRounded.Add(toAdd);

        // assert
        Assert.Equal(expectedTotalMilliseconds, actual.TotalMilliseconds);
    }

    [Theory]
    [InlineData(3, 2, 1)]
    public void Subtract_RoundedMillisecond_returns_expected_value(int baseMilliseconds, int minutesToSubtract, double expectedTotalMilliseconds)
    {
        // arrange
        var baseRounded = new RoundedMillisecond(TimeSpan.FromMilliseconds(baseMilliseconds));
        var toAdd = new RoundedMillisecond(TimeSpan.FromMilliseconds(minutesToSubtract));

        // act
        var actual = baseRounded.Subtract(toAdd);

        // assert
        Assert.Equal(expectedTotalMilliseconds, actual.TotalMilliseconds);
    }

    [Theory]
    [InlineData(2001, 2, 1)]
    public void Subtract_RoundedSecond_returns_expected_value(int baseMilliseconds, int secondsToSubtract, double expectedTotalMilliseconds)
    {
        // arrange
        var baseRounded = new RoundedMillisecond(TimeSpan.FromMilliseconds(baseMilliseconds));
        var toAdd = new RoundedSecond(TimeSpan.FromSeconds(secondsToSubtract));

        // act
        var actual = baseRounded.Subtract(toAdd);

        // assert
        Assert.Equal(expectedTotalMilliseconds, actual.TotalMilliseconds);
    }

    [Theory]
    [InlineData(60001, 1, 1)]
    public void Subtract_RoundedMinute_returns_expected_value(int baseMilliseconds, int minutesToSubtract, double expectedTotalMilliseconds)
    {
        // arrange
        var baseRounded = new RoundedMillisecond(TimeSpan.FromMilliseconds(baseMilliseconds));
        var toAdd = new RoundedMinute(TimeSpan.FromMinutes(minutesToSubtract));

        // act
        var actual = baseRounded.Subtract(toAdd);

        // assert
        Assert.Equal(expectedTotalMilliseconds, actual.TotalMilliseconds);
    }

    [Theory]
    [InlineData(1, 0.0015, MidpointRounding.AwayFromZero, 3)]
    [InlineData(1, 0.0015, MidpointRounding.ToZero, 2)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void AddSeconds_MidpointRounding_returns_expected_value(int baseMilliseconds, double secondsToAdd, MidpointRounding mode, double expectedTotalMilliseconds)
    {
        // arrange
        var baseTime = TimeSpan.FromMilliseconds(baseMilliseconds);
        var baseRoundedMillisecond = new RoundedMillisecond(baseTime);

        // act
        var actual = baseRoundedMillisecond.AddSeconds(secondsToAdd, mode);

        // assert
        Assert.Equal(expectedTotalMilliseconds, actual.TotalMilliseconds);
    }

    [Theory]
    [InlineData(1, 0.0015, AbsoluteRounding.Ceiling, 3)]
    [InlineData(1, 0.0015, AbsoluteRounding.Floor, 2)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void AddSeconds_AbsoluteRounding_returns_expected_value(int baseMilliseconds, double secondsToAdd, AbsoluteRounding mode, double expectedTotalMilliseconds)
    {
        // arrange
        var baseTime = TimeSpan.FromMilliseconds(baseMilliseconds);
        var baseRoundedMillisecond = new RoundedMillisecond(baseTime);

        // act
        var actual = baseRoundedMillisecond.AddSeconds(secondsToAdd, mode);

        // assert
        Assert.Equal(expectedTotalMilliseconds, actual.TotalMilliseconds);
    }

    [Theory]
    [InlineData(1, 0.000025, MidpointRounding.AwayFromZero, 3)] // add 1.5s
    [InlineData(1, 0.000025, MidpointRounding.ToZero, 2)] // add 1.5s
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void AddMinutes_MidpointRounding_returns_expected_value(int baseMilliseconds, double minutesToAdd, MidpointRounding mode, double expectedTotalMilliseconds)
    {
        // arrange
        var baseTime = TimeSpan.FromMilliseconds(baseMilliseconds);
        var baseRoundedMillisecond = new RoundedMillisecond(baseTime);

        // act
        var actual = baseRoundedMillisecond.AddMinutes(minutesToAdd, mode);

        // assert
        Assert.Equal(expectedTotalMilliseconds, actual.TotalMilliseconds);
    }

    [Theory]
    [InlineData(1, 0.0000375, AbsoluteRounding.Ceiling, 4)] // add 2.25s
    [InlineData(1, 0.0000625, AbsoluteRounding.Floor, 4)] // add 3.75s
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void AddMinutes_AbsoluteRounding_returns_expected_value(int baseMilliseconds, double minutesToAdd, AbsoluteRounding mode, double expectedTotalMilliseconds)
    {
        // arrange
        var baseTime = TimeSpan.FromMilliseconds(baseMilliseconds);
        var baseRoundedMillisecond = new RoundedMillisecond(baseTime);

        // act
        var actual = baseRoundedMillisecond.AddMinutes(minutesToAdd, mode);

        // assert
        Assert.Equal(expectedTotalMilliseconds, actual.TotalMilliseconds);
    }

    [Theory]
    [InlineData(1, 0.00008375, MidpointRounding.AwayFromZero, 303)] // adding 301.5s
    [InlineData(1, 0.00008375, MidpointRounding.ToZero, 302)] // adding 301.5s
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void AddHours_MidpointRounding_returns_expected_value(int baseMilliseconds, double hoursToAdd, MidpointRounding mode, double expectedTotalMilliseconds)
    {
        // arrange
        var baseTime = TimeSpan.FromMilliseconds(baseMilliseconds);
        var baseRoundedMillisecond = new RoundedMillisecond(baseTime);

        // act
        var actual = baseRoundedMillisecond.AddHours(hoursToAdd, mode);

        // assert
        Assert.Equal(expectedTotalMilliseconds, actual.TotalMilliseconds);
    }

    [Theory]
    [InlineData(1, 0.00008375, AbsoluteRounding.Ceiling, 303)] // adding 301.5s
    [InlineData(1, 0.00008375, AbsoluteRounding.Floor, 302)] // adding 301.5s
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void AddHours_AbsoluteRounding_returns_expected_value(int baseMilliseconds, double hoursToAdd, AbsoluteRounding mode, double expectedTotalMilliseconds)
    {
        // arrange
        var baseTime = TimeSpan.FromMilliseconds(baseMilliseconds);
        var baseRoundedMillisecond = new RoundedMillisecond(baseTime);

        // act
        var actual = baseRoundedMillisecond.AddHours(hoursToAdd, mode);

        // assert
        Assert.Equal(expectedTotalMilliseconds, actual.TotalMilliseconds);
    }

    [Theory]
    [InlineData(1000, 0)]
    [InlineData(60000, 0)]
    [InlineData(3600000, 1)]
    public void Hours_returns_expected_value(int milliseconds, double expectedTotalHours)
    {
        // arrange
        var roundedSecond = new RoundedMillisecond(TimeSpan.FromMilliseconds(milliseconds));

        // act
        var actualHours = roundedSecond.Hours;

        // assert
        Assert.Equal(expectedTotalHours, actualHours);
    }

    [Theory]
    [InlineData(1, 0.000016666666666666666)]
    [InlineData(90000, 1.5)]
    [InlineData(120000, 2)]
    public void TotalMinutes_returns_expected_value(int milliseconds, double expectedMinutes)
    {
        // arrange
        var roundedMinute = new RoundedMillisecond(TimeSpan.FromMilliseconds(milliseconds));

        // act
        var actualTotalMinutes = roundedMinute.TotalMinutes;

        // assert
        Assert.Equal(expectedMinutes, actualTotalMinutes);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(1000, 0)]
    [InlineData(60000, 1)]
    [InlineData(3600000, 0)]
    public void Minutes_returns_expected_value(int milliseconds, double expectedMinutes)
    {
        // arrange
        var roundedSecond = new RoundedMillisecond(TimeSpan.FromMilliseconds(milliseconds));

        // act
        var actualMinutes = roundedSecond.Minutes;

        // assert
        Assert.Equal(expectedMinutes, actualMinutes);
    }

    [Theory]
    [InlineData(1, 0.001)]
    [InlineData(1001, 1.001)]
    [InlineData(60001, 60.001)]
    public void TotalSeconds_returns_expected_value(int milliseconds, double expectedSeconds)
    {
        // arrange
        var roundedSecond = new RoundedMillisecond(TimeSpan.FromMilliseconds(milliseconds));

        // act
        var actualTotalSeconds = roundedSecond.TotalSeconds;

        // assert
        Assert.Equal(expectedSeconds, actualTotalSeconds);
    }

    [Theory]
    [InlineData(1000, 1)]
    [InlineData(60000, 0)]
    public void Seconds_returns_expected_value(int milliseconds, int expectedSeconds)
    {
        // arrange
        var roundedSecond = new RoundedMillisecond(TimeSpan.FromMilliseconds(milliseconds));

        // act
        var actualSeconds = roundedSecond.Seconds;

        // assert
        Assert.Equal(expectedSeconds, actualSeconds);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2000)]
    public void TotalMilliseconds_returns_expected_value(int milliseconds)
    {
        // arrange
        var roundedSecond = new RoundedMillisecond(TimeSpan.FromMilliseconds(milliseconds));

        // act
        var actualTotalMilliseconds = roundedSecond.TotalMilliseconds;

        // assert
        Assert.Equal(milliseconds, actualTotalMilliseconds);
    }

    [Theory]
    [InlineData(50, 50)]
    [InlineData(60050, 50)]
    public void Milliseconds_returns_expected_value(int milliseconds, int expectedMilliseconds)
    {
        // arrange
        var roundedSecond = new RoundedMillisecond(TimeSpan.FromMilliseconds(milliseconds));

        // act
        var actualMilliseconds = roundedSecond.Milliseconds;

        // assert
        Assert.Equal(expectedMilliseconds, actualMilliseconds);
    }

    [Theory]
    [InlineData(1, 10000)]
    public void TimeSpan_returns_expected_value(int milliseconds, double expectedTimeSpanTicks)
    {
        // arrange
        var roundedSecond = new RoundedMillisecond(TimeSpan.FromMilliseconds(milliseconds));

        // act
        var actualTimeSpanTicks = roundedSecond.TimeSpan.Ticks;

        // assert
        Assert.Equal(expectedTimeSpanTicks, actualTimeSpanTicks);
    }
}
