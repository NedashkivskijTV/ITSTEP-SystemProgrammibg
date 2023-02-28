// Выставляем режим Release - !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

using System;
using System.Threading;

namespace VolatileOperation
{
    class VolatileOperation
    {
        ////Var1,2
        //static bool finish = false;
        ////Var1,2

        //Var3
        static volatile bool finish = false; // властивість з модифікаторм volatile
        //Var3

        static void Main(string[] args)
        {
            new Thread(ThreadProc).Start();
            int x = 0;

            //Var1,3
            while (!finish)
            //Var1,3

            ////Var2
            //while (!Volatile.Read(ref finish)) // захищене читання
            ////Var2

            {
                x++;
            }
            Console.WriteLine($"x = {x}");
            Console.Read();
        }

        static void ThreadProc()
        {
            Thread.Sleep(1000);

            //Var1,3
            finish = true;
            //Var1,3

            ////Var2
            //Volatile.Write(ref finish, true); // захищена операція Write
            ////Var2
        }
    }
}
