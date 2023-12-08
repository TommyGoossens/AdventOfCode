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

    public int TraverseToAndCountSteps(string startRegex, string desinationSuffix, IEnumerable<char> instuctionSet)
    {
        var steps = 0;
        var startingNodeKeys = map.Where(m => Regex.Match(m.Key, startRegex).Success).Select(k => k.Key);

        Console.WriteLine($"Starting nodes are: {string.Join(" - ", startingNodeKeys)}");

        var runners = map.Where(m => startingNodeKeys.Contains(m.Key)).Select((n) => (n.Value, n.Key)).ToArray();

        while (runners.Select(g => g.Key).Any(g => !g.EndsWith(desinationSuffix)) && steps < int.MaxValue)
        {
            foreach (var instuction in instuctionSet)
            {
                steps++;
                for (var i = 0; i < runners.Length; i++)
                {
                    var (node, _) = runners[i];
                    var nextNode = instuction switch
                    {
                        'L' => node.Left,
                        'R' => node.Right,
                        _ => throw new NotImplementedException(),
                    };
                    runners[i].Value = map[nextNode];
                    runners[i].Key = nextNode;
                }
            }
        }
        return steps;
    }


}

public class Day8 : AdventOfCodeProgram
{
    public Day8(string? text = null) : base(text)
    {
    }

    [Theory]
    [InlineData(@"RL

    AAA = (BBB, CCC)
    BBB = (DDD, EEE)
    CCC = (ZZZ, GGG)
    DDD = (DDD, DDD)
    EEE = (EEE, EEE)
    GGG = (GGG, GGG)
    ZZZ = (ZZZ, ZZZ)", "2")]
    [InlineData(@"LLR

    AAA = (BBB, BBB)
    BBB = (AAA, ZZZ)
    ZZZ = (ZZZ, ZZZ)", "6")]
    public override void RunTestsPartOne(string input, string expectedResult)
    {
        var program = new Day8(input);
        var result = program.RunPartOne();
        result.Should().Be(expectedResult);
    }
    [Theory]
    [InlineData(@"LR

    11A = (11B, XXX)
    11B = (XXX, 11Z)
    11Z = (11B, XXX)
    22A = (22B, XXX)
    22B = (22C, 22C)
    22C = (22Z, 22Z)
    22Z = (22B, 22B)
    XXX = (XXX, XXX)", "6")]
    public override void RunTestsPartTwo(string input, string expectedResult)
    {
        var program = new Day8(input);
        var result = program.RunPartTwo();
        result.Should().Be(expectedResult);
    }

    protected override string RunPartOne()
    {
        var instuctionSet = Lines[0].Select(c => c);
        var map = new MapTraverser(Lines.Skip(2));
        var steps = map.TraverseToAndCountSteps(@"[A]{3}", "Z", instuctionSet);
        return steps.ToString();
    }

    protected override string RunPartTwo()
    {
        var instuctionSet = Lines[0].Select(c => c);
        var map = new MapTraverser(Lines.Skip(2));
        var steps = map.TraverseToAndCountSteps(@"(\d{2}|[A-Z]{2})[A]{1}", "Z", instuctionSet);
        return steps.ToString();
    }
}
