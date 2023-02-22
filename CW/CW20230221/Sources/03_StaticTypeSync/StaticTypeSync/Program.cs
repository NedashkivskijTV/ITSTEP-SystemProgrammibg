using System;
using System.Threading;

namespace StaticTypeSync
{
    class StaticTypeSyncClass
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Синхронизация статического типа:");

            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(StaticLockCounter.UpdateFields);
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();

            Console.WriteLine($"Field1: {StaticLockCounter.Field1}, Field2: {StaticLockCounter.Field2}\n\n");

        }
    }

    static class StaticLockCounter
    {
        public static int Field1 { get; private set; }

        public static int Field2 { get; private set; }

        public static void UpdateFields()
        {
            for (int i = 0; i < 1000000; ++i)
            {
                lock(typeof(StaticLockCounter))
                {
                    ++Field1;
                    if (Field1 % 2 == 0)
                        ++Field2;
                }
            }
        }
    }

}
