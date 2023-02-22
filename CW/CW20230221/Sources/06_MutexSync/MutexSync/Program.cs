using System;
using System.Threading;

namespace MutexSync
{
    class MutexSyncClass
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Синхронизация мютексом начало {DateTime.Now.ToLocalTime().ToLongTimeString()}");

            Counter c = new Counter();

            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(c.UpdateFields);
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();

            Console.WriteLine($"Синхронизация мютексом конец {DateTime.Now.ToLocalTime().ToLongTimeString()}");
            Console.WriteLine($"Count: {c.Count}");
            Console.ReadKey();
        }

        class Counter
        {
            Mutex m;

            public Counter()
            {
                m = new Mutex(false, "SYNC_MUTEX"); //false - поток не блокировать сразу, SYNC_MUTEX - имя объекта синхронизации
            }

            public int Count { get; private set; }

            public void UpdateFields()
            {
                //// Var2
                // m.WaitOne();
                for (int i = 0; i < 1000000; ++i)
                {
                    // Var1
                    m.WaitOne();
                    ++Count;
                    // Var1
                    m.ReleaseMutex();
                }
                //// Var2
                // m.ReleaseMutex();
            }
        }
    }
}
