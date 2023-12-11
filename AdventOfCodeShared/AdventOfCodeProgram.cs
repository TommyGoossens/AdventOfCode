
namespace AdventOfCodeShared;
public interface IAdventOfCodeProgram
{
    public void RunProgramAndDisplayAnswer();
    public int DayNumber { get; }

}

public abstract class AdventOfCodeTestRunner<T>
{
    public abstract void RunTestsPartOne(string input, T expectedResult);
    public abstract void RunTestsPartTwo(string input, T expectedResult);
}


public abstract class AdventOfCodeProgram<T> : IAdventOfCodeProgram
{
    public int DayNumber { get; private init; }
    protected IEnumerable<string> Lines { get; private init; }

    protected AdventOfCodeProgram(string? text = null)
    {
        DayNumber = GetDayNumber();
        if (!string.IsNullOrEmpty(text)) Lines = text.SplitOnNewLines();
        else
        {
            var inputPath = Path.Combine($"Day{DayNumber}", "Input.txt");
            if (!File.Exists(inputPath)) throw new Exception($"Could not find input.txt for Day {DayNumber}");
            Lines = File.ReadAllLines(inputPath);
        }
    }

    public abstract T RunPartOne();


    public abstract T RunPartTwo();

    public void RunProgramAndDisplayAnswer()
    {
        var partOne = RunProgram(RunPartOne, 1);
        var partTwo = RunProgram(RunPartTwo, 2);
        var maxWidth = GetMaxWidth(partOne, partTwo);

        PrintTitle(maxWidth);
        PrintResultMessageForPart(partOne, 1, maxWidth);
        PrintDivider(maxWidth);
        PrintResultMessageForPart(partTwo, 2, maxWidth);
        PrintDivider(maxWidth);
    }

    private int GetDayNumber()
    {
        var className = GetType().Name;
        return className.ExtractNumber<int>();
    }

    private void PrintTitle(int maxWidth)
    {
        var dayString = $" Day {DayNumber} ";
        var middle = maxWidth / 2;
        var leftSide = (int)Math.Floor((decimal)middle - dayString.Length / 2);
        var rightSide = (int)Math.Floor((decimal)middle - dayString.Length / 2);
        PrintText($"|{new string('-', leftSide)}", ConsoleColor.Blue);
        PrintText(dayString);
        PrintText($"{new string('-', rightSide - 1)}", ConsoleColor.Blue);
        var remainder = maxWidth - (leftSide + dayString.Length + rightSide) + 1;
        if (remainder > 0) PrintText(new string('-', remainder), ConsoleColor.Blue);
        PrintText($"|{Environment.NewLine}", ConsoleColor.Blue);
    }

    private static void PrintText(string text, ConsoleColor background = ConsoleColor.Black, ConsoleColor foreground = ConsoleColor.White)
    {
        Console.BackgroundColor = background;
        Console.ForegroundColor = foreground;
        Console.Write(text);
        Console.ResetColor();
    }

    private static void PrintDivider(int maxWidth)
    {
        var divider = $"|{new string('-', maxWidth)}|{Environment.NewLine}";
        PrintText(divider, ConsoleColor.Blue);
    }

    private static void PrintResultMessageForPart((string answer, int ms) programResult, int part, int maxWidth)
    {
        var answer = programResult.answer;
        var ms = programResult.ms;
        var answerRemainder = maxWidth - answer.Length;

        PrintText("|", ConsoleColor.Blue);
        PrintText($"{answer}{new string(' ', answerRemainder)}");
        PrintText($"|{Environment.NewLine}", ConsoleColor.Blue);

        PrintText("|", ConsoleColor.Blue);
        var timeRow = $" Part {part} took: {ms} milliseconds";
        var timeRemainder = maxWidth - timeRow.Length;
        PrintText($"{timeRow}{new string(' ', Math.Max(0, timeRemainder))}");
        PrintText($"|{Environment.NewLine}", ConsoleColor.Blue);
    }

    private static int GetMaxWidth((string answer, int ms) partOne, (string answer, int ms) partTwo)
    {
        var (answer1, time1) = partOne;
        var (answer2, time2) = partTwo;
        var lengthPartOne = answer1.Split(Environment.NewLine).FirstOrDefault()?.Length ?? answer1.Length;
        var timeRow1 = $" Part 1 took: {time1} milliseconds";
        var lengthPartTwo = answer2.Split(Environment.NewLine).FirstOrDefault()?.Length ?? answer2.Length;
        var timeRow2 = $" Part 2 took: {time2} milliseconds";
        var maxAnswerRow = Math.Max(lengthPartOne, lengthPartTwo);
        var maxTimeRow = Math.Max(timeRow1.Length, timeRow2.Length);
        return Math.Max(maxAnswerRow, maxTimeRow);
    }

    private static (string answer, int ms) RunProgram(Func<T> program, int part)
    {
        string answer;
        var startTime = DateTime.Now;
        try
        {
            answer = $" Part {part}: {program()} ";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            answer = $" Part {part} has not yet been coded ";
        }
        var processingTime = DateTime.Now - startTime;
        return (answer, processingTime.Milliseconds);
    }
}
