using System;
using System.Threading;

namespace LockSync
{
    class LockSyncClass
    {
        static void Main(string[] args)
        {
            Oper();
        }

        private static void Oper()
        {
            //Var1 BadOperation
            Console.WriteLine("Синхронизация Interlocked методом:");
            InterlockedCounter c = new InterlockedCounter();
            //Var1 BadOperation

            ////Var2 GoodOperation
            //Console.WriteLine("Синхронизация Monitor(Lock) методом:");
            //MonitorLockCounter c = new MonitorLockCounter();
            ////Var2 GoodOperation

            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(c.UpdateFields);
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();

            Console.WriteLine($"Field1: {c.Field1}, Field2: {c.Field2}\n\n");
        }
    }


    //Var1 BadOperation
    class InterlockedCounter
    {
        int field1;
        int field2;

        public int Field1
        {
            get { return field1; }
        }

        public int Field2
        {
            get { return field2; }
        }

        public void UpdateFields()
        {
            for (int i = 0; i < 1000000; ++i)
            {
                Interlocked.Increment(ref field1);
                if (field1 % 2 == 0)
                    Interlocked.Increment(ref field2);
            }
        }
    }
    //Var1 BadOperation


    ////Var2 GoodOperation
    //class MonitorLockCounter
    //{
    //    public int Field1 { get; private set; }

    //    public int Field2 { get; private set; }

    //    public void UpdateFields()
    //    {
    //        for (int i = 0; i < 1000000; ++i)
    //        {

    //            // Вариант 1 
    //            Monitor.Enter(this);
    //            try
    //            {
    //                ++Field1;
    //                if (Field1 % 2 == 0)
    //                    ++Field2;
    //            }
    //            finally
    //            {
    //                Monitor.Exit(this);
    //            }
    //            // Вариант 1 

    //            //// Вариант 2
    //            //lock (this)
    //            //{
    //            //    ++Field1;
    //            //    if (Field1 % 2 == 0)
    //            //        ++Field2;
    //            //}
    //            //// Вариант 2
    //        }
    //    }
    //}
    ////Var2 GoodOperation
}
