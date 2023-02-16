using System;
using System.Threading;

namespace TimerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Таймер запустится через 0,01 сек с интервалом 1 сек
            Timer t = new Timer(TimerMethod, "Method Main", 10, 1000);

            ////Таймер запустится однократно
            //Timer t = new Timer(TimerMethod, "Method Main", 0, Timeout.Infinite);

            ////Таймер не запустится никогда
            //Timer t = new Timer(TimerMethod, "Method Main", Timeout.Infinite, 1000);

            Console.WriteLine($"Основной поток {Thread.CurrentThread.ManagedThreadId} продолжается...");
            
            // Пауза виконання основного потоку на 5 сек
            Thread.Sleep(5000);

            t.Dispose();
        }

        static void TimerMethod(object obj)
        {
            Console.WriteLine($"{(string)obj} - Поток {Thread.CurrentThread.ManagedThreadId} : {DateTime.Now}");
        }
    }
}
