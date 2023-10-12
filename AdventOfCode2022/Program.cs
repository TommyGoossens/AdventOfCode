using AdventOfCodeShared;
using System.Reflection;

var programList = AdventOfCodeProgramFactory.CreatePrograms(Assembly.GetExecutingAssembly());
AdventOfCodeProgramRunner.RunPrograms(programList);