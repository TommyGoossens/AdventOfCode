using AdventOfCodeShared;

namespace AdventOfCode2022.Day3
{
    public class ProgramDay3 : AdventOfCodeProgram<int>
    {
        private readonly Dictionary<char, int> letterValues;

        public ProgramDay3(string? text = null) : base(text)
        {
            letterValues = new Dictionary<char, int>();
            var allLetters = Enumerable.Range('a', 26);
            for (int i = 0; i < allLetters.Count(); i++)
            {
                var letter = (char)allLetters.ElementAt(i);
                letterValues.Add(letter, i + 1);
                letterValues.Add(char.ToUpper(letter), i + 27);
            }
        }

        protected override int RunPartOne() => GetAnswerPart1();

        protected override int RunPartTwo() => GetAnswerPart2();

        private int GetAnswerPart1()
        {
            var total = 0;
            Lines.AsParallel().ForAll(l =>
            {
                var firstHalf = l[..(l.Length / 2)];
                var secondHalf = l[(l.Length / 2)..];
                var itemsInBoth = firstHalf.Intersect(secondHalf);
                Interlocked.Add(ref total, GetValueOfIntersectedChars(itemsInBoth));
            });
            return total;
        }

        private int GetAnswerPart2()
        {
            var total = 0;
            var i = 0;
            while ((i * 3) != Lines.Count())
            {
                var batch = Lines.Skip(i * 3).Take(3);
                var intersection = batch.Skip(1).Aggregate(new HashSet<char>(batch.First()),
                    (h, e) => { h.IntersectWith(e); return h; }).ToList();
                total += GetValueOfIntersectedChars(intersection);
                i++;
            }

            return total;
        }

        private int GetValueOfIntersectedChars(IEnumerable<char> chars) => chars.Where(c => letterValues.TryGetValue(c, out var _)).Select(c => letterValues[c]).Sum();

        [Theory]
        [InlineData("vJrwpWtwJgWrhcsFMMfFFhFp\r\njqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL\r\nPmmdzqPrVvPwwTWBwg\r\nwMqvLMZHhHMvwLHjbvcjnnSBnvTQFn\r\nttgJtRGJQctTZtZT\r\nCrZsJsPPZsGzwwsLwLmpwMDw", 157)]
        public override void RunTestsPartOne(string input, int expectedResult)
        {
            new ProgramDay3(input).RunPartOne().Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("vJrwpWtwJgWrhcsFMMfFFhFp\r\njqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL\r\nPmmdzqPrVvPwwTWBwg\r\nwMqvLMZHhHMvwLHjbvcjnnSBnvTQFn\r\nttgJtRGJQctTZtZT\r\nCrZsJsPPZsGzwwsLwLmpwMDw", 70)]
        public override void RunTestsPartTwo(string input, int expectedResult)
        {
            new ProgramDay3(input).RunPartTwo().Should().Be(expectedResult);
        }
    }
}
