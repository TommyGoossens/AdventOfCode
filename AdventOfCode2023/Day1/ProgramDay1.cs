using System.Text.RegularExpressions;
using AdventOfCodeShared;

namespace AdventOfCode2023;
public class ProgramDay1(string? text = null) : AdventOfCodeProgram<int>(text)
{
    public override int RunPartOne()
    {
        var sum = Lines
        .Select(line => Regex.Replace(line, "[a-z]", ""))
        .Select(line => $"{line.First()}{line.Last()}")
        .Select(int.Parse)
        .Sum();
        return sum;
    }

    public override int RunPartTwo()
    {
        var sum = Lines
        .Select(ReplaceOverlappingDigitsAndRemoveUnwantedChars)
        .Select(line => $"{line.First()}{line.Last()}")
        .Select(int.Parse)
        .Sum();

        return sum;
    }

    private string ReplaceOverlappingDigitsAndRemoveUnwantedChars(string digit)
    {
        var result = replaces.Aggregate(digit, (current, replace) => current.Replace(replace.Key, replace.Value));
        return Regex.Replace(result, "[a-z]", "");
    }

    Dictionary<string, string> replaces = new()
        {
            {"one", "o1e"},
            {"two", "t2o"},
            {"three", "t3e"},
            {"four", "4"},
            {"five", "5e"},
            {"six", "6"},
            {"seven", "7n"},
            {"eight", "e8t"},
            {"nine", "n9e"},
        };
}