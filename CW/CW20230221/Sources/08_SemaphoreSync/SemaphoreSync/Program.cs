using System;
using System.Threading;

namespace SemaphoreSync
{
    class Program
    {
        static void Main(string[] args)
        {
            // public Semaphore (int initialCount, int maximumCount, string name);
            // initialCount Начальное количество запросов для семафора, которое может быть обеспечено одновременно.
            // maximumCount Максимальное количество запросов семафора, которое может быть обеспеченно одновременно.
            // name Имя, если объект синхронизации должен использоваться совместно с другими процессами.
            // В противном случае — null или пустая строка.Имя указано с учетом регистра.

            Semaphore s = new Semaphore(3, 3, "My_SEMAPHORE");

            for (int i = 0; i < 6; ++i)
                ThreadPool.QueueUserWorkItem(SomeMethod, s);

            Console.ReadKey();
        }

        static void SomeMethod(object obj)
        {
            Semaphore s = obj as Semaphore;
            bool stop = false;

            while (!stop)
            {
                if (s.WaitOne(500))
                {
                    try
                    {
                        Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} блокировку получил");
                        Thread.Sleep(2000);
                    }
                    finally
                    {
                        stop = true;
                        Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} блокировку снял");
                        s.Release();
                    }
                }
                else
                    Console.WriteLine($"Таймаут для потока {Thread.CurrentThread.ManagedThreadId} истек. Повторное ожидание");
            }
        }
    }
}
