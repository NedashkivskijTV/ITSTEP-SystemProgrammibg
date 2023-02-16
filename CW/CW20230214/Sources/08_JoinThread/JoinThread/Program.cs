using System;
using System.Threading;

namespace JoinThread
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread T = new Thread(Method);
            Console.WriteLine("Сейчас будет запущен поток");
            T.Start();
            Thread.Sleep(200);
            Console.WriteLine("Ожидание завершения работы потока");

            //Пробуем закомментировать эту строку
            T.Join();
            Console.WriteLine("Завершение работы программы");

            Console.ReadKey();

        }
        static void Method()
        {
            Console.WriteLine("Порожденный поток работает");
            Thread.Sleep(2000);
            Console.WriteLine("Порожденный поток завершил работу");
        }
    }
}
