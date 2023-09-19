using Xunit;

namespace jaytwo.TimeRounding.Tests;

public class RoundedMinuteTests
{
    [Theory]
    [InlineData(1, "0:01")]
    [InlineData(100, "1:40")]
    [InlineData(525601, "8760:01")]
    public void ToString_returns_expected_value(int minutes, string expectedString)
    {
        // arrange
        var roundedMinute = new RoundedMinute(TimeSpan.FromMinutes(minutes));

        // act
        var actualString = roundedMinute.ToString();

        // assert
        Assert.Equal(expectedString, actualString);
    }

    [Theory]
    [InlineData(1, 60000)]
    [InlineData(10, 600000)]
    public void ToRoundedMillisecond_returns_expected_value(int minutes, int expectedMilliseconds)
    {
        // arrange
        var roundedMinute = RoundedMinute.FromMinutesExact(minutes);

        // act
        var roundedMillisecond = roundedMinute.ToRoundedMillisecond();

        // assert
        Assert.Equal(expectedMilliseconds, roundedMillisecond.TotalMilliseconds);
    }

    [Theory]
    [InlineData(1, 60)]
    [InlineData(10, 600)]
    public void ToRoundedSecond_returns_expected_value(int minutes, int expectedSeconds)
    {
        // arrange
        var roundedMinute = RoundedMinute.FromMinutesExact(minutes);

        // act
        var roundedSecond = roundedMinute.ToRoundedSecond();

        // assert
        Assert.Equal(expectedSeconds, roundedSecond.TotalSeconds);
    }

    [Theory]
    [InlineData(1, -1)]
    [InlineData(-1, 1)]
    public void Negative_returns_expected_value(int value, double expectedNegative)
    {
        // arrange
        var roundedMinute = new RoundedMinute(TimeSpan.FromMinutes(value));

        // act
        var negative = roundedMinute.Negative();

        // assert
        Assert.Equal(expectedNegative, negative.TotalMinutes);
    }

    [Theory]
    [InlineData(1, 0.016666666666666666)]
    [InlineData(2, 0.033333333333333333)]
    [InlineData(90, 1.5)]
    [InlineData(119, 1.9833333333333334)]
    [InlineData(120, 2)]
    [InlineData(121, 2.0166666666666666)]
    public void TotalHours_returns_expected_value(int minutes, double expectedTotalHours)
    {
        // arrange
        var roundedMinute = new RoundedMinute(TimeSpan.FromMinutes(minutes));

        // act
        var actualTotalHours = roundedMinute.TotalHours;

        // assert
        Assert.Equal(expectedTotalHours, actualTotalHours);
    }

    [Theory]
    [InlineData(30, 0, MidpointRounding.AwayFromZero, 1)]
    [InlineData(30, 0, MidpointRounding.ToZero, 0)]
    [InlineData(30, 1, MidpointRounding.AwayFromZero, 0.5)]
    [InlineData(30, 1, MidpointRounding.ToZero, 0.5)]
    public void GetRoundedTotalHours_MidpointRounding_returns_expected_value(int minutes, int digits, MidpointRounding mode, double expectedTotalHours)
    {
        // arrange
        var roundedMinute = new RoundedMinute(TimeSpan.FromMinutes(minutes));

        // act
        var actualTotalHours = roundedMinute.GetRoundedTotalHours(digits, mode);

        // assert
        Assert.Equal(expectedTotalHours, actualTotalHours);
    }

    [Theory]
    [InlineData(29, 0, AbsoluteRounding.Ceiling, 1)]
    [InlineData(31, 0, AbsoluteRounding.Floor, 0)]
    [InlineData(29, 2, AbsoluteRounding.Ceiling, 0.49)]
    [InlineData(31, 2, AbsoluteRounding.Floor, 0.51)]
    public void GetRoundedTotalHours_AbsoluteRounding_returns_expected_value(int minutes, int digits, AbsoluteRounding mode, double expectedTotalHours)
    {
        // arrange
        var roundedMinute = new RoundedMinute(TimeSpan.FromMinutes(minutes));

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
    public void RoundMinutes_MidpointRounding_returns_expected_value(double minutes, MidpointRounding mode, double expectedMinutes)
    {
        // arrange
        var inputTime = TimeSpan.FromMinutes(minutes);

        // act
        var actualTime = RoundedMinute.RoundMinutes(inputTime, mode);

        // assert
        Assert.Equal(expectedMinutes, actualTime.TotalMinutes);
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
    public void RoundMinutes_AbsoluteRounding_returns_expected_value(double minutes, AbsoluteRounding mode, double expectedMinutes)
    {
        // arrange
        var inputTime = TimeSpan.FromMinutes(minutes);

        // act
        var actualTime = RoundedMinute.RoundMinutes(inputTime, mode);

        // assert
        Assert.Equal(expectedMinutes, actualTime.TotalMinutes);
    }

    [Theory]
    [InlineData(0.5, MidpointRounding.AwayFromZero, 1)]
    [InlineData(0.5, MidpointRounding.ToZero, 0)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void Constructor_MidpointRounding_returns_expected_value(double minutes, MidpointRounding mode, double expectedMinutes)
    {
        // arrange
        var time = TimeSpan.FromMinutes(minutes);

        // act
        var actual = new RoundedMinute(time, mode);

        // assert
        Assert.Equal(expectedMinutes, actual.TotalMinutes);
    }

    [Theory]
    [InlineData(0.4, AbsoluteRounding.Ceiling, 1)]
    [InlineData(0.6, AbsoluteRounding.Floor, 0)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void Constructor_AbsoluteRounding_returns_expected_value(double minutes, AbsoluteRounding mode, double expectedMinutes)
    {
        // arrange
        var time = TimeSpan.FromMinutes(minutes);

        // act
        var actual = new RoundedMinute(time, mode);

        // assert
        Assert.Equal(expectedMinutes, actual.TotalMinutes);
    }

    [Theory]
    [InlineData(0.5, MidpointRounding.AwayFromZero, 1)]
    [InlineData(0.5, MidpointRounding.ToZero, 0)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void FromMinutes_MidpointRounding_returns_expected_value(double minutes, MidpointRounding mode, double expectedMinutes)
    {
        // arrange

        // act
        var actualTime = RoundedMinute.FromMinutes(minutes, mode);

        // assert
        Assert.Equal(expectedMinutes, actualTime.TotalMinutes);
    }

    [Theory]
    [InlineData(0.4, AbsoluteRounding.Ceiling, 1)]
    [InlineData(0.6, AbsoluteRounding.Floor, 0)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void FromMinutes_AbsoluteRounding_returns_expected_value(double minutes, AbsoluteRounding mode, double expectedMinutes)
    {
        // arrange

        // act
        var actualTime = RoundedMinute.FromMinutes(minutes, mode);

        // assert
        Assert.Equal(expectedMinutes, actualTime.TotalMinutes);
    }

    [Theory]
    [InlineData(0.025, MidpointRounding.AwayFromZero, 2)] //90s
    [InlineData(0.025, MidpointRounding.ToZero, 1)] //90s
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void FromHours_MidpointRounding_returns_expected_value(double hours, MidpointRounding mode, double expectedMinutes)
    {
        // arrange

        // act
        var actualTime = RoundedMinute.FromHours(hours, mode);

        // assert
        Assert.Equal(expectedMinutes, actualTime.TotalMinutes);
    }

    [Theory]
    [InlineData(0.025, AbsoluteRounding.Ceiling, 2)] //90s
    [InlineData(0.025, AbsoluteRounding.Floor, 1)] //90s
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void FromHours_AbsoluteRounding_returns_expected_value(double hours, AbsoluteRounding mode, double expectedMinutes)
    {
        // arrange

        // act
        var actualTime = RoundedMinute.FromHours(hours, mode);

        // assert
        Assert.Equal(expectedMinutes, actualTime.TotalMinutes);
    }

    [Theory]
    [InlineData(1d / 60d, 1)]
    public void FromHoursExact_returns_expected_value(double totalHours, double expectedMinutes)
    {
        // arrange

        // act
        var actualTime = RoundedMinute.FromHoursExact(totalHours);

        // assert
        Assert.Equal(expectedMinutes, actualTime.TotalMinutes);
    }

    [Theory]
    [InlineData(1, 1.5, MidpointRounding.AwayFromZero, 3)]
    [InlineData(1, 1.5, MidpointRounding.ToZero, 2)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void Add_MidpointRounding_returns_expected_value(int baseMinutes, double minutesToAdd, MidpointRounding mode, double expectedMinutes)
    {
        // arrange
        var baseTime = TimeSpan.FromMinutes(baseMinutes);
        var baseRoundedMinute = new RoundedMinute(baseTime);
        var timeToAdd = TimeSpan.FromMinutes(minutesToAdd);

        // act
        var actual = baseRoundedMinute.Add(timeToAdd, mode);

        // assert
        Assert.Equal(expectedMinutes, actual.TotalMinutes);
    }

    [Theory]
    [InlineData(1, 1.5, AbsoluteRounding.Ceiling, 3)]
    [InlineData(1, 1.5, AbsoluteRounding.Floor, 2)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void Add_AbsoluteRounding_returns_expected_value(int baseMinutes, double minutesToAdd, AbsoluteRounding mode, double expectedMinutes)
    {
        // arrange
        var baseTime = TimeSpan.FromMinutes(baseMinutes);
        var baseRoundedMinute = new RoundedMinute(baseTime);
        var timeToAdd = TimeSpan.FromMinutes(minutesToAdd);

        // act
        var actual = baseRoundedMinute.Add(timeToAdd, mode);

        // assert
        Assert.Equal(expectedMinutes, actual.TotalMinutes);
    }

    [Theory]
    [InlineData(1, 2, 3)]
    public void Add_RoundedMinute_returns_expected_value(int baseMinutes, int minutesToAdd, double expectedTotalMinutes)
    {
        // arrange
        var baseRounded = new RoundedMinute(TimeSpan.FromMinutes(baseMinutes));
        var toAdd = new RoundedMinute(TimeSpan.FromMinutes(minutesToAdd));

        // act
        var actual = baseRounded.Add(toAdd);

        // assert
        Assert.Equal(expectedTotalMinutes, actual.TotalMinutes);
    }

    [Theory]
    [InlineData(3, 2, 1)]
    public void Subtract_RoundedMinute_returns_expected_value(int baseMinutes, int minutesToSubtract, double expectedTotalMinutes)
    {
        // arrange
        var baseRounded = new RoundedMinute(TimeSpan.FromMinutes(baseMinutes));
        var toAdd = new RoundedMinute(TimeSpan.FromMinutes(minutesToSubtract));

        // act
        var actual = baseRounded.Subtract(toAdd);

        // assert
        Assert.Equal(expectedTotalMinutes, actual.TotalMinutes);
    }

    [Theory]
    [InlineData(1, 1.5, MidpointRounding.AwayFromZero, 3)]
    [InlineData(1, 1.5, MidpointRounding.ToZero, 2)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void AddMinutes_MidpointRounding_returns_expected_value(int baseMinutes, double minutesToAdd, MidpointRounding mode, double expectedMinutes)
    {
        // arrange
        var baseTime = TimeSpan.FromMinutes(baseMinutes);
        var baseRoundedMinute = new RoundedMinute(baseTime);

        // act
        var actual = baseRoundedMinute.AddMinutes(minutesToAdd, mode);

        // assert
        Assert.Equal(expectedMinutes, actual.TotalMinutes);
    }

    [Theory]
    [InlineData(1, 1.5, AbsoluteRounding.Ceiling, 3)]
    [InlineData(1, 1.5, AbsoluteRounding.Floor, 2)]
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void AddMinutes_AbsoluteRounding_returns_expected_value(int baseMinutes, double minutesToAdd, AbsoluteRounding mode, double expectedMinutes)
    {
        // arrange
        var baseTime = TimeSpan.FromMinutes(baseMinutes);
        var baseRoundedMinute = new RoundedMinute(baseTime);

        // act
        var actual = baseRoundedMinute.AddMinutes(minutesToAdd, mode);

        // assert
        Assert.Equal(expectedMinutes, actual.TotalMinutes);
    }

    [Theory]
    [InlineData(1, 0.025, MidpointRounding.AwayFromZero, 3)] // adding 90s
    [InlineData(1, 0.025, MidpointRounding.ToZero, 2)] // adding 90s
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void AddHours_MidpointRounding_returns_expected_value(int baseMinutes, double hoursToAdd, MidpointRounding mode, double expectedMinutes)
    {
        // arrange
        var baseTime = TimeSpan.FromMinutes(baseMinutes);
        var baseRoundedMinute = new RoundedMinute(baseTime);

        // act
        var actual = baseRoundedMinute.AddHours(hoursToAdd, mode);

        // assert
        Assert.Equal(expectedMinutes, actual.TotalMinutes);
    }

    [Theory]
    [InlineData(1, 0.025, AbsoluteRounding.Ceiling, 3)] // adding 90s
    [InlineData(1, 0.025, AbsoluteRounding.Floor, 2)] // adding 90s
    // we don't need to test every variation, we just need to verify it's considering the mode
    public void AddHours_AbsoluteRounding_returns_expected_value(int baseMinutes, double hoursToAdd, AbsoluteRounding mode, double expectedMinutes)
    {
        // arrange
        var baseTime = TimeSpan.FromMinutes(baseMinutes);
        var baseRoundedMinute = new RoundedMinute(baseTime);

        // act
        var actual = baseRoundedMinute.AddHours(hoursToAdd, mode);

        // assert
        Assert.Equal(expectedMinutes, actual.TotalMinutes);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(2, 0)]
    [InlineData(90, 1)]
    [InlineData(119, 1)]
    [InlineData(120, 2)]
    [InlineData(121, 2)]
    public void Hours_returns_expected_value(int minutes, double expectedTotalHours)
    {
        // arrange
        var roundedMinute = new RoundedMinute(TimeSpan.FromMinutes(minutes));

        // act
        var actualHours = roundedMinute.Hours;

        // assert
        Assert.Equal(expectedTotalHours, actualHours);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(90)]
    [InlineData(119)]
    [InlineData(120)]
    [InlineData(121)]
    public void TotalMinutes_returns_expected_value(int minutes)
    {
        // arrange
        var roundedMinute = new RoundedMinute(TimeSpan.FromMinutes(minutes));

        // act
        var actualTotalMinutes = roundedMinute.TotalMinutes;

        // assert
        Assert.Equal(minutes, actualTotalMinutes);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(90, 30)]
    [InlineData(119, 59)]
    [InlineData(120, 0)]
    [InlineData(121, 1)]
    public void Minutes_returns_expected_value(int minutes, double expectedMinutes)
    {
        // arrange
        var roundedMinute = new RoundedMinute(TimeSpan.FromMinutes(minutes));

        // act
        var actualMinutes = roundedMinute.Minutes;

        // assert
        Assert.Equal(expectedMinutes, actualMinutes);
    }

    [Theory]
    [InlineData(1, 60)]
    [InlineData(2, 120)]
    [InlineData(60, 3600)]
    [InlineData(120, 7200)]
    public void TotalSeconds_returns_expected_value(int minutes, double expectedTotalSeconds)
    {
        // arrange
        var roundedMinute = new RoundedMinute(TimeSpan.FromMinutes(minutes));

        // act
        var actualTotalSeconds = roundedMinute.TotalSeconds;

        // assert
        Assert.Equal(expectedTotalSeconds, actualTotalSeconds);
    }

    [Theory]
    [InlineData(1, 60000)]
    [InlineData(2, 120000)]
    [InlineData(60, 3600000)]
    [InlineData(120, 7200000)]
    public void TotalMilliseconds_returns_expected_value(int minutes, double expectedTotalMilliseconds)
    {
        // arrange
        var roundedMinute = new RoundedMinute(TimeSpan.FromMinutes(minutes));

        // act
        var actualTTotalMilliseconds = roundedMinute.TotalMilliseconds;

        // assert
        Assert.Equal(expectedTotalMilliseconds, actualTTotalMilliseconds);
    }

    [Theory]
    [InlineData(1, 60)]
    [InlineData(2, 120)]
    [InlineData(60, 3600)]
    [InlineData(120, 7200)]
    public void TimeSpan_returns_expected_value(int minutes, double expectedTimeSpanTotalSeconds)
    {
        // arrange
        var roundedMinute = new RoundedMinute(TimeSpan.FromMinutes(minutes));

        // act
        var actualTimeSpanTotalSeconds = roundedMinute.TimeSpan.TotalSeconds;

        // assert
        Assert.Equal(expectedTimeSpanTotalSeconds, actualTimeSpanTotalSeconds);
    }
}
