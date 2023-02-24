using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Counter c = new Counter();

            Thread[] threads = new Thread[3];
            threads[0] = new Thread(c.task1);
            threads[1] = new Thread(c.task2);
            threads[2] = new Thread(c.task3);
            threads[0].Start();
            threads[1].Start();
            threads[2].Start();

            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();
        }

        class Counter
        {
            public Mutex m ;

            public Counter(){ m = new Mutex(false, @"Global\MyMutex"); }

            public bool IsPrime(int number)
            {
                if (number <= 1) return false;
                if (number == 2) return true;
                if (number % 2 == 0) return false;

                var boundary = (int)Math.Floor(Math.Sqrt(number));

                for (int i = 3; i <= boundary; i += 2)
                    if (number % i == 0)
                        return false;

                return true;
            }

            public bool IsSeven(int number)
            {
                if (number % 10 == 7)
                {
                    return true;
                }
                return false;
            }

            public void task1()
            {
                m.WaitOne();
                int[] Numbers = new int[50];
                Random rnd = new Random();
                for (int j = 0; j < Numbers.Length; ++j)
                {
                    Numbers[j] = rnd.Next(101);
                }

                try
                {
                    using (FileStream myStream = new FileStream("C:\\1.txt", FileMode.Create, FileAccess.Write))
                    {
                        StreamWriter myWriter = new StreamWriter(myStream);
                        for (int j = 0; j < Numbers.Length; ++j)
                        {
                            myWriter.WriteLine(Numbers[j]);
                        }
                        myWriter.Flush();
                        myWriter.Dispose();
                        myStream.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                m.ReleaseMutex();
            }

            public void task2()
            {
                m.WaitOne();
                String line;
                try
                {
                    StreamReader reader = new StreamReader(File.OpenRead("C:\\1.txt"));
                    line = reader.ReadToEnd();
                    reader.Close();
                    reader.Dispose();

                    var temp = line.Replace("\r\n", " ");
                    int[] numbers1 = temp.Split(' ').
                                Where(x => !string.IsNullOrWhiteSpace(x)).
                                Select(x => int.Parse(x)).ToArray();

                    using (FileStream myStream = new FileStream("C:\\2.txt", FileMode.Create, FileAccess.Write))
                    {
                        StreamWriter myWriter = new StreamWriter(myStream);
                        for (int j = 0; j < numbers1.Length; ++j)
                        {
                            if (IsPrime(numbers1[j]))
                            {
                                myWriter.WriteLine(numbers1[j]);
                            }
                        }
                        myWriter.Flush();
                        reader.Dispose();

                        myStream.Close();

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                m.ReleaseMutex();
            }

            public void task3()
            {
                m.WaitOne();
                String line;
                //try
                //{

                StreamReader reader = new StreamReader(File.OpenRead("C:\\2.txt"));
                line = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();

                var temp = line.Replace("\r\n", " ");
                    int[] numbers = temp.Split(' ').
                                Where(x => !string.IsNullOrWhiteSpace(x)).
                                Select(x => int.Parse(x)).ToArray();

                using (FileStream myStream = new FileStream("C:\\3.txt", FileMode.Create, FileAccess.Write))
                {
                    StreamWriter myWriter = new StreamWriter(myStream);
                    for (int j = 0; j < numbers.Length; ++j)
                    {
                        if (IsSeven(numbers[j]))
                        {
                            myWriter.WriteLine(numbers[j]);
                        }
                      

                    }
                    myWriter.Close();
                    reader.Dispose();

                    myStream.Close();

                }
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine("Exception: " + e.Message);
                //}
                m.ReleaseMutex();
            }
        }
    }
}
