using AssemblyExample;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using static System.Console;

namespace SimpleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Определяем имя каталога, из которого запущена сборка
                string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                List<Type> list = new List<Type>();

                // Получаем все файлы с расширением dll в этом каталоге
                foreach (string s in Directory.GetFiles(path, "*.dll"))
                {
                    // Загружаем сборку по имени *.dll файла
                    Assembly a = Assembly.LoadFrom(s);

                    // Получаем все открытые типы из указанной сборки
                    foreach (Type t in a.GetExportedTypes())
                    {
                        //Для каждого типа осуществляем проверку, является ли данный тип классом
                        // и поддерживает ли он интерфейс IAssemblyExample
                        if (t.IsClass & typeof(IAssemblyExample).IsAssignableFrom(t))
                            list.Add(t);
                    }
                }

                if (list.Count > 0)
                {
                    Write("Введите первое число: ");
                    int number1 = int.Parse(ReadLine());

                    Write("Введите второе число: ");
                    int number2 = int.Parse(ReadLine());
                    WriteLine();

                    // вызов метода интерфейса для всех
                    // найденных типов
                    foreach (Type t in list)
                    {
                        WriteLine($"Class name - {t.Name}");
                        var currentType = Activator.CreateInstance(t) as IAssemblyExample;
                        WriteLine(currentType.SomeMethod(number1, number2));
                        WriteLine();
                    }
                }
                else WriteLine("Нет ни одного подходящего типа данных");

            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }
    }
}
