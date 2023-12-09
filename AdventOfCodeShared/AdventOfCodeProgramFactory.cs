using System.Reflection;

namespace AdventOfCodeShared
{
    public static class AdventOfCodeProgramFactory
    {
        public static IEnumerable<IAdventOfCodeProgram> CreatePrograms(Assembly assembly)
        {
            var programList = new List<IAdventOfCodeProgram>();

            foreach (var program in assembly.GetTypes().Where(type => typeof(IAdventOfCodeProgram).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract))
            {
                try
                {
                    if (Activator.CreateInstance(program, string.Empty) is IAdventOfCodeProgram instance) programList.Add(instance);
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