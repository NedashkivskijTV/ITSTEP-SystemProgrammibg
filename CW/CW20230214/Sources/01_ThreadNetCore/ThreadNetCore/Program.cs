using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // получаем текущий поток
        Thread t = Thread.CurrentThread;

        //получаем имя потока
        Console.WriteLine($"Имя потока: {t.Name}");
        t.Name = "Метод Main";
        Console.WriteLine($"Имя потока: {t.Name}");

        Console.WriteLine($"ID потока: {t.ManagedThreadId}");
        Console.WriteLine($"Запущен ли поток: {t.IsAlive}");
        Console.WriteLine($"Приоритет потока: {t.Priority}");
        Console.WriteLine($"Статус потока: {t.ThreadState}");

        // получаем домен приложения
        Console.WriteLine($"Домен приложения: {Thread.GetDomain().FriendlyName}");

        Console.ReadLine();
    }
}