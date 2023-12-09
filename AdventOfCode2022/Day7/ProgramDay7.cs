using System.Text.RegularExpressions;
using AdventOfCodeShared;

namespace AdventOfCode2022.Day7
{

    public class File
    {
        public int Size { get; private init; }
        public string Name { get; private init; }

        private File(int size, string name)
        {
            Size = size;
            Name = name;
        }

        public static File? Create(string fileLine)
        {
            Regex re = new(@"\d+");
            if (!re.IsMatch(fileLine)) return null;
            var fileSizeAsString = re.Match(fileLine).Value;
            var size = int.Parse(fileSizeAsString);
            var name = fileLine.Split(fileSizeAsString).Last();
            return new File(size, name);
        }
    }

    public class Directory
    {
        public string Name { get; private init; }
        private List<File> files { get; init; }
        private List<Directory> dirs { get; init; }
        public Directory(string line)
        {
            Name = line.Split("$ cd ").Last();
            files = new List<File>();
            dirs = new List<Directory>();
        }

        public int GetTotalSizeOfFiles() => files.Sum(f => f.Size) + dirs.Sum(d => d.GetTotalSizeOfFiles());
        public void AddFile(string line)
        {
            var file = File.Create(line);
            if (file != null) files.Add(file);
        }

        public Directory AddDirectory(Directory dir)
        {
            dirs.Add(dir);
            return dir;
        }
    }

    public class ProgramDay7 : AdventOfCodeProgram<int>
    {
        public ProgramDay7(string? text = null) : base(text)
        {
        }
        protected override int RunPartTwo()
        {
            var listOfAllKnownDirectories = GetListOfAllKnownDirectories();
            var rootDir = listOfAllKnownDirectories.First(d => d.Name.Equals("/"));
            var usedSpace = rootDir!.GetTotalSizeOfFiles();
            var freeSpace = 70000000 - usedSpace;
            var needToClearUp = 30000000 - freeSpace;
            var dirsWithMatchingSize = listOfAllKnownDirectories.Where(d => d.GetTotalSizeOfFiles() >= needToClearUp).ToList();

            var part2 = dirsWithMatchingSize.Select(d => d.GetTotalSizeOfFiles()).OrderBy(d => d).First();
            return part2;
        }

        protected override int RunPartOne()
        {
            var listOfAllKnownDirectories = GetListOfAllKnownDirectories();

            var part1 = listOfAllKnownDirectories.Where(d => d.GetTotalSizeOfFiles() <= 100000).Sum(d => d.GetTotalSizeOfFiles());

            return part1;
        }

        private IEnumerable<Directory> GetListOfAllKnownDirectories()
        {
            var directoryTree = new Stack<Directory>();
            var listOfAllKnownDirectories = new List<Directory>();
            Directory? rootDir = null;
            Directory? currentDir = null;
            foreach (var l in Lines)
            {
                if (l.StartsWith("$ cd") && !l.EndsWith(".."))
                {
                    var dir = new Directory(l);
                    rootDir ??= dir;
                    if (currentDir == null) currentDir = dir;
                    else currentDir = currentDir.AddDirectory(dir);
                    directoryTree.Push(dir);
                    listOfAllKnownDirectories.Add(dir);
                }
                else if (l.Equals("$ cd .."))
                {
                    directoryTree.Pop();
                    currentDir = directoryTree.First();
                }
                else if (l.Equals("$ ls")) continue;
                else currentDir?.AddFile(l);
            }
            return listOfAllKnownDirectories;
        }

        [Theory]
        [InlineData("$ cd /\r\n$ ls\r\ndir a\r\n14848514 b.txt\r\n8504156 c.dat\r\ndir d\r\n$ cd a\r\n$ ls\r\ndir e\r\n29116 f\r\n2557 g\r\n62596 h.lst\r\n$ cd e\r\n$ ls\r\n584 i\r\n$ cd ..\r\n$ cd ..\r\n$ cd d\r\n$ ls\r\n4060174 j\r\n8033020 d.log\r\n5626152 d.ext\r\n7214296 k", 95437)]
        public override void RunTestsPartOne(string input, int expectedResult)
        {
            new ProgramDay7(input).RunPartOne().Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("$ cd /\r\n$ ls\r\ndir a\r\n14848514 b.txt\r\n8504156 c.dat\r\ndir d\r\n$ cd a\r\n$ ls\r\ndir e\r\n29116 f\r\n2557 g\r\n62596 h.lst\r\n$ cd e\r\n$ ls\r\n584 i\r\n$ cd ..\r\n$ cd ..\r\n$ cd d\r\n$ ls\r\n4060174 j\r\n8033020 d.log\r\n5626152 d.ext\r\n7214296 k", 24933642)]
        public override void RunTestsPartTwo(string input, int expectedResult)
        {
            new ProgramDay7(input).RunPartTwo().Should().Be(expectedResult);
        }
    }
}
