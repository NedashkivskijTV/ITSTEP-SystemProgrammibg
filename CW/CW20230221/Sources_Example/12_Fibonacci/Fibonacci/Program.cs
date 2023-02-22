using System;
using System.Threading;

namespace Fibonacci
{
    class FibonacciClass
    {
        static void Main()
        {
            const int threadCount = 10;

            ManualResetEvent[] finishEvents = new ManualResetEvent[threadCount];
            FibonacciCalculatorClass[] numberArr = new FibonacciCalculatorClass[threadCount];
            Random r = new Random();

            Console.WriteLine("Запуск операции подсчета...");
            for (int i = 0; i < threadCount; i++)
            {
                finishEvents[i] = new ManualResetEvent(false);
                FibonacciCalculatorClass f = new FibonacciCalculatorClass(r.Next(10, 20), finishEvents[i]);
                numberArr[i] = f;
                ThreadPool.QueueUserWorkItem(f.CallBackMethod);
            }

            WaitHandle.WaitAll(finishEvents);
            Console.WriteLine("Вычисления закончены");

            for (int i = 0; i < threadCount; i++)
            {
                FibonacciCalculatorClass f = numberArr[i];
                Console.WriteLine($"{f.Number}-е число Фибоначчи = {f.Result}");
            }
        }
    }

    public class FibonacciCalculatorClass
    {
        private ManualResetEvent ev;

        public int Number { get; private set; } 

        public int Result { get; private set; } 
        
        public FibonacciCalculatorClass(int n, ManualResetEvent mre)
        {
            Number = n;
            ev = mre;
        }

        public void CallBackMethod(Object arg)
        {
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} стартовал...");
            Result = Calculate(Number);
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} вычисления закончил...");
            ev.Set();
        }

        public int Calculate(int nunber)
        {
            if (nunber <= 1)
            {
                return nunber;
            }

            return Calculate(nunber - 1) + Calculate(nunber - 2);
        }
    }
}
