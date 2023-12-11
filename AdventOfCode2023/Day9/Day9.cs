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

public class Day9(string? text = null) : AdventOfCodeProgram<long>(text)
{
    public override long RunPartOne()
    {
        long sum = Lines.Select(l => new History(l)).Select(h => h.GetHistoryValue()).Sum();
        return sum;
    }

    public override long RunPartTwo()
    {
        long sum = Lines.Select(l => new History(l, true)).Select(h => h.GetHistoryValue()).Sum();
        return sum;
    }
}
