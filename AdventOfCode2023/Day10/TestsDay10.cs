using AdventOfCodeShared;

namespace AdventOfCode2023;

public class TestsDay10 : AdventOfCodeTestRunner<int>
{
    [Theory]
    [InlineData(@".....
.S-7.
.|.|.
.L-J.
.....", 4)]
    [InlineData(@"..F7.
.FJ|.
SJ.L7
|F--J
LJ...", 8)]
    public override void RunTestsPartOne(string input, int expectedResult)
    {
        new Day10(input).RunPartOne().Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(@".....
.S-7.
.|.|.
.L-J.
.....", 4)]
    public override void RunTestsPartTwo(string input, int expectedResult)
    {
        new Day10(input).RunPartTwo().Should().Be(expectedResult);
    }
}