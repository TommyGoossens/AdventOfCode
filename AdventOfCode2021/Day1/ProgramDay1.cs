namespace AdventOfCode2021.Day1
{
    internal class ProgramDay1 : AdventOfCodeProgram
    {
        public ProgramDay1() : base(1)
        {
        }

        protected override string Run()
        {
            string[] lines = File.ReadAllLines(@"Day1\input.txt");
            var nrOfIncreases = GetAnswerPart1(lines);
            var nrOfLargerSums = GetAnswerPart2(lines);
            
            return $"there are {nrOfIncreases} measurements that are larger than the previous. There are {nrOfLargerSums} sums that are larger than the previous sum";
        }

        private int GetAnswerPart1(string[] lines)
        {
            int? previousDepth = null;
            int nrOfIncreases = 0;
            foreach (var line in lines)
            {
                int.TryParse(line, out var currDepth);
                if (previousDepth == null) { previousDepth = currDepth; continue; }
                if (currDepth > previousDepth) nrOfIncreases++;
                previousDepth = currDepth;
            }

            return nrOfIncreases;
        }

        private int GetAnswerPart2(string[] lines)
        {
            int nrOfLargerSums = 0;
            int? previousDepth = null;
            for (int i = 0; i < lines.Length; i++)
            {
                var sumOfWindow = 0;
                try
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var line = lines[i + j];
                        int.TryParse(line, out var currDepth);
                        sumOfWindow += currDepth;

                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine($"Section at row {i} contains less than 3 measurements");
                    break;
                }

                if (previousDepth == null) { previousDepth = sumOfWindow; continue; }
                if (sumOfWindow > previousDepth) nrOfLargerSums++;
                previousDepth = sumOfWindow;
            }

            return nrOfLargerSums;
        }
    }
}
