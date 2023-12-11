namespace AdventOfCodeShared;

public enum SortingAlgo
{
    Bubble,
    Quick,
    Merge
}

public interface ISorter
{
    public abstract IEnumerable<T> SortEnumerable<T>(IEnumerable<T> listToSort);


}
