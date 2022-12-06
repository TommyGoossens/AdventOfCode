// See https://aka.ms/new-console-template for more information
using AdventOfCodeShared;
using System.Reflection;

var ProgramList = new List<AdventOfCodeProgram>();

foreach (var program in Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(AdventOfCodeProgram).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract))
{
    try
    {
        if (Activator.CreateInstance(program, string.Empty) is AdventOfCodeProgram instance) ProgramList.Add(instance);
    }
    catch (Exception)
    {
        Console.WriteLine($"Unable to create instance of: {program.Name}");
    }
}

foreach (var program in ProgramList.OrderByDescending(p => p.DayNumber)) Console.WriteLine(program.GetAnswer());