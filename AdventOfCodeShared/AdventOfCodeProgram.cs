namespace AdventOfCodeShared
{
    public abstract class AdventOfCodeProgram
    {
        public int DayNumber { get; private init; }
        protected string[] lines { get; private set; }
        protected AdventOfCodeProgram(int dayNumber)
        {
            this.DayNumber = dayNumber;
            lines = File.ReadAllLines(@$"Day{dayNumber}\input.txt");
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

        protected void CreateTestInput(string testInput)
        {
            lines = testInput.Split(Environment.NewLine);
        }
    }
}