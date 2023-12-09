using AdventOfCodeShared;

namespace AdventOfCode2023;

internal class HistorySection
{
    public HistorySection(IEnumerable<long> numbers)
    {
        this.numbers = numbers.ToList();
    }

    private readonly List<long> numbers;

    public HistorySection(HistorySection previous)
    {
        var prevNumbers = previous.numbers;
        numbers = prevNumbers.Take(prevNumbers.Count - 1)
        .Select((n, i) => prevNumbers.ElementAt(i + 1) - n).ToList();
    }

    public bool ContainsOnlyZeros() => numbers.All(n => n == 0);

    public void AddNumber(HistorySection? sectionBelow = null)
    {
        if (ContainsOnlyZeros()) { numbers.Add(0); return; }
        else if (sectionBelow == null) throw new ArgumentException($"Given section is null, while this is needed to calculate the next number");
        numbers.Add(numbers.Last() + sectionBelow.numbers.Last());
    }

    internal long GetSectionValue() => numbers.Last();
}

internal class History
{
    internal History(string line, bool reversed = false)
    {
        var numbers = line.ExtractNumbers<long>();
        if (reversed) numbers = numbers.Reverse();
        sections.Add(new HistorySection(numbers));
        while (!sections.Last().ContainsOnlyZeros()) sections.Add(new HistorySection(sections.Last()));
        UpdateHistorySections();
    }

    private readonly List<HistorySection> sections = new();

    private void UpdateHistorySections()
    {
        sections.Reverse();
        foreach (var (section, i) in sections.Select((s, i) => (s, i)))
        {
            if (i == 0) section.AddNumber();
            else section.AddNumber(sections.ElementAt(i - 1));
        }
        sections.Reverse();
    }

    internal long GetHistoryValue() => sections.First().GetSectionValue();
}

public class Day9 : AdventOfCodeProgram
{
    public Day9(string? text = null) : base(text)
    {
    }

    [Theory]
    [InlineData(@"11 9 7 5 3 1 -1 -3 -5 -7 -9 -11 -13 -15 -17 -19 -21 -23 -25 -27 -29
0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45", "83")]
    public override void RunTestsPartOne(string input, string expectedResult)
    {
        var program = new Day9(input);
        var result = program.RunPartOne();
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(@"0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45", "2")]
    public override void RunTestsPartTwo(string input, string expectedResult)
    {
        var program = new Day9(input);
        var result = program.RunPartTwo();
        result.Should().Be(expectedResult);
    }

    protected override string RunPartOne()
    {
        long sum = Lines.Select(l => new History(l)).Select(h => h.GetHistoryValue()).Sum();
        return sum.ToString();
    }

    protected override string RunPartTwo()
    {
        long sum = Lines.Select(l => new History(l, true)).Select(h => h.GetHistoryValue()).Sum();
        return sum.ToString();
    }
}
