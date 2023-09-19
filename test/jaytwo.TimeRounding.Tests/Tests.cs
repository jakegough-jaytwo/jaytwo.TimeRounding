using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace jaytwo.TimeRounding.Tests;

public class Tests
{
    [Fact(Skip = "only run manually")]
    public void millisecond_precision_confirmed()
    {
        // this takes about 25s per day
        // 10 days takes 3.1m
        // 500 days takes  557 min

        Parallel.ForEach(Enumerable.Range(0, 500), days =>
        {
            for (int hours = 0; hours < 24; hours++)
            {
                for (int minutes = 0; minutes < 60; minutes++)
                {
                    for (int seconds = 0; seconds < 60; seconds++)
                    {
                        for (int milliseconds = 0; milliseconds < 1000; milliseconds++)
                        {
                            var time = TimeSpan.FromDays(days)
                                .Add(TimeSpan.FromHours(hours))
                                .Add(TimeSpan.FromMinutes(minutes))
                                .Add(TimeSpan.FromSeconds(seconds))
                                .Add(TimeSpan.FromMilliseconds(milliseconds));

                            var fromHours = RoundedMillisecond.FromHoursExact(time.TotalHours);
                            var fromMinute = RoundedMillisecond.FromMinutesExact(time.TotalMinutes);
                            var fromSecond = RoundedMillisecond.FromSecondsExact(time.TotalSeconds);

                            Assert.Equal(fromHours.TotalHours, fromMinute.TotalHours);
                            Assert.Equal(fromSecond.TotalHours, fromMinute.TotalHours);
                        }
                    }
                }
            }
        });
    }
}
