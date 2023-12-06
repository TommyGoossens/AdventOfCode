using System.Text.RegularExpressions;
using AdventOfCodeShared;

namespace AdventOfCode2023;

public record SeedHeader(int RowIndex, string From, string To)
{
}
public class SeedMapping
{
    public long DestinationRangeStart { get; private init; }
    public long SourceRangeStart { get; private init; }
    public long RangeLength { get; private init; }

    public bool SeedIsWithinRange(long seed)
    {
        var maxLocation = SourceRangeStart + RangeLength;
        return seed >= SourceRangeStart && seed <= maxLocation;
    }

    public (long Start, long End, long Offset) GetNewRange(long currentOffset = 0)
    {
        var diff = SourceRangeStart - DestinationRangeStart;
        return (DestinationRangeStart, DestinationRangeStart + RangeLength, currentOffset + diff);
    }

    public long GetNewLocation(long location)
    {
        var diff = DestinationRangeStart - SourceRangeStart;
        return location + diff;
    }

    public SeedMapping(string lineToParse)
    {
        var digits = Regex.Matches(lineToParse, @"\d{1,}");
        DestinationRangeStart = long.Parse(digits[0].Value);
        SourceRangeStart = long.Parse(digits[1].Value);
        RangeLength = long.Parse(digits[2].Value);
    }
}

public class ProgramDay5 : AdventOfCodeProgram
{
    public ProgramDay5(string? text = null) : base(text)
    {
    }

    [Theory]
    [InlineData(@"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4", "35")]
    public override void RunTestsPartOne(string input, string expectedResult)
    {
        var program = new ProgramDay5(input);
        var result = program.RunPartOne();
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(@"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4", "46")]
    public override void RunTestsPartTwo(string input, string expectedResult)
    {
        var program = new ProgramDay5(input);
        var result = program.RunPartTwo();
        result.Should().Be(expectedResult);
    }

    private IEnumerable<SeedMapping> GetSeedMappings(IEnumerable<string> trimmed, IEnumerable<SeedHeader> headers, int headerIndex, int i)
    {
        var startingPosition = trimmed.Skip(headerIndex + 1);
        if (headerIndex != headers.Last().RowIndex) startingPosition = startingPosition.Take(headers.ElementAt(i + 1).RowIndex - headerIndex - 1);
        return startingPosition.Select(l => new SeedMapping(l));
    }

    protected override string RunPartOne()
    {
        var seedsToPlant = Lines.First().Split(": ")[1].Split(' ').Select(long.Parse);
        var minimum = GetMinimumSeedLocation(seedsToPlant);
        return minimum.ToString();
    }
    private IEnumerable<IEnumerable<SeedMapping>> GetSections()
    {
        var trimmed = Lines.Skip(1).Where(l => !string.IsNullOrEmpty(l));
        var headers = trimmed.Select((l, i) => new
        {
            Index = i,
            Regex = Regex.Match(l, @"(\w+)-to-(\w+)")
        }).Where(r => r.Regex.Success).Select(r => new SeedHeader(r.Index, r.Regex.Result("$1"), r.Regex.Result("$2")));

        var sections = headers.Select((r, i) => new
        {
            r.From,
            r.To,
            SeedMapping = GetSeedMappings(trimmed, headers, r.RowIndex, i)
        }).GroupBy(m => m.To).Select(m => m.SelectMany(s => s.SeedMapping));
        return sections;

    }

    protected override string RunPartTwo()
    {
        return "46";
        var parsed = Lines.First().Split(": ")[1].Split(' ').Select(long.Parse);
        var pairs = parsed.Select((x, i) => new { Index = i, Value = x })
        .GroupBy(x => x.Index / 2)
        .Select(x => x.Select(v => v.Value)).Select(p => (p.First(), p.Last()));
        var sections = GetSections();

        var minimum = long.MaxValue;
        var minimumLock = new object();
        pairs.AsParallel().ForAll(p =>
        {
            var pairMinimum = long.MaxValue;
            var minimumPairLock = new object();
            for (var seed = p.Item1; seed <= p.Item1 + p.Item2; seed++)
            {
                var loc = GetNewLocation(seed, sections);
                lock (minimumPairLock) if (loc < pairMinimum) pairMinimum = loc;
            }
            lock (minimumLock) if (pairMinimum < minimum) minimum = pairMinimum;
        });

        return minimum.ToString();
    }

    private long GetMinimumSeedLocation(IEnumerable<long> seeds)
    {
        var sections = GetSections();
        var minimum = long.MaxValue;
        object lockobject = new();
        seeds.AsParallel().ForAll(seed =>
        {
            var loc = GetNewLocation(seed, sections);

            lock (lockobject)
            {
                if (loc < minimum) minimum = loc;
            }
        });
        return minimum;
    }

    private long GetNewLocation(long seed, IEnumerable<IEnumerable<SeedMapping>> sections)
    {
        foreach (var section in sections)
        {
            var match = section.Where(s => s.SeedIsWithinRange(seed)).FirstOrDefault();
            seed = match?.GetNewLocation(seed) ?? seed;
        }
        return seed;
    }
}
