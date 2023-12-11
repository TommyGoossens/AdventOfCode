using AdventOfCodeShared;

namespace AdventOfCode2023;

public class TestsDay6 : AdventOfCodeTestRunner<long>
{
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
}