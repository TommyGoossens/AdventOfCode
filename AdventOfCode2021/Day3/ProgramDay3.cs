using AdventOfCodeShared;
using System.Numerics;

namespace AdventOfCode2021.Day3
{
    internal static class Helper
    {
        internal static string To8BitString(this string original)
        {
            var len = original.Length;
            var missing = 8 - len;
            for (int i = 0; i < missing; i++) original = "0" + original;
            return original;
        }

        internal static string FlipBits(this string original)
        {
            var result = "";
            foreach (var c in original) result += c == '0' ? "1" : "0";
            return result.To8BitString();
        }

    }
    internal class ProgramDay3 : AdventOfCodeProgram
    {
        public ProgramDay3() : base(3)
        {
        }

        protected override string[] Run()
        {
            var binaryNumber = "";
            for (int i = 0; i < lines.First().Length; i++)
            {
                var col = lines.Select(l => l[i]).ToList();
                var nrOfZeros = col.Count(l => l == '0');
                var nrOfOnes = col.Count(l => l == '1');
                if (nrOfZeros > nrOfOnes) binaryNumber += "0";
                else binaryNumber += "1";
            }

            var gamma = Convert.ToInt32(binaryNumber.To8BitString(), 2);
            var epsilon = Convert.ToInt32(binaryNumber.FlipBits(), 2);

            return new string[] { $"Gamma ({gamma}) * Epsilon ({epsilon}) = {gamma * epsilon}" };
        }
    }
}
