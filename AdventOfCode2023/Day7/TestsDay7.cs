using AdventOfCodeShared;

namespace AdventOfCode2023;

public class TestsDay7 : AdventOfCodeTestRunner<long>
{
    [Theory]
    [InlineData(@"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483", 6440)]
    public override void RunTestsPartOne(string input, long expectedResult)
    {
        new ProgramDay7(input).RunPartOne().Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(@"2345A 1
Q2KJJ 13
Q2Q2Q 19
T3T3J 17
T3Q33 11
2345J 3
J345A 2
32T3K 5
T55J5 29
KK677 7
KTJJT 34
QQQJA 31
JJJJJ 37
JAAAA 43
AAAAJ 59
AAAAA 61
2AAAA 23
2JJJJ 53
JJJJ2 41", 6839)]
    public override void RunTestsPartTwo(string input, long expectedResult)
    {
        new ProgramDay7(input).RunPartTwo().Should().Be(expectedResult);
    }
}