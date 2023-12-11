
namespace AdventOfCodeShared;

internal class BubbleSort : ISorter
{
    public IEnumerable<T> SortEnumerable<T>(IEnumerable<T> listToSort)
    {
        T currentElement, nextElement;
        T[] sorted = [];
        for (var i = 0; i < listToSort.Count() - 1; i++)
        {
            currentElement = listToSort.ElementAt(i);
            nextElement = listToSort.ElementAt(i + 1);
        }
        return sorted;
    }
}
