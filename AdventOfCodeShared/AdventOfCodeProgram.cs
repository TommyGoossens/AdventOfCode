using AdventOfCodeShared.RegularExpressions;

namespace AdventOfCodeShared
{
    public abstract class AdventOfCodeProgram
    {
        public int DayNumber { get; private init; }
        protected string[] Lines { get; private init; } 

        protected AdventOfCodeProgram(string? text = null)
        {
            DayNumber = GetDayNumber();
            if (!string.IsNullOrEmpty(text)) Lines = text.Replace("\r", "").Split(Environment.NewLine);
            else
            {
                var inputPath = Path.Combine($"Day{DayNumber}", "input.txt");
                if (!File.Exists(inputPath)) throw new Exception($"Could not find input.txt for Day {DayNumber}");
                Lines = File.ReadAllLines(inputPath);
            }
        }

        protected abstract string RunPartOne();
        public abstract void RunTestsPartOne(string input, string expectedResult);

        protected abstract string RunPartTwo();
        public abstract void RunTestsPartTwo(string input, string expectedResult);

        public void RunProgramAndDisplayAnswer()
        {
            var partOne = RunProgram(RunPartOne, 1);
            var partTwo = RunProgram(RunPartTwo, 2);
            var maxWidth = GetMaxWidth(partOne.answer, partTwo.answer);

            PrintTitle(maxWidth);
            PrintResultMessageForPart(partOne, 1, maxWidth);
            PrintDivider(maxWidth);
            PrintResultMessageForPart(partTwo, 2, maxWidth);
            PrintDivider(maxWidth);
        }

        private int GetDayNumber()
        {
            var className = GetType().Name;
            var re = DigitRegularExpressions.DayNumberRegex();
            return int.Parse(re.Match(className).Value);
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

        private static int GetMaxWidth(string partOne, string partTwo)
        {
            var lengthPartOne = partOne.Split(Environment.NewLine).FirstOrDefault()?.Length ?? 0;
            var lengthPartTwo = partTwo.Split(Environment.NewLine).FirstOrDefault()?.Length ?? 0;
            return Math.Max(lengthPartOne, lengthPartTwo);
        }

        private (string answer, int ms) RunProgram(Func<string> program, int part)
        {
            string answer;
            var startTime = DateTime.Now;
            try
            {
                answer = $" Part {part}: {program()} ";
            }
            catch (Exception)
            {
                answer = $" Part {part} has not yet been coded ";
            }
            var processingTime = DateTime.Now - startTime;
            return (answer, processingTime.Milliseconds);
        }        
    }
}