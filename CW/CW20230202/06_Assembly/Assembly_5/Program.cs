using System;
using System.IO;
using System.Reflection;
using static System.Console;

namespace SimpleProject
{
    class Program
    {
        //Var2 Загрузка сборок
        //Определение всей иерархии базовых типов 
        static void BaseTypeSearch(Type currType)
        {
            if (currType.BaseType != null)
            {
                //Write($" -> {currType.BaseType.Name}");

                // Подняться на родительский уровень для данного класса
                BaseTypeSearch(currType.BaseType);
            }
            Write($" -> {currType.Name}");
        }
        //Var2 Загрузка сборок

        static void Main(string[] args)
        {
            ////Var1 определение текущей сборки
            ////Assembly theAssembly = Assembly.GetExecutingAssembly();

            //Assembly theAssembly = Assembly.LoadFrom(@"D:\ITSTEP\SystemPrograming\L-02\repit2\Module6_Урок2\Урок2\Source\05_ManyModuleAssembly\car.dll");

            //// использование свойств и методов для текущей сборки
            //WriteLine($"Assembly name:\t{theAssembly.FullName}\n");
            //WriteLine($"Full path of the uploaded file:\t{theAssembly.Location}\n");
            //WriteLine($"Whether the assembly was loaded into a context that is intended only for reflection:\t{theAssembly.ReflectionOnly}\n");
            //WriteLine($"Originally specifed assembly location:\t{theAssembly.CodeBase}\n");
            //WriteLine($"Assembly entry point:\t{theAssembly.EntryPoint}");
            //FileStream[] files = theAssembly.GetFiles(true);
            //WriteLine($"\nNumber of files: {files.Length}");
            //foreach (FileStream f in files)
            //{
            //    WriteLine($"\t{f.Name}");
            //}
            //Module[] modules = theAssembly.GetLoadedModules(true);
            //WriteLine($"\nNumber of modules: {modules.Length}");
            //foreach (Module m in modules)
            //{
            //    WriteLine($"\t{m.Name}");
            //}
            ////Var1 определение текущей сборки


            //Var2 Загрузка сборок
            Assembly a = Assembly.Load("mscorlib");

            foreach (Type t in a.GetTypes())
            {
                //Var21 - Раскомментировать
                if (!t.IsPrimitive)
                {
                    continue;
                }
                //Var21 - Раскомментировать

                Write($"Type name: {t.Name} ");

                Write("\tBase type hierarchy: ");
                BaseTypeSearch(t);

                WriteLine();
            }
            //Var2 Загрузка сборок
        }
    }
}