namespace AdventOfCodeShared.Tests;

public class BubbleSortingTests
{
    [Theory]
    [InlineData(new int[] { 8, 1, 20, 40, 3, 7 })]
    [InlineData(new int[] { -5, -100, -2, 5, 0, 6 })]
    public void ShouldBeAbleToSortSimpleValueTypes(IEnumerable<int> source)
    {
        var result = source.Sort(SortingAlgo.Bubble);
        result.Should().BeInAscendingOrder();
    }
}
