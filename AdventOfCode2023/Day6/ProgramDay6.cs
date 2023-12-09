using AdventOfCodeShared;

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
        long nrOfWaysToWin = 0;
        for (var ms = 0; ms <= Time; ms++)
        {
            if ((Time - ms) * ms > Distance) nrOfWaysToWin++;
            else if (RaceIsOverHalftime(ms)) return nrOfWaysToWin;
        }
        return nrOfWaysToWin;
    }

    private bool RaceIsOverHalftime(int elapsedTime) => elapsedTime >= Time / 2;
}

public class ProgramDay6 : AdventOfCodeProgram<long>
{
    public ProgramDay6(string? text = null) : base(text)
    {
    }

    [Theory]
    [InlineData(@"Time:      7  15   30
Distance:  9  40  200", 288)]
    public override void RunTestsPartOne(string input, long expectedResult)
    {
        new ProgramDay6(input).RunPartOne().Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(@"Time:      7  15   30
Distance:  9  40  200", 71503)]
    public override void RunTestsPartTwo(string input, long expectedResult)
    {
        new ProgramDay6(input).RunPartTwo().Should().Be(expectedResult);
    }

    protected override long RunPartOne()
    {
        var times = Lines.First().Split(" ").Where(s => int.TryParse(s, out _)).Select(int.Parse);
        var distances = Lines.Last().Split(" ").Where(s => int.TryParse(s, out _)).Select(int.Parse);
        var races = times.Select((t, i) => new Race(t, distances.ElementAt(i)));
        var wins = races.Select(r => r.GetNumberOfWaysToWin());
        return wins.Aggregate((a, x) => a * x);
    }

    protected override long RunPartTwo()
    {
        var startTimeParsing = DateTime.Now;
        var timeString = string.Join("", Lines.First().Split(" ").Where(s => int.TryParse(s, out _)));
        var distanceString = string.Join("", Lines.Last().Split(" ").Where(s => int.TryParse(s, out _)));
        var time = long.Parse(timeString);
        var distance = long.Parse(distanceString);
        var race = new Race(time, distance);
        Console.WriteLine($"Parsing took {(DateTime.Now - startTimeParsing).Milliseconds}");
        var startTimeRace = DateTime.Now;
        var wins = race.GetNumberOfWaysToWin();
        Console.WriteLine($"Race took {(DateTime.Now - startTimeRace).Milliseconds}");
        return wins;
    }
}
