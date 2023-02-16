using System;
using System.Threading;

namespace PoolApplication
{
    class PoolUsingClass
    {
        static void Main(string[] args)
        {
            // Количество рабочих потоков в пуле потоков.
            int nWorkerThreads;

            // Количество потоков асинхронного ввода-вывода в пуле потоков.
            int nCompletionPortThreads;

            // Виведення максимальної кількості потоків
            ThreadPool.GetMaxThreads(out nWorkerThreads, out nCompletionPortThreads);
            Console.WriteLine($"Максимальное количество потоков: {nWorkerThreads}" 
                + $"\nПотоков ввода-вывода доступно: {nCompletionPortThreads}\n");
            
            // Виведення мінімальної кількості потоків
            ThreadPool.GetMinThreads(out nWorkerThreads, out nCompletionPortThreads);
            Console.WriteLine($"Минимальное количество потоков: {nWorkerThreads}"
                + $"\nПотоков ввода-вывода доступно: {nCompletionPortThreads}\n");

            // Головний код
            Console.WriteLine($"Основной поток {Thread.CurrentThread.ManagedThreadId}: ставим в очередь рабочий элемент");
            Random r = new Random();
            for (int i = 0; i < 10; ++i)
                ThreadPool.QueueUserWorkItem(WorkingElementMethod, r.Next(100));

            // Виведення кількості доступних потоків
            ThreadPool.GetAvailableThreads(out nWorkerThreads, out nCompletionPortThreads);
            Console.WriteLine($"Доступное количество потоков: {nWorkerThreads}"
                + $"\nПотоков ввода-вывода доступно: {nCompletionPortThreads}\n");

            // Продовження роботи головного потоку
            Console.WriteLine("Основной поток: выполняем другие задачи");
            Thread.Sleep(1000);
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadLine();
        }

        private static void WorkingElementMethod(object state)
        {
            Console.WriteLine($"\tпоток: {Thread.CurrentThread.ManagedThreadId} состояние = {state}");
            Thread.Sleep(1000);
        }
    }
}
