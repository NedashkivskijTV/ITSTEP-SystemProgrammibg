using System;
using System.Reflection;

namespace ModulesInAction
{
    class Program
    {
        static void Main(string[] args)
        {
            string name_dll = "SampleLibrary";
            string method_print = "Print";

            Console.Title = "Модуль в действии";
            Console.WindowWidth = 50;
            Console.BufferWidth = 50;

            // Динамическое подключение сборки
            //загружаем сборку
            Assembly asm = Assembly.Load(AssemblyName.GetAssemblyName(name_dll+".dll"));

            //получаем необходимый модуль этой сборки
            Module mod = asm.GetModule(name_dll+".dll");

            //выводим список типов данных, объявленный в текущем модуле
            Console.WriteLine("Объявленные типы данных:");

            //получаем типы данных из сборки
            foreach (Type t in mod.GetTypes())
            {
                try
                {
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Тип -  {t.FullName}");
                    Console.ForegroundColor = ConsoleColor.White;
                    
                    object dataobject;

                    if (t.FullName.Contains("Person"))
                    {
                        //создаём объект полученного типа данных
                        dataobject = Activator.CreateInstance(t);
                        Console.WriteLine();
                        //вызываем метод Print от созданного объекта
                        t.GetMethod(method_print).Invoke(dataobject, null);

                        //создаём объект полученного типа данных
                        dataobject = Activator.CreateInstance(t, new object[] { "Иван", "Иванов", 30 });
                        Console.WriteLine();
                        //вызываем метод Print от созданного объекта
                        t.GetMethod(method_print).Invoke(dataobject, null);
                    }

                    if (t.FullName.Contains("Employee"))
                    {
                        //создаём объект полученного типа данных
                        dataobject = Activator.CreateInstance(t);
                        Console.WriteLine();
                        //вызываем метод Print от созданного объекта
                        t.GetMethod(method_print).Invoke(dataobject, null);

                        //создаём объект полученного типа данных
                        dataobject = Activator.CreateInstance(t, new object[] { "Иван", "Иванов", 30, "Merried", "Manager", (decimal)5000 });
                        Console.WriteLine();
                        //вызываем метод Print от созданного объекта
                        t.GetMethod(method_print).Invoke(dataobject, null);

                        //создаём объект полученного типа данных
                        dataobject = Activator.CreateInstance(t, new object[] { "Петр", "Петров", 20, "Saler", (decimal)3000 });
                        Console.WriteLine();
                        //вызываем метод Print от созданного объекта
                        t.GetMethod(method_print).Invoke(dataobject, null);
                    }
                }
                catch 
                {
                }
            }
        }
    }
}
