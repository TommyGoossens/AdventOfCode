using AdventOfCodeShared;
using FluentAssertions;
using System.Text.RegularExpressions;
using Xunit;

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

    public class ProgramDay7 : AdventOfCodeProgram
    {
        public ProgramDay7(string? text = null) : base(text)
        {
        }

        protected override string[] Run()
        {
            var directoryTree = new Stack<Directory>();
            var listOfAllKnownDirectories = new List<Directory>();
            Directory? rootDir = null;
            Directory? currentDir = null;
            foreach (var l in lines)
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

            var part1 = listOfAllKnownDirectories.Where(d => d.GetTotalSizeOfFiles() <= 100000).Sum(d => d.GetTotalSizeOfFiles());
            var usedSpace = rootDir!.GetTotalSizeOfFiles();
            var freeSpace = 70000000 - usedSpace;
            var needToClearUp = 30000000 - freeSpace;
            var dirsWithMatchingSize = listOfAllKnownDirectories.Where(d => d.GetTotalSizeOfFiles() >= needToClearUp).ToList();
            
            var part2 = dirsWithMatchingSize.Select(d => d.GetTotalSizeOfFiles()).OrderBy(d => d).First();
            return new[] { $"Sum of files: {part1}", $"Size of dir to delete: {part2}" };
        }

        [Fact]
        public void RunTestsPartOne()
        {
            var program = new ProgramDay7("$ cd /\r\n$ ls\r\ndir a\r\n14848514 b.txt\r\n8504156 c.dat\r\ndir d\r\n$ cd a\r\n$ ls\r\ndir e\r\n29116 f\r\n2557 g\r\n62596 h.lst\r\n$ cd e\r\n$ ls\r\n584 i\r\n$ cd ..\r\n$ cd ..\r\n$ cd d\r\n$ ls\r\n4060174 j\r\n8033020 d.log\r\n5626152 d.ext\r\n7214296 k");
            var result = program.Run();
            var part1 = result.FirstOrDefault();
            part1.Should().EndWithEquivalentOf("95437");
        }

        [Fact]
        public void RunTestsPartTwo()
        {
            var program = new ProgramDay7("$ cd /\r\n$ ls\r\ndir a\r\n14848514 b.txt\r\n8504156 c.dat\r\ndir d\r\n$ cd a\r\n$ ls\r\ndir e\r\n29116 f\r\n2557 g\r\n62596 h.lst\r\n$ cd e\r\n$ ls\r\n584 i\r\n$ cd ..\r\n$ cd ..\r\n$ cd d\r\n$ ls\r\n4060174 j\r\n8033020 d.log\r\n5626152 d.ext\r\n7214296 k");
            var result = program.Run();
            var part2 = result.LastOrDefault();
            part2.Should().EndWithEquivalentOf("24933642");
        }
    }
}
