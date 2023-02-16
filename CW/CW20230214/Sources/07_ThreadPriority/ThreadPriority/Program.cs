using System;
using System.Threading;

namespace ThreadPriority1
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t1 = new Thread(Method);
            Thread t2 = new Thread(Method);
            Thread t3 = new Thread(Method);
            Thread t4 = new Thread(Method);
            Thread t5 = new Thread(Method);

            // Назначение приоритета потоку
            t1.Priority = ThreadPriority.Highest;
            t2.Priority = ThreadPriority.AboveNormal;
            t3.Priority = ThreadPriority.Normal;
            t4.Priority = ThreadPriority.BelowNormal;
            t5.Priority = ThreadPriority.Lowest;

            t1.Start(t1.Priority.ToString());
            t2.Start("\t\t"+t2.Priority.ToString());
            t3.Start("\t\t\t\t"+t3.Priority.ToString());
            t4.Start("\t\t\t\t\t\t"+t4.Priority.ToString());
            t5.Start("\t\t\t\t\t\t\t\t"+t5.Priority.ToString());

            Console.ReadKey();
        }

        static void Method(object str)
        {
            string text = (string)str;
            for (int i = 1; i <= 500; i++)
            {
                Console.WriteLine($"{text} #{i}");
            }
        }
    }
}
