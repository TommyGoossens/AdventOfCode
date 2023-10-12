using System.Reflection;

namespace AdventOfCodeShared
{
    public static class AdventOfCodeProgramFactory
    {
        public static IEnumerable<AdventOfCodeProgram> CreatePrograms(Assembly assembly)
        {
            var programList = new List<AdventOfCodeProgram>();

            foreach (var program in assembly.GetTypes().Where(type => typeof(AdventOfCodeProgram).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract))
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

            return programList;
        }
    }

}