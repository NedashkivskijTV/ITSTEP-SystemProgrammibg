using System;
using System.Threading;

namespace TwoProcessEvent
{
    class TwoProcessEventClass
    {
        static EventWaitHandle canCalc;
        static int result;

        static void Main(string[] args)
        {
            if (EventWaitHandle.TryOpenExisting("PROCESS_EVENT", out canCalc))
            {

                //Задает сигнальное состояние события, позволяя одному или нескольким 
                //ожидающим потокам продолжить.
                canCalc.Set();

                result = 2;
            }
            else
            {
                Console.WriteLine("Не существует");
                canCalc = new EventWaitHandle(false, EventResetMode.AutoReset, "PROCESS_EVENT");
                result = 1;
                Console.WriteLine("Запустите второй экземпляр приложения...");
            }

            ThreadPool.QueueUserWorkItem(ThreadMethod);

            Console.ReadKey();
        }

        static void ThreadMethod(object obj)
        {
            canCalc.WaitOne();
            Thread.Sleep(1000);
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId}: Результат = {result}");
            result += 2;

            //Задает сигнальное состояние события, позволяя одному или нескольким 
            //ожидающим потокам продолжить.
            canCalc.Set();
            Thread.Sleep(1000);
            if (result <= 10)
                ThreadPool.QueueUserWorkItem(ThreadMethod);
        }
    }
}
