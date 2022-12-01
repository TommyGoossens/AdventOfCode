﻿// See https://aka.ms/new-console-template for more information
using AdventOfCodeShared;
using System.Reflection;

var ProgramList = new List<AdventOfCodeProgram>();

foreach (var program in Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(AdventOfCodeProgram).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract))
{
    if (Activator.CreateInstance(program) is AdventOfCodeProgram instance) ProgramList.Add(instance);
}

ProgramList = ProgramList.OrderBy(p => p.DayNumber).ToList();

foreach (var program in ProgramList) Console.WriteLine(program.GetAnswer());