using AdventOfCodeShared;
using System.Reflection;

var programList = new List<AdventOfCodeProgram>();

foreach (var program in Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(AdventOfCodeProgram).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract))
{
    try
    {
        if (Activator.CreateInstance(program, string.Empty) is AdventOfCodeProgram instance) programList.Add(instance);
    }
    catch (Exception)
    {
        Console.WriteLine($"Unable to create instance of: {program.Name}");
    }
}

foreach (var program in programList.OrderBy(p => p.DayNumber))
{
    Console.WriteLine();
    program.RunProgramAndDisplayAnswer();
    Console.WriteLine();
}