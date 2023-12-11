using System.Text.RegularExpressions;
using AdventOfCodeShared;

namespace AdventOfCode2023;

public class MapTraverser
{
    private readonly Dictionary<string, (string Left, string Right)> map = new();

    public MapTraverser(IEnumerable<string> lines)
    {
        foreach (var l in lines)
        {
            var matches = Regex.Matches(l, @"([A-Z]{3})|(\d{2}[A-Z]{1})");
            matches.Should().HaveCount(3);
            var key = matches[0].Value;
            var left = matches[1].Value;
            var right = matches[2].Value;
            map[key] = (left, right);
        }
    }

    public long TraverseToAndCountSteps(string startRegex, string desinationSuffix, IEnumerable<char> instuctionSet)
    {
        var startingNodeKeys = map.Where(m => Regex.Match(m.Key, startRegex).Success).Select(k => k.Key);
        var runners = map.Where(m => startingNodeKeys.Contains(m.Key)).Select(n => n.Value);
        var result = new List<long>();

        foreach (var startNode in runners)
        {
            var steps = 0;
            var currentNode = startNode;
            while (!DestinationReached(instuctionSet, desinationSuffix, ref currentNode, ref steps)) ;
            result.Add(steps);
        }
        return result.Aggregate(CalculateLeastCommonMultiple);
    }

    private bool DestinationReached(IEnumerable<char> instuctionSet, string desinationSuffix, ref (string Left, string Right) currentNode, ref int steps)
    {
        foreach (var instuction in instuctionSet)
        {
            steps++;
            var nextNode = instuction == 'L' ? currentNode.Left : currentNode.Right;
            if (nextNode.EndsWith(desinationSuffix)) return true;
            currentNode = map[nextNode];
        }
        return false;
    }
    private static long CalculateLeastCommonMultiple(long a, long b) => Math.Abs(a * b) / GCD(a, b);

    private static long GCD(long a, long b) => b == 0 ? a : GCD(b, a % b);
}

public class Day8(string? text = null) : AdventOfCodeProgram<long>(text)
{
    public override long RunPartOne()
    {
        var instuctionSet = Lines.First().Select(c => c);
        var map = new MapTraverser(Lines.Skip(2));
        var steps = map.TraverseToAndCountSteps(@"[A]{3}", "Z", instuctionSet);
        return steps;
    }

    public override long RunPartTwo()
    {
        var instuctionSet = Lines.First().Select(c => c);
        var map = new MapTraverser(Lines.Skip(2));
        var steps = map.TraverseToAndCountSteps(@"(\d{2}|[A-Z]{2})[A]{1}", "Z", instuctionSet);
        return steps;
    }
}
