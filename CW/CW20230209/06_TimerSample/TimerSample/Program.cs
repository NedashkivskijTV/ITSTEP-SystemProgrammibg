using System;
using System.Threading;

namespace TimerSample
{
    class Program
    {
        static int Sek = 10;

        static void Main(string[] args)
        {
            //// Полный вариант
            //TimerCallback timercallback = new TimerCallback(TimerTick);
            //Timer timer = new Timer(timercallback);

            // Сокращенный вариант
            Timer timer = new Timer(TimerTick);

            timer.Change(1000, 1000); // Запуск таймера.

            Console.ReadKey(); // Задержка.
            timer.Dispose();
        }

        static void TimerTick(object obj)
        {
            //// Var1
            //Console.WriteLine("Hello in timer");
            //// Var1

            // Var2
            Sek--;
            Console.WriteLine(Sek.ToString());
            if (Sek <= 0)
            {
                Timer a = (Timer)obj;
                a.Dispose(); // Остановка таймера.
                Console.WriteLine("Timer End");
            }
            // Var2
        }
    }
}
