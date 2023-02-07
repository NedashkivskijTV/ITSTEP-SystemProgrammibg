using AssemblyExample;

namespace ModuleOne
{
    public class ClassMul : IAssemblyExample
    {
        public string SomeMethod(int a, int b)
        {
            return $"ModuleOne: {a * b}";
        }
    }
}
