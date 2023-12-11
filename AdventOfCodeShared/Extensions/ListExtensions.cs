namespace AdventOfCodeShared;

public static class ListExtensions
{
    public static IEnumerable<TSource> Sort<TSource>(this IEnumerable<TSource> source, SortingAlgo sortingAlgo)
    {
        ISorter sorter = sortingAlgo switch
        {
            SortingAlgo.Bubble => new BubbleSort(),
            SortingAlgo.Quick => throw new NotImplementedException(),
            SortingAlgo.Merge => throw new NotImplementedException(),
            _ => throw new NotImplementedException($"Sorter for algo {sortingAlgo} has not (yet) been implemented"),
        };
        return sorter.SortEnumerable(source);
    }
}
