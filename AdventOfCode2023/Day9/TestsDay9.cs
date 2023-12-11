using AdventOfCodeShared;

namespace AdventOfCode2023;

public class TestsDay9 : AdventOfCodeTestRunner<long>
{
    [Theory]
    [InlineData(@"11 9 7 5 3 1 -1 -3 -5 -7 -9 -11 -13 -15 -17 -19 -21 -23 -25 -27 -29
0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45", 83)]
    public override void RunTestsPartOne(string input, long expectedResult)
    {
        new Day9(input).RunPartOne().Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(@"0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45", 2)]
    public override void RunTestsPartTwo(string input, long expectedResult)
    {
        new Day9(input).RunPartTwo().Should().Be(expectedResult);
    }
}