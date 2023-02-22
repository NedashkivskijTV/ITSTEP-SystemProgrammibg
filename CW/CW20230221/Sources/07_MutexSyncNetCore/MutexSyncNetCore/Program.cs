using System;
using System.Threading;

namespace MutexSyncNetCore
{
    class Program
    {
        static Mutex mutexObj = new Mutex();
        static int x = 0;

        static void Main(string[] args)
        {
            Thread[] myThreads;
            int count = 5;

            myThreads = new Thread[count];

            for (int i = 0; i < count; i++)
            {
                myThreads[i] = new Thread(Counter);
                myThreads[i].Name = $"Поток {i+1}";
                myThreads[i].Start();
            }

            Console.ReadLine();
        }
        public static void Counter()
        {
            mutexObj.WaitOne();
            x = 0;
            for (int i = 1; i <= 9; i++)
            {
                x = i;
                Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                Thread.Sleep(100);
            }
            Console.WriteLine();
            mutexObj.ReleaseMutex();
        }
    }
}
