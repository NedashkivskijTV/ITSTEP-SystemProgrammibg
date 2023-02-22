using System;
using System.Threading;

namespace GoodLockSync
{
    class GoodLockSyncClass
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Синхронизация динамического типа:");

            LockCounter lc = new LockCounter();

            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(lc.UpdateFields);
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();

            Console.WriteLine($"Field1: {lc.Field1}, Field2: {lc.Field2}");

            Console.ReadKey();
        }
    }

    class LockCounter
    {
        // Внутренний объект для блокировки
        object lockObj = new object();

        public int Field1 { get; private set; }

        public int Field2 { get; private set; }

        public void UpdateFields()
        {
            for (int i = 0; i < 1000000; ++i)
            {
                // Блокируем внутренний объект
                lock(lockObj)
                {
                    ++Field1;
                    if (Field1 % 2 == 0)
                        ++Field2;
                }
            }
        }
    }
}


