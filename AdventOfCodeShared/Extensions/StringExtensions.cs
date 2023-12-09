using System.Text.RegularExpressions;

namespace AdventOfCodeShared;

public static class StringExtensions
{
    public static IEnumerable<T> ExtractNumbers<T>(this string line) where T : IParsable<T> => Regex.Matches(line, @"-?\d+").Select(m => T.Parse(m.Value, null));
    public static T ExtractNumber<T>(this string line) where T : IParsable<T> => T.Parse(Regex.Match(line, @"-?\d+").Value, null);

    public static IEnumerable<string> SplitOnNewLines(this string text) => text.Replace("\r", "").Split(Environment.NewLine);
}
