using AssemblyExample;

namespace ModuleTwo
{
    public class ClassDiv : IAssemblyExample
    {
        public string SomeMethod(int a, int b)
        {
            return $"ModuleTwo: {a / b}";
        }
    }
}
