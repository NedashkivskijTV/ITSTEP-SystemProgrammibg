using System;
using System.Threading;
namespace ThreadSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //// Var1

            //// Создание объекта потока.
            //Thread thread = new Thread(Method);

            //// Запуск работы потока.
            //thread.Start(); // Запуск работы потока.

            //Console.WriteLine($"Primary Thread ID - {Thread.CurrentThread.ManagedThreadId}");

            //// Работа основного потока.
            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine($"Hello in main-{i}");
            //}
            //// Var1


            // Var2
            Console.WriteLine($"Primary Thread ID - {Thread.CurrentThread.ManagedThreadId}");
            ParameterizedThreadStart threadstart = new ParameterizedThreadStart(ThreadFunk);

            // Запуск первого потока.
            Thread thread1 = new Thread(threadstart);
            Console.WriteLine($"Thread ID - {thread1.ManagedThreadId}");
            thread1.Start("One");

            // Запуск второго потока.
            Thread thread2 = new Thread(threadstart);
            Console.WriteLine($"\t\tThread ID - {thread2.ManagedThreadId}");
            thread2.Start("\t\tTwo");
            // Var2
        }

        ////Var1
        ////Метод в котором будет выполнятся поток.
        //static void Method()
        //{
        //    Console.WriteLine($"\t\t\tThread ID - {Thread.CurrentThread.ManagedThreadId}");

        //    // Работа вторичного потока.
        //    for (int i = 0; i < 100; i++)
        //    {
        //        Console.WriteLine($"\t\t\tHello in thread-{i}");
        //    }
        //}
        ////Var1


        //Var2
        static void ThreadFunk(object a)
        {
            // Получаем String из прнятого object.
            string ID = (string)a;

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"{ID}-{i}");
            }
        }
        //Var2
    }
}
