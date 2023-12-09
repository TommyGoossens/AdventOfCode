using System.Text.RegularExpressions;

namespace AdventOfCodeShared;

public static class StringExtensions
{
    public static IEnumerable<T> ExtractNumbers<T>(this string line) where T : IParsable<T> => Regex.Matches(line, @"-?\d+").Select(m => T.Parse(m.Value, null));
}
