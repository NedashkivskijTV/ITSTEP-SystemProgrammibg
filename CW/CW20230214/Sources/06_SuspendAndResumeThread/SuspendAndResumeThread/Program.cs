using System;
using System.Threading;

namespace SuspendAndResumeThread
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(Method);
            t.Start(); // Запуск потока.

            try
            {
                Console.WriteLine("Нажмите любую клавишу для остановки");
                Console.ReadKey();
                t.Suspend(); // Приостановка потока.

                Console.WriteLine("Поток остановлен!");
                Console.WriteLine("Нажмите любую клавишу для возобновления");
                Console.ReadKey();
                t.Resume(); // Возобновление работы.

                Console.WriteLine("Нажмите кнопку для завершения потока");
                Console.ReadKey();
                t.Abort(); // Принудительное завершение работы потока.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void Method()
        {
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(100);
                    Console.WriteLine(i.ToString());
                }
            }
            finally
            {
                // Попадаем сюда в любом случае.
                Console.WriteLine("End Thread Work");
            }
        }
    }
}
