using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //1 Parallel.For
            Console.WriteLine("1 Parallel.For");

            //Var 1		
            ParallelLoopResult result1 = Parallel.For(0, 10, i =>
            //Var 1		

            ////Var 2		
            //ParallelLoopResult result1 = Parallel.For(0, 10, async i =>
            ////Var 2		
            {
                Console.WriteLine($"i: {i}, task: {Task.CurrentId}, thread: {Thread.CurrentThread.ManagedThreadId}");

                //Var 1		
                Thread.Sleep(10);
                //Var 1

                ////Var 2		
                //await Task.Delay(10);
                ////Var 2		

                Console.WriteLine($"\t\ti: {i}, task: {Task.CurrentId}, thread: {Thread.CurrentThread.ManagedThreadId}");
            });
            
            Console.WriteLine($"Is completed: {result1.IsCompleted}");
            Console.WriteLine($"Press Enter to continue");
            Console.Read();

            
            //2 Parallel.For
            Console.WriteLine("\n\n2 Parallel.For");
            ParallelLoopResult result2 = Parallel.For(10, 40, (int i, ParallelLoopState pis) =>
            {
                Console.WriteLine($"i: {i} task {Task.CurrentId}, thread: {Thread.CurrentThread.ManagedThreadId}");
                if (i > 15)
                    pis.Break();
            });
            Console.WriteLine($"Is completed: {result2.IsCompleted}");
            Console.WriteLine($"lowest break iteration: {result2.LowestBreakIteration}");
            Console.WriteLine($"Press Enter to continue");
            Console.Read();


            //3 Parallel.ForEach
            Console.WriteLine("\n\n3 Parallel.ForEach");
            string[] data = {"zero", "one", "two", "three", "four", "five",
                                 "six", "seven", "eight", "nine", "ten", "eleven", "twelve"};

            Parallel.ForEach(data, s =>
            {
                Console.WriteLine(s);
            });
            Console.WriteLine($"Press Enter to continue");
            Console.Read();

            
            //4 Parallel.Invoke
            Console.WriteLine("\n\n4 Parallel.Invoke");
            Parallel.Invoke(One, Two, Three);
            Console.Read();
        }

        //4
        static void One()
        {
            Console.WriteLine($"Method - {nameof(One)}");
        }
        static void Two()
        {
            Console.WriteLine($"Method - {nameof(Two)}");
        }
        static void Three()
        {
            Console.WriteLine($"Method - {nameof(Three)}");
        }
    }
}