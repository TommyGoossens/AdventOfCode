﻿using AdventOfCodeShared;

namespace AdventOfCode2023;

public class Race
{
    public Race(long time, long distance)
    {
        Time = time;
        Distance = distance;
    }

    public long Time { get; private init; }
    public long Distance { get; private init; }
    public long GetNumberOfWaysToWin()
    {
        var multiplier = 0;
        long nrOfWaysToWin = 0;
        for (int ms = 0; ms <= Time; ms++)
        {
            var timeRemaining = Time - ms;
            if (ms + timeRemaining > Time) break;
            var distanceMoved = timeRemaining * multiplier;
            if (distanceMoved > Distance) nrOfWaysToWin++;
            else
            {
                var halfTimePassed = ms >= Time / 2;
                if (halfTimePassed) break;
            }
            multiplier++;
        }
        return nrOfWaysToWin;
    }
}

public class ProgramDay6 : AdventOfCodeProgram
{
    public ProgramDay6(string? text = null) : base(text)
    {
    }

    [Theory]
    [InlineData(@"Time:      7  15   30
Distance:  9  40  200", "288")]
    public override void RunTestsPartOne(string input, string expectedResult)
    {
        var program = new ProgramDay6(input);
        var result = program.RunPartOne();
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(@"Time:      7  15   30
Distance:  9  40  200", "71503")]
    public override void RunTestsPartTwo(string input, string expectedResult)
    {
        var program = new ProgramDay6(input);
        var result = program.RunPartTwo();
        result.Should().Be(expectedResult);
    }

    protected override string RunPartOne()
    {
        var times = Lines[0].Split(" ").Where(s => int.TryParse(s, out _)).Select(int.Parse);
        var distances = Lines[1].Split(" ").Where(s => int.TryParse(s, out _)).Select(int.Parse);
        var races = times.Select((t, i) => new Race(t, distances.ElementAt(i)));
        var wins = races.Select(r => r.GetNumberOfWaysToWin());
        return wins.Aggregate((a, x) => a * x).ToString();
    }

    protected override string RunPartTwo()
    {
        var timeString = string.Join("", Lines[0].Split(" ").Where(s => int.TryParse(s, out _)));
        var distanceString = string.Join("", Lines[1].Split(" ").Where(s => int.TryParse(s, out _)));
        var time = long.Parse(timeString);
        var distance = long.Parse(distanceString);
        var race = new Race(time, distance);
        return race.GetNumberOfWaysToWin().ToString();
    }
}
