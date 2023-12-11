using AdventOfCodeShared;

namespace AdventOfCode2023;

public class TestsDay8 : AdventOfCodeTestRunner<long>
{
    [Theory]
    [InlineData(@"RL

    AAA = (BBB, CCC)
    BBB = (DDD, EEE)
    CCC = (ZZZ, GGG)
    DDD = (DDD, DDD)
    EEE = (EEE, EEE)
    GGG = (GGG, GGG)
    ZZZ = (ZZZ, ZZZ)", 2)]
    [InlineData(@"LLR

    AAA = (BBB, BBB)
    BBB = (AAA, ZZZ)
    ZZZ = (ZZZ, ZZZ)", 6)]
    public override void RunTestsPartOne(string input, long expectedResult)
    {
        new Day8(input).RunPartOne().Should().Be(expectedResult);
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
    XXX = (XXX, XXX)", 6)]
    public override void RunTestsPartTwo(string input, long expectedResult)
    {
        new Day8(input).RunPartTwo().Should().Be(expectedResult);
    }
}