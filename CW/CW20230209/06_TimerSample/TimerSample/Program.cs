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

            // Запуск таймера.
            // Параметри у мілісекундах: 1-затримка першого стартуб 2-інтервал повторення
            timer.Change(1000, 1000); 

            Console.ReadKey(); // Задержка.

            // Звільнення ресурсів, виділених під таймер, завершення роботи таймера
            timer.Dispose();
        }

        static void TimerTick(object obj) // колбек метод отримує у параметрах таймер який його викликає - для реалізації можливості керування таймером з методу
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
