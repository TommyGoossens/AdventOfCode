
using System.Text.RegularExpressions;
using AdventOfCodeShared;
using AdventOfCodeShared.Models;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2022.Day10
{
    internal class HandheldCommunicationProgramParser
    {
        public int Cycles { get; private set; }
        private int signalStrength = 0;
        private int xValue = 1;
        //private readonly Dictionary<char, int> results = new();

        private bool ShouldRegisterSignalStrength() => Cycles == 20 || (Cycles - 20) % 40 == 0;

        private void RegisterValueAndResetValue(Operation o)
        {
            //if (!results.TryGetValue(o.VariableName, out var _)) results[o.VariableName] = 1;
            var strength = xValue * Cycles;
            signalStrength += strength;
            //results[o.VariableName] = 1;
        }

        public int GetSumOfSignalStrenghts(IEnumerable<Operation> operations)
        {
            foreach (var o in operations)
            {
                if (o.OperationType == OperationType.noop)
                {
                    Cycles++;
                    if (ShouldRegisterSignalStrength()) RegisterValueAndResetValue(o);
                    continue;
                }

                for (var i = 0; i < (int)o.OperationType; i++)
                {
                    Cycles++;
                    if (ShouldRegisterSignalStrength()) RegisterValueAndResetValue(o);
                }
                xValue += o.Value;
            }


            return signalStrength;
        }

    }

    public class ProgramDay10 : AdventOfCodeProgram
    {


        public ProgramDay10(string? text = null) : base(text)
        { }

        protected override string RunPartOne()
        {
            var operations = Lines.Select(Operation.Parse);
            return new HandheldCommunicationProgramParser().GetSumOfSignalStrenghts(operations).ToString();
        }

        protected override string RunPartTwo()
        {
            return "";
        }

        [Theory]
        [InlineData("addx 15\naddx -11\naddx 6\naddx -3\naddx 5\naddx -1\naddx -8\naddx 13\naddx 4\nnoop\naddx -1\naddx 5\naddx -1\naddx 5\naddx -1\naddx 5\naddx -1\naddx 5\naddx -1\naddx -35\naddx 1\naddx 24\naddx -19\naddx 1\naddx 16\naddx -11\nnoop\nnoop\naddx 21\naddx -15\nnoop\nnoop\naddx -3\naddx 9\naddx 1\naddx -3\naddx 8\naddx 1\naddx 5\nnoop\nnoop\nnoop\nnoop\nnoop\naddx -36\nnoop\naddx 1\naddx 7\nnoop\nnoop\nnoop\naddx 2\naddx 6\nnoop\nnoop\nnoop\nnoop\nnoop\naddx 1\nnoop\nnoop\naddx 7\naddx 1\nnoop\naddx -13\naddx 13\naddx 7\nnoop\naddx 1\naddx -33\nnoop\nnoop\nnoop\naddx 2\nnoop\nnoop\nnoop\naddx 8\nnoop\naddx -1\naddx 2\naddx 1\nnoop\naddx 17\naddx -9\naddx 1\naddx 1\naddx -3\naddx 11\nnoop\nnoop\naddx 1\nnoop\naddx 1\nnoop\nnoop\naddx -13\naddx -19\naddx 1\naddx 3\naddx 26\naddx -30\naddx 12\naddx -1\naddx 3\naddx 1\nnoop\nnoop\nnoop\naddx -9\naddx 18\naddx 1\naddx 2\nnoop\nnoop\naddx 9\nnoop\nnoop\nnoop\naddx -1\naddx 2\naddx -37\naddx 1\naddx 3\nnoop\naddx 15\naddx -21\naddx 22\naddx -6\naddx 1\nnoop\naddx 2\naddx 1\nnoop\naddx -10\nnoop\nnoop\naddx 20\naddx 1\naddx 2\naddx 2\naddx -6\naddx -11\nnoop\nnoop\nnoop", "13140")]
        public override void RunTestsPartOne(string input, string expectedResult)
        {
            var program = new ProgramDay10(input);
            var result = program.RunPartOne();
            result.Should().StartWithEquivalentOf(expectedResult);
        }

        [Theory]
        [InlineData("R 5\r\nU 8\r\nL 8\r\nD 3\r\nR 17\r\nD 10\r\nL 25\r\nU 20", "36")]
        public override void RunTestsPartTwo(string input, string expectedResult)
        {
            var program = new ProgramDay10(input);
            var result = program.RunPartTwo();
            result.Should().StartWithEquivalentOf(expectedResult);
        }
    }

    internal class Operation
    {
        public OperationType OperationType { get; private init; }
        public char VariableName { get; private init; }
        public int Value { get; private init; }

        private Operation(OperationType opType, char variableName, string valueToAdd)
        {
            OperationType = opType;
            if (opType == OperationType.noop) return;
            VariableName = variableName;
            Value = int.Parse(valueToAdd);
        }

        public static Operation Parse(string line)
        {
            var opType = line switch
            {
                "noop" => OperationType.noop,
                { } when line.StartsWith("add") => OperationType.add,
                _ => throw new ArgumentException($"Could not parse the givent text into an Operation: {line}")
            };

            var variableName = line[3];
            var valueToAdd = line[4..];

            return new(opType, variableName, valueToAdd);
        }
    }


    internal enum OperationType
    {
        noop = 1,
        add = 2
    }

    //internal static partial class OperationExtensions
    //{
    //    internal static Operation ParseOperation(string input)
    //    {
    //        var match = MyRegex().Match(input).Value;
    //        return match switch
    //        {
    //            "noop" => Operation.noop,
    //            { } when match.StartsWith("add") => Operation.add,
    //            _ => throw new ArgumentException($"Could not parse the givent text into an Operation: {input}")
    //        };
    //    }

    //    //internal static (char VariableName, int Change) GetAddOperation(this Operation input, string fullText)
    //    //{
    //    //    if (input != Operation.add) throw new InvalidOperationException($"{input} is not supported in this context");
    //    //    var varName = fullText[3];
    //    //    var changeText = fullText[4..];
    //    //    return (varName, int.Parse(changeText));
    //    //}

    //    [GeneratedRegex("\\w{1,}")]
    //    private static partial Regex MyRegex();
    //}
}
