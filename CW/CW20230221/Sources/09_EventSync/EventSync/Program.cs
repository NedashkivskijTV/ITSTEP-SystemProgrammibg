using System;
using System.Threading;

namespace EventSync
{
    class EventSyncClass
    {
        static void Main(string[] args)
        {
            ////Var1
            //EventTest("Man");

            //Var2
            EventTest("Auto");
        }

        private static void EventTest(string typeEvent)
        {
            Console.WriteLine("Событие с ручным сбросом:");
            EventWaitHandle re;
            if (typeEvent == "Man")
                re = new ManualResetEvent(true); //Поток не блокируется сигнальное состояние включено
            else
                re = new AutoResetEvent(true); //Поток не блокируется сигнальное состояние включено

            for (int i = 0; i < 10; ++i)
                ThreadPool.QueueUserWorkItem(SomeEventMethod, re);

            Console.ReadKey();
        }

        
        static void SomeEventMethod(object obj)
        {
            EventWaitHandle ev = obj as EventWaitHandle;

            if (ev.WaitOne(10))
            {
                Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} успел проскочить");

                ////Var1
                ////Thread.Sleep(1); // Пробуем раскомментировать
                //ev.Reset(); //Задает несигнальное состояние события, вызывая блокирование потоков.
                ////Var1

                //Var2
                Thread.Sleep(1);
                // Пробуем раскомментировать
                // ev.Set(); //Задает сигнальное состояние события, вызывая снятие блокирования потоков.
                // Пробуем раскомментировать
                //Var2 
            }
            else
                Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} опоздал");
        }
    }
}
