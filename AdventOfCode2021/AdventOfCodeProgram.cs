namespace AdventOfCode2021
{
    internal abstract class AdventOfCodeProgram
    {
        public int DayNumber { get; private init; }
        internal AdventOfCodeProgram(int dayNumber)
        {
            this.DayNumber = dayNumber;
        }
        protected abstract string Run();
        internal string GetAnswer()
        {
            var startTime = DateTime.Now;
            var answer = Run();            
            var processingTime = DateTime.Now - startTime;
            return $"The answer for day {DayNumber} is: [{answer}]. Took: {processingTime.TotalSeconds} seconds";
        }
    }
}
