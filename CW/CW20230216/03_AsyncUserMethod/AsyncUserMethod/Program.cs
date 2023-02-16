using System;
using System.Threading;

namespace AsyncUserMethod
{
    class AsyncUserMethodClass
    {
        private delegate ulong AsyncSumDel(ulong n);

        static void Main(string[] args)
        {
            Console.WriteLine($"Основной поток начало ID = {Thread.CurrentThread.ManagedThreadId}\n");
            
            AsyncSumDel del = Sum;

            Console.WriteLine("Расчёт запущен...\n");

            // Синхронний варіант
            ulong res = del.Invoke(1000000);
            Console.WriteLine("Сумма = " + res);


            //// Асинхронний варіант
            //del.BeginInvoke(1000000, EndSum, del);

            Console.WriteLine($"Основной поток конец ID = {Thread.CurrentThread.ManagedThreadId}\n");
            Console.ReadKey();
        }

        public static ulong Sum(ulong n)
        {
            ulong sum = 1;
            for (ulong i = 2; i < n; ++i)
                sum += i;

            Console.WriteLine($"Метод {nameof(Sum)} поток ID = {Thread.CurrentThread.ManagedThreadId}\n");

            return sum;
        }

        private static void EndSum(IAsyncResult ar)
        {
            AsyncSumDel del = (AsyncSumDel)ar.AsyncState;
            ulong res = del.EndInvoke(ar);

            Console.WriteLine($"Метод {nameof(EndSum)} поток ID = {Thread.CurrentThread.ManagedThreadId}\n");
            Console.WriteLine("Сумма = " + res);
        }
    }
}
