namespace AdventOfCodeShared
{
    public static class AdventOfCodeProgramRunner
    {
        public static void RunPrograms(IEnumerable<AdventOfCodeProgram> programs)
        {
            var startTime = DateTime.Now;
            foreach (var program in programs.OrderBy(p => p.DayNumber))
            {
                Console.WriteLine();
                program.RunProgramAndDisplayAnswer();
                Console.WriteLine();
            }

            var processingTime = DateTime.Now - startTime;
            Console.WriteLine();
            Console.WriteLine($"It took {processingTime.TotalMilliseconds} milliseconds to run {programs.Count()} Advent of Code programs");
            Console.WriteLine();
        }
    }
}