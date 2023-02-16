    using System;
    using System.Threading;

    namespace BackgroundThreads
    {
        class Program
        {
            static void Main(string[] args)
            {
                Thread t = new Thread(Method);

                //// Делаем поток фоновым.
                ////При нажатии клавиши потоки останавливаются
                //t.IsBackground = true;

                //При нажатии клавиши вторичный поток работает дальше
                //Значение по умолчанию
                t.IsBackground = false;

                t.Start();

                Console.WriteLine("Нажмите любую клавишу для завершения работы приложения");
                Console.ReadKey();
            }

            static void Method()
            {
                for (int i = 10; i >= 0; i--)
                {
                    Thread.Sleep(500);
                    Console.Write(i.ToString() + " ");
                }
                Console.WriteLine("Фоновый поток завершил свою работу");
            }
        }
    }
