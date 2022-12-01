using AdventOfCodeShared;

namespace AdventOfCode2021.Day2
{
    internal class ProgramDay2 : AdventOfCodeProgram
    {
        public ProgramDay2() : base(2)
        {
        }

        protected override string[] Run()
        {
            int horizontalPos = 0;
            int part1Depth = 0;
            int part2Depth = 0;
            int aim = 0;
            foreach (var line in lines)
            {
                var direction = line.Split(' ')[0];
                var unitsString = line.Split(' ')[1];
                var units = int.Parse(unitsString);
                switch (direction)
                {
                    case "forward":
                        horizontalPos += units;
                        part2Depth += aim * units;
                        break;
                    case "down":
                        part1Depth += units;
                        aim += units;
                        break;
                    case "up":
                        part1Depth -= units;
                        aim -= units;
                        break;
                }
            }

            return new string[] {
                $"Horizontal * depth = {horizontalPos * part1Depth}",
                $"Horizontal * depth (adjusted with aim) = {horizontalPos * part2Depth}"
            };
        }
    }
}
