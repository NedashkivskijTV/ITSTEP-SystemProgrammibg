using AssemblyExample;

namespace ModuleThree
{
    public class ClassMod : IAssemblyExample
    {
        public string SomeMethod(int a, int b)
        {
            return $"ModuleThree: {a % b}";
        }
    }
}
