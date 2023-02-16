using System;
using System.Diagnostics;
using System.Reflection;

namespace ListAppDomain
{
    class Program
    {
        static void PrintInfo(AppDomain domain)
        {
            // имя домена приложения
            Console.WriteLine($"\n\nName Domen: {domain.FriendlyName}");

            // id домена приложения
            Console.WriteLine($"ID Domen: {domain.Id}");

            // базовый каталог, который используется для получения сборок
            // (как правило, каталог самого приложения)
            Console.WriteLine($"Base Directory: {domain.BaseDirectory}");
            Console.WriteLine();

            // получает набор сборок .NET, загруженных в домен приложения
            Assembly[] assemblies = domain.GetAssemblies();
            string nameAssembly = "";

            Console.WriteLine($"Loaded Assembly\n");
            foreach (Assembly asm in assemblies)
            {
                nameAssembly = asm.GetName().Name;

                if (domain.FriendlyName.Contains(nameAssembly))
                {
                    // получить имя сборки .NET
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Assembly of Project -  {nameAssembly}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                    Console.WriteLine(nameAssembly);
            }
        }

        static void Main(string[] args)
        {
            //выводим инфо текущего процесса
            Console.WriteLine("Current Process name: " + Process.GetCurrentProcess().ProcessName);
            Console.WriteLine("Current Process ID: " + Process.GetCurrentProcess().Id);


            // домен текущего приложения
            AppDomain baseDomain = AppDomain.CurrentDomain;

            // создаём домен приложения с приозвольным именем
            AppDomain addDomain1 = AppDomain.CreateDomain("AppDomain1");
            AppDomain addDomain2 = AppDomain.CreateDomain("AppDomain2");

            PrintInfo(baseDomain);
            PrintInfo(addDomain1);
            PrintInfo(addDomain2);

            // Вигружаем домен приложения
            AppDomain.Unload(addDomain1);
            AppDomain.Unload(addDomain2);

            Console.Read();
        }
    }
}
