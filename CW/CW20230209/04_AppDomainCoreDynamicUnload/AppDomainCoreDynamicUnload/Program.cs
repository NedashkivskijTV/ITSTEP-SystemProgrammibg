using System;
using System.Reflection;
using System.Runtime.Loader;
using System.IO;

namespace HelloApp
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadAssembly(6);
            
            // очистка
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // смотим, какие сборки у нас загружены
            PrintAssembly();

            Console.Read();
        }
 

        private static void LoadAssembly(int number)
        {
            var context = new AssemblyLoadContext("Factorial", true);

            try
            {
                // получаем путь к сборке MyApp
                var assemblyPath = Path.Combine(Directory.GetCurrentDirectory(), "MyApp.dll");
                // загружаем сборку
                Assembly assembly = context.LoadFromAssemblyPath(assemblyPath);

                PrintContextState("MyApp", "загружена");

                // установка обработчика выгрузки
                context.Unloading += Context_Unloading;

                // получаем тип Program из сборки MyApp.dll
                var type = assembly.GetType("MyApp.Program");
                // получаем его метод Factorial
                var greetMethod = type.GetMethod("Factorial");

                // вызываем метод
                var instance = Activator.CreateInstance(type);
                var result = greetMethod?.Invoke(instance, new object[] { number });

                if (result is int)
                {
                    // выводим результат метода на консоль
                    Console.WriteLine($"Факториал числа {number} равен {result}\n\n");
                }

                // смотим, какие сборки у нас загружены
                PrintAssembly();

                // выгружаем контекст
                context.Unload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void PrintAssembly()
        {
            AppDomain domain = AppDomain.CurrentDomain;
            Console.WriteLine($"Имя домена: {domain.FriendlyName}\n");
            Console.WriteLine($"ID домена: {domain.Id}\n");

            // смотим, какие сборки у нас загружены
            Console.WriteLine($"Загруженные сборки:");
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                string name = asm.GetName().Name;

                if ((name == "MyApp") || (name == "AppDomainCoreDynamicUnload"))
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(name);

                if ((name == "MyApp") || (name == "AppDomainCoreDynamicUnload"))
                    Console.ForegroundColor = ConsoleColor.White;
            }
        }
        
        // обработчик выгрузки контекста
        private static void Context_Unloading(AssemblyLoadContext obj)
        {
            PrintContextState("MyApp", "выгружена");
        }

        private static void PrintContextState(string assemblyName, string state)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nБиблиотека {assemblyName} {state} \n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
