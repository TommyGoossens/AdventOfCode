namespace AdventOfCodeShared
{
    public abstract class AdventOfCodeProgram
    {
        public int DayNumber { get; private init; }
        protected string[] lines { get; private set; }

        protected AdventOfCodeProgram(int dayNumber, string? text = null)
        {
            DayNumber = dayNumber;
            lines = string.IsNullOrEmpty(text) ? File.ReadAllLines(@$"Day{dayNumber}\input.txt") : text.Split(Environment.NewLine);
        }

        protected abstract string[] Run();
        public string GetAnswer()
        {
            var startTime = DateTime.Now;
            var answer = Run();
            var processingTime = DateTime.Now - startTime;

            var resultMessage = $"--- Day {DayNumber} --- {Environment.NewLine}{Environment.NewLine}";
            for (int i = 0; i < answer.Length; i++) resultMessage += $"Part {i + 1}: [{answer[i]}]{Environment.NewLine}";
            resultMessage += $"Took: {processingTime.TotalSeconds} seconds{Environment.NewLine}";
            return resultMessage;
        }
    }
}