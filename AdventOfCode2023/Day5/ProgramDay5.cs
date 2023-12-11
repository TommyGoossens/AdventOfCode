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

    public bool SeedIsWithinRange((long Start, long Range) pair)
    {
        var (start, range) = pair;
        var maxLocation = SourceRangeStart + RangeLength;
        return start <= maxLocation && start + range >= SourceRangeStart;
    }

    public (long Start, long End, long Offset) GetNewRange(long currentOffset = 0)
    {
        var diff = SourceRangeStart - DestinationRangeStart;
        return (DestinationRangeStart, DestinationRangeStart + RangeLength, currentOffset + diff);
    }

    public long GetNewLocation(long location)
    {
        return location + DestinationRangeStart - SourceRangeStart;
    }

    internal void Deconstruct(out long dest, out long source, out long range)
    {
        dest = DestinationRangeStart;
        source = SourceRangeStart;
        range = RangeLength;
    }

    public SeedMapping(string lineToParse)
    {
        var digits = lineToParse.ExtractNumbers<long>();
        DestinationRangeStart = digits.ElementAt(0);
        SourceRangeStart = digits.ElementAt(1);
        RangeLength = digits.ElementAt(2);
    }
}

public class ProgramDay5(string? text = null) : AdventOfCodeProgram<long>(text)
{
    private IEnumerable<SeedMapping> GetSeedMappings(IEnumerable<string> trimmed, IEnumerable<SeedHeader> headers, int headerIndex, int i)
    {
        var startingPosition = trimmed.Skip(headerIndex + 1);
        if (headerIndex != headers.Last().RowIndex) startingPosition = startingPosition.Take(headers.ElementAt(i + 1).RowIndex - headerIndex - 1);
        return startingPosition.Select(l => new SeedMapping(l));
    }

    public override long RunPartOne()
    {
        var seedsToPlant = Lines.First().Split(": ")[1].Split(' ').Select(long.Parse);
        var minimum = GetMinimumSeedLocation(seedsToPlant);
        return minimum;
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


    /**
for seeds in [[[x, x] for x in sds], [[sds[e], sds[e] + sds[e + 1] - 1] for e in range(0, len(sds), 2)]]:
            for seed_min, seed_max in seeds:
                current_ranges = [(seed_min, seed_max)]
                for block in data:
                    arrangements = [[int(x) for x in y.split()] for y in block.splitlines()[1:]]
                    next_ranges = []
                    while current_ranges:
                        start, end = current_ranges.pop(0)
                        for dest, source, rng in arrangements:
                            if source <= start and source + rng > start:
                                if end - start < rng:
                                    next_ranges.append((dest + (start - source), dest + (end - source)))
                                else:
                                    next_ranges.append((dest + (start - source), dest + (start + rng - 1 - source)))
                                    current_ranges.append((start + rng, end))
                            elif source <= end and source + rng > end:
                                next_ranges.append((dest, dest + (end - source - 1)))
                                current_ranges.append((start, source - 1))
                            elif source > start and source + rng < end:
                                next_ranges.append((dest, dest + rng - 1))
                                current_ranges.extend([(start, source - 1), (source + rng, end)])
                            else:
                                continue
                            break
                        else:
                            next_ranges.append((start, end))
                    current_ranges = next_ranges
                mins.append((min(x[0] for x in current_ranges)))
            result.append(min(mins))
    **/

    public override long RunPartTwo()
    {
        var parsed = Lines.First().Split(": ")[1].Split(' ').Select(long.Parse);
        var pairs = parsed.Select((x, i) => new { Index = i, Value = x })
        .GroupBy(x => x.Index / 2)
        .Select(x => x.Select(v => v.Value)).Select(p => (p.First(), p.Last()));
        var sections = GetSections();
        var results = new List<long>();
        foreach (var pair in pairs)
        {
            Queue<(long start, long end)> currentRanges = new();
            foreach (var s in sections)
            {
                Queue<(long start, long end)> nextRanges = new();
                currentRanges.Enqueue((pair.Item1, pair.Item1 + pair.Item2 - 1));
                while (currentRanges.Any())
                {
                    var (start, end) = currentRanges.Dequeue();
                    var loopIsBroken = false;
                    foreach (var arrangement in s)
                    {
                        var (dest, source, rng) = arrangement;
                        if (source <= start && source + rng > start)
                        {
                            if (end - start < rng) nextRanges.Enqueue((dest + (start - source), dest + (end - source)));
                            else
                            {
                                nextRanges.Enqueue((dest + (start - source), dest + (start + rng - 1 - source)));
                                currentRanges.Enqueue((start + rng, end));
                            }
                        }
                        else if (source <= end && source + rng > end)
                        {
                            nextRanges.Enqueue((dest, dest + (end - source - 1)));
                            currentRanges.Enqueue((start, source - 1));
                        }
                        else if (source > start && source + rng < end)
                        {
                            nextRanges.Enqueue((dest, dest + rng - 1));
                            currentRanges.Enqueue((start, source - 1));
                            currentRanges.Enqueue((source + rng, end));
                        }
                        else continue;
                        loopIsBroken = true;
                        break;
                    }
                    if (loopIsBroken) nextRanges.Enqueue((start, end));
                }
                currentRanges = nextRanges;
            }
            var min = currentRanges.Select(p => p.start).Min();
            results.Add(min);
        }
        return results.Where(r => r != 0).Min() - 1;
    }

    private long GetMinimumSeedLocation(IEnumerable<long> seeds)
    {
        var sections = GetSections();
        return seeds.AsParallel().Min(seed => GetNewLocation(seed, sections));
    }

    private long GetNewLocation(long seed, IEnumerable<IEnumerable<SeedMapping>> sections)
    {
        foreach (var section in sections)
        {
            var match = section.FirstOrDefault(s => s.SeedIsWithinRange(seed));
            seed = match?.GetNewLocation(seed) ?? seed;
        }
        return seed;
    }
}
