using System;
using System.IO;
using System.Threading;
using System.Text;

namespace AsyncReadCallBack
{
    class AsyncReadCallBackClass
    {
        private static byte[] staticData = new byte[1000];

        static void Main(string[] args)
        {
            //1 Вариант метод обратного вызова (callback делегат)
            AsyncReadOneFileCallBack();

            ////2 Вариант метод обратного вызова (анонимный метод)
            //AsyncReadOneFileCallBackAnonimus();

            ////3 Вариант массив методов обратного вызова (callback делегат)
            //AsyncReadMultiplyFilesAnonimus();
        }

        //1 Вариант метод обратного вызова (callback делегат)
        private static void AsyncReadOneFileCallBack()
        {
            Console.WriteLine($"Основной поток начало ID = {Thread.CurrentThread.ManagedThreadId}\n");

            FileStream fs = new FileStream(@"../../Program.cs", FileMode.Open,
                                           FileAccess.Read, FileShare.Read, 1024,
                                           FileOptions.Asynchronous);

            fs.BeginRead(staticData, 0, staticData.Length, ReadIsComplete, fs);

            Console.WriteLine($"Основной поток конец ID = {Thread.CurrentThread.ManagedThreadId}\n");
            Console.ReadLine();
        }

        private static void ReadIsComplete(IAsyncResult ar)
        {
            FileStream fs = (FileStream)ar.AsyncState;

            int bytesRead = fs.EndRead(ar);

            fs.Close();

            Console.WriteLine(Encoding.UTF8.GetString(staticData).Remove(0, 1));
            Console.WriteLine($"\nЧтение в потоке ID = {Thread.CurrentThread.ManagedThreadId} закончено");
            Console.WriteLine($"Количество считаных байт = {bytesRead}\n\n");
        }
        //1 Вариант метод обратного вызова (callback делегат)


        //2 Вариант метод обратного вызова (анонимный метод)
        private static void AsyncReadOneFileCallBackAnonimus()
        {
            byte[] data = new byte[1000];

            Console.WriteLine($"Основной поток начало ID = {Thread.CurrentThread.ManagedThreadId} \n");

            FileStream fs = new FileStream(@"../../Program.cs", FileMode.Open,
                                           FileAccess.Read, FileShare.Read, 1024,
                                           FileOptions.Asynchronous);

            fs.BeginRead(data, 0, data.Length,

                // Подвариант1 с делегатом
                delegate (IAsyncResult ar)
                // Подвариант1 с делегатом

                //// Подвариант2 с лямбда выражением
                //(IAsyncResult ar) =>
                //// Подвариант2 с лямбда выражением

                {
                    int bytesRead = fs.EndRead(ar);

                    fs.Close();

                    Console.WriteLine(Encoding.UTF8.GetString(data).Remove(0, 1));
                    Console.WriteLine($"\nЧтение в потоке ID = {Thread.CurrentThread.ManagedThreadId} закончено");
                    Console.WriteLine($"Количество считаных байт = {bytesRead}\n\n");

                    Console.ReadLine();
                },
                null);


            Console.WriteLine($"Основной поток конец ID = {Thread.CurrentThread.ManagedThreadId}\n");

            Console.ReadLine();
        }

    
        //3 Вариант массив методов обратного вызова (callback делегат)
        private static void AsyncReadMultiplyFilesAnonimus()
        {
            string[] files = {"../../Program.cs", 
                              "../../AsyncReadCallBack.csproj", 
                              "../../Properties/AssemblyInfo.cs"};

            Console.WriteLine($"Основной поток начало ID = {Thread.CurrentThread.ManagedThreadId}\n");
            
            for (int i = 0; i < files.Length; ++i)
                new AsyncCallBackReader(new FileStream(files[i], FileMode.Open, FileAccess.Read,
                                        FileShare.Read, 1024, FileOptions.Asynchronous), 500);

            Console.WriteLine($"Основной поток конец ID = {Thread.CurrentThread.ManagedThreadId}\n");
            Console.ReadLine();
        }
    }

    class AsyncCallBackReader
    {
        FileStream stream;
        byte[] data;
        IAsyncResult asRes;

        public AsyncCallBackReader(FileStream s, int size)
        {
            stream = s;
            data = new byte[size];
            asRes = s.BeginRead(data, 0, size, ReadIsComplete, null);
        }

        public void ReadIsComplete(IAsyncResult ar)
        {
            int countByte = stream.EndRead(asRes);
            stream.Close();
            Array.Resize(ref data, countByte);
            // Process the data.

            Console.WriteLine(Encoding.UTF8.GetString(data).Remove(0, 1));
            Console.WriteLine($"\nЧтение в потоке ID = {Thread.CurrentThread.ManagedThreadId} закончено");
            Console.WriteLine($"Количество прочитаных байт = {data.Length}\n");
        }
    }
}
